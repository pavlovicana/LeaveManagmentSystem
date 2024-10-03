using LeaveManagmentSystem.Web.Data;
using LeaveManagmentSystem.Web.Models.LeaveTypes;
using AutoMapper;

namespace LeaveManagmentSystem.Web.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        
        public AutoMapperProfile() {

            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            // .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));

            CreateMap<LeaveTypeCreateVM, LeaveType>();

            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
        }
    }
}
