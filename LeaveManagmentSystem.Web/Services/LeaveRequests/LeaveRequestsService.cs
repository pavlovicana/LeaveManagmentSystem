
using AutoMapper;
using LeaveManagmentSystem.Web.Models.LeaveAllocations;
using LeaveManagmentSystem.Web.Models.LeaveRequests;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagmentSystem.Web.Services.LeaveRequests
{
    public class LeaveRequestsService(IMapper _mapper, UserManager<ApplicationUser> _userManager, IHttpContextAccessor _httpContextAccessor,
        ApplicationDbContext _context) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Cancelled;


            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(m => m.EndDate.Year == currentDate.Year); //one period per year
            var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations
                .FirstAsync(x => x.EmployeeId == leaveRequest.EmployeeId && x.LeaveTypeId == leaveRequest.LeaveTypeId
                && x.PeriodId == period.Id);
            allocation.DaysAllocated += numberOfDays; //add the days back to the allocation

            await _context.SaveChangesAsync();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            //map data to leave request data model
            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            //get logged in employee id
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            leaveRequest.EmployeeId = user.Id; //set employee id to logged in employee id

            //set LeaveRequestStatusId to pending

            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;
            //save leave request
            _context.Add(leaveRequest);

            //deduct leave days from leave allocation

            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(m => m.EndDate.Year == currentDate.Year); //one period per year

            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocationToDeduct = await _context.LeaveAllocations
                .FirstAsync(x => x.EmployeeId == user.Id && x.LeaveTypeId == model.LeaveTypeId
                && x.PeriodId == period.Id);

            allocationToDeduct.DaysAllocated -= numberOfDays;

            await _context.SaveChangesAsync();
        }

        public Task<LeaveRequestReadOnlyVM> GetAllLeaveRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var leaveRequests = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .Where(x => x.EmployeeId == user.Id)
                .ToListAsync();

            var model = leaveRequests.Select(x => new LeaveRequestReadOnlyVM
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                LeaveType = x.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequestStatusId,
                NumberOfDays = x.EndDate.DayNumber - x.StartDate.DayNumber
            }).ToList();

            return model;
        }

        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(m => m.EndDate.Year == currentDate.Year); //one period per year
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var allocation = await _context.LeaveAllocations
                .FirstAsync(x => x.EmployeeId == user.Id && x.LeaveTypeId == model.LeaveTypeId && x.PeriodId == period.Id);
            return allocation.DaysAllocated < numberOfDays;
        }

        public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved 
                ? (int)LeaveRequestStatusEnum.Approved 
                : (int)LeaveRequestStatusEnum.Declined;

            

            leaveRequest.ReviewerId = user.Id; //set reviewer id to logged in employee id

            if (!approved)
            {
                var currentDate = DateTime.Now;
                var period = await _context.Periods.SingleAsync(m => m.EndDate.Year == currentDate.Year); //one period per year
                var allocation = await _context.LeaveAllocations
                .FirstAsync(x => x.EmployeeId == leaveRequest.EmployeeId && x.LeaveTypeId == leaveRequest.LeaveTypeId && x.PeriodId == period.Id);
                
                var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
                allocation.DaysAllocated += numberOfDays; //add the days back to the allocation
            }
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var leaveRequests = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .ToListAsync();

            var approvedLeaveRequestsCount = leaveRequests.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved);

            var pendingLeaveRequestsCount = leaveRequests.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending);

            var declinedLeaveRequestsCount = leaveRequests.Count(x => x.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined);

            var leaveRequestsModels = leaveRequests.Select(x => new LeaveRequestReadOnlyVM
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                LeaveType = x.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequestStatusId,
                NumberOfDays = x.EndDate.DayNumber - x.StartDate.DayNumber
            }).ToList();

            var model = new EmployeeLeaveRequestListVM
            {
                ApprovedRequests = approvedLeaveRequestsCount,
                PendingRequests = pendingLeaveRequestsCount,
                DeclinedRequests = declinedLeaveRequestsCount,
                TotalRequests = leaveRequests.Count,
                LeaveRequests = leaveRequestsModels

            };

            return model;
        }

        public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstAsync(x => x.Id == id);
            var user = await _userManager.FindByIdAsync(leaveRequest.EmployeeId);

            var model = new ReviewLeaveRequestVM
            {
                Id = leaveRequest.Id,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveType = leaveRequest.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
                NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
                RequestComments = leaveRequest.RequestComments,
                Employee = new EmployeeListVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                },

            };
            return model;
        }
    }
}
