﻿using AutoMapper;
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
        }
    }
}
