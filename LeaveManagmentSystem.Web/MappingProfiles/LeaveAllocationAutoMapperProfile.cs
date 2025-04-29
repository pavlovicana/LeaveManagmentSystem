using AutoMapper;
using LeaveManagmentSystem.Web.Models.LeaveAllocations;
using LeaveManagmentSystem.Web.Models.LeaveTypes;
using LeaveManagmentSystem.Web.Models.Periods;

namespace LeaveManagmentSystem.Web.MappingProfiles
{
    public class LeaveAllocationAutoMapperProfile : Profile
    {
        public LeaveAllocationAutoMapperProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationVM>();
            CreateMap<Period, PeriodVM>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
            CreateMap<ApplicationUser, EmployeeListVM>();
            CreateMap<LeaveAllocation, LeaveAllocationEditVM>();    

        }

    }
}
