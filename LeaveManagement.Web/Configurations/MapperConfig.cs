using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Configurations
{
    public class MapperConfig : Profile
    {
        // Tell Automapper it is ok to convert Type A to Type B
        public MapperConfig()
        {
           CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();
           CreateMap<Employee, EmployeeListViewModel>().ReverseMap();
           CreateMap<Employee, EmployeeAllocationViewModel>().ReverseMap();
           CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
           CreateMap<LeaveAllocation, LeaveAllocationEditViewModel>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestViewModel>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestCreateViewModel>().ReverseMap();
           CreateMap<LeaveRequest, EmployeeLeaveRequestViewModel>().ReverseMap();
           CreateMap<LeaveRequest, AdminLeaveRequestViewModel>().ReverseMap();
        }
    }
}

