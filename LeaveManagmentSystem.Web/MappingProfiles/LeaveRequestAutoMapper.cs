using AutoMapper;
using LeaveManagmentSystem.Web.Models.LeaveRequests;

namespace LeaveManagmentSystem.Web.MappingProfiles
{
    public class LeaveRequestAutoMapperProfile : Profile
    {
        public LeaveRequestAutoMapperProfile()
        {
            CreateMap<LeaveRequestCreateVM,LeaveRequest>();

        }

    }
}
