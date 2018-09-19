using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Organizations.Models;
using AutoMapper;

namespace Organizations.Services
{
    public static class AutoMapper
    {
        public static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Organization, OrganizationLeaveManagement.Organization>();
            cfg.CreateMap<Employee, OrganizationLeaveManagement.Employee>();
            cfg.CreateMap<Leave, OrganizationLeaveManagement.Leave>();
            cfg.CreateMap<Task, OrganizationLeaveManagement.Task>();
        });
        public static readonly IMapper mapper = config.CreateMapper();   
    }
}
