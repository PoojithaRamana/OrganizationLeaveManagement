using Autofac;
using PetaPoco;

namespace Organizations.Services
{
    public static class DependencyInjection
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<LeaveService>().As<ILeaveService>();
            builder.RegisterType<TaskService>().As<ITaskService>();
            builder.Register<Database>(c => new Database("OrganizationLeaveManagement"));
            return builder.Build();
        }
    }
}
