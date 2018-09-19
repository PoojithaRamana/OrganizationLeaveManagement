using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizations.Models;
using Organizations.Services;
using PetaPoco;
using Autofac;

namespace OrganizationSimulation
{
    public class ClientModel
    {
        public static IOrganizationService organizationService;
        public static IEmployeeService employeeService;
        public static ILeaveService leaveService;
        public static ITaskService taskService;
        public ClientModel(IOrganizationService orgService, IEmployeeService empService, ILeaveService leavvService, ITaskService tasskService)
        {
            organizationService = orgService;
            employeeService = empService;
            leaveService = leavvService;
            taskService = tasskService;
        }
    }

    public class Client
    {
        public Client()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ClientModel>();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<LeaveService>().As<ILeaveService>();
            builder.RegisterType<TaskService>().As<ITaskService>();
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope("AutofacWebRequest"))
            {
              var records = scope.Resolve<ClientModel>();
            }
        }
    }
}
