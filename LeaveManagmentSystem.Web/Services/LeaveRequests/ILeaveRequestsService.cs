using LeaveManagmentSystem.Web.Models.LeaveRequests;

namespace LeaveManagmentSystem.Web.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequests();

        Task CancelLeaveRequest(int leaveRequestId);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestListVM> AdminGetAllLeaveRequests();
        Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id);
        Task ReviewLeaveRequest(int leaveRequestId, bool approved);
    }
}