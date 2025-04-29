using AutoMapper;
using LeaveManagmentSystem.Web.Common;
using LeaveManagmentSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace LeaveManagmentSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor,
        UserManager<ApplicationUser> _userManager, IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            // get all leave types
            var leaveTypes = await _context.LeaveTypes
                .Where(m=> !m.LeaveAllocations.Any(x => x.EmployeeId == employeeId)).ToListAsync();

            // get the current period based on the year
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(m => m.EndDate.Year == currentDate.Year); //one period per year
            var monthsRemaining = period.EndDate.Month - currentDate.Month;


            // foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    DaysAllocated = (int)Math.Ceiling(accuralRate * monthsRemaining)
                };
                _context.Add(leaveAllocation);
            }
            await _context.SaveChangesAsync();
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVm)
        {
            /*var leaveAllocation = await GetEmployeeAllocation(allocationEditVm.Id);
            if (leaveAllocation == null)
            {
                throw new Exception($"Leave allocation record does not exist");
            }
            leaveAllocation.DaysAllocated = allocationEditVm.DaysAllocated;
            _context.Update(leaveAllocation);
            await _context.SaveChangesAsync();*/
            _context.LeaveAllocations
                .Where(m => m.Id == allocationEditVm.Id)
                .ExecuteUpdate(m => m.SetProperty(x => x.DaysAllocated, allocationEditVm.DaysAllocated));
        }

        public async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            string employeeId = string.Empty;
            if (string.IsNullOrEmpty(userId))
            {
                employeeId = userId;

            }
            else
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
                employeeId = user.Id;
            }         
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(m => m.EndDate.Year == currentDate.Year);
            var leaveAllocations = await _context.LeaveAllocations
                .Include(m => m.LeaveType) //join statement
                .Include(m => m.Period) //join statement
                .Where(m => m.EmployeeId == userId && m.PeriodId == period.Id)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(m => m.LeaveType)
                .Include(m => m.Employee)
                .FirstOrDefaultAsync(m => m.Id == allocationId);

            var model= _mapper.Map<LeaveAllocationEditVM>(allocation);
            return model;

        }

        public async Task<EmployeeAllocationVM> GetEmployeeLeaveAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User) 
                : await _userManager.FindByIdAsync(userId);

            var allocations = await GetAllocations(user.Id);
            var allocationVMlist = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();

            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVMlist,
                isComplitedAllocation = leaveTypesCount == allocations.Count
            };
            return employeeVm;
        }
        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }
        private async Task<bool>AllocationExists(string userId, int periodId, int LeaveTypeId)
        {
           var exists =  await _context.LeaveAllocations
                .AnyAsync(m => 
                m.EmployeeId == userId && 
                m.PeriodId == periodId && 
                m.LeaveTypeId == LeaveTypeId);
            return exists;
        }

    }
}
