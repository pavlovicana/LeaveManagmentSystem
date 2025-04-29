using LeaveManagmentSystem.Web.Models.LeaveAllocations;

namespace LeaveManagmentSystem.Web.Services.LeaveAllocations
{
    public interface ILeaveAllocationsService
    {
        Task AllocateLeave(string employeeId);
        Task<List<LeaveAllocation>> GetAllocations(string? userId);
        Task<EmployeeAllocationVM> GetEmployeeLeaveAllocations(string? userId);
        Task<List<EmployeeListVM>> GetEmployees();
        Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);
        Task EditAllocation(LeaveAllocationEditVM allocationEditVm);
    }
}