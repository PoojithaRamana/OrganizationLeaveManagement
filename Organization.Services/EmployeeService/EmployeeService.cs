using System;
using System.Collections.Generic;
using Organizations.Models;
using PetaPoco;

namespace Organizations.Services
{ 
    public class EmployeeService : IEmployeeService
    {
        public Database DB { get; set; }
        public ILeaveService LeaveService { get; set; }
        public EmployeeService(Database db, ILeaveService leaveService)
        {
            this.DB = db;
            this.LeaveService = leaveService;
        }

        public void CreateEmployee(Employee employee, int totalSickLeaves, int totalCausalLeaves, int totalSpecialLeaves)
        {
            OrganizationLeaveManagement.Employee employeeDB = AutoMapper.mapper.Map<Employee, OrganizationLeaveManagement.Employee>(employee);
            employeeDB.AuditFieldsFillingOnCreating();           
            this.DB.Insert(employeeDB);
            this.LeaveService.AssignLeaves(employeeDB.ID, totalSickLeaves, totalCausalLeaves, totalSpecialLeaves);
        }

        public List<Employee> GetAvailableHRs()
        {
            List<Employee> Employees = new List<Employee>();
            var employees = this.DB.Fetch<OrganizationLeaveManagement.Employee>("select * from Employee where Designation = @0", (int)Designation.HR);
            foreach(OrganizationLeaveManagement.Employee employee in employees)
            {
                Employee emp = AutoMapper.mapper.Map<OrganizationLeaveManagement.Employee, Employee>(employee); 
                Employees.Add(emp);
            }
            return Employees;
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> Employees = new List<Employee>();
            var employees = this.DB.Fetch<OrganizationLeaveManagement.Employee>("");
            foreach (OrganizationLeaveManagement.Employee employee in employees)
            {
                Employee emp = AutoMapper.mapper.Map<OrganizationLeaveManagement.Employee, Employee>(employee);
                Employees.Add(emp);
            }
            return Employees;
        }

        public int CheckHREmployeeOrNot(int empID)
        {
           return this.DB.SingleOrDefault<int>("SELECT count(ID) FROM Employee WHERE ID = @0 AND ReportingManagerID != @1", empID,(int)Designation.HR);
        }
    }
}