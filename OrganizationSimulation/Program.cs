using System;
using System.Collections.Generic;
using System.Linq;
using Organizations.Models;
using Organizations.Services;
using Autofac;

namespace OrganizationSimulation
{
    public class Program 
    {
        public static IOrganizationService organizationService;
        public static IEmployeeService employeeService;
        public static ILeaveService leaveService;
        public static ITaskService taskService;
        static void AssignNumberOfLeaves(Organization organization)
        {
            foreach (LeaveType leaveType in Enum.GetValues(typeof(LeaveType)))
            {
                Console.WriteLine($"Enter Number of {leaveType} Leaves");
                uint numberOfLeaves = CheckIntegerTypeOrNot();
                if (leaveType == LeaveType.Sick)
                    organization.TotalSickLeaves = numberOfLeaves;
                else if (leaveType == LeaveType.Causal)
                    organization.TotalCausalLeaves= numberOfLeaves;
                else
                    organization.TotalSpecialLeaves = numberOfLeaves;
            }
        }                                                        
        
        static void DisplayChoice()
        {           
            for (int i = 1; i <= Enum.GetValues(typeof(Choice)).Length; i++)
            {
                Console.WriteLine($" {i}.{(Choice)(i)}");
            }
        }

        static void DisplayJobDesignations(int enumLength)
        {
            for (int i = 1; i < enumLength; i++)
            {
                Console.WriteLine($" {i}.{(Designation)(i)}");
            }
        }
        
        static void DisplayLeaveTypes(int enumLength)
        {
            for (int i = 1; i <= enumLength; i++)
            {
                Console.WriteLine($" {i}.{(LeaveType)(i)}");
            }
        }
                   
        static uint CheckIntegerTypeOrNot()
        {
            uint number;
            bool isValid;          
            do
            {
                isValid = UInt32.TryParse(Console.ReadLine(), out number);
                if(!isValid)
                Console.WriteLine("Please Enter Valid Format for given input type");                                            
            } while (!isValid);
            return number;
        }

        static bool IsExistsWithGivenId(int id, List<Employee> employees)
        {
            return employees.Exists(emp => emp.ID == id);           
        }

        static Task GetSelectedTask(List<Task> tasks, int taskid)
        {
            return tasks.SingleOrDefault(task => task.ID == taskid);
        }

        static void DisplayEmployees(List<Employee> hrs)
        {
            foreach (Employee employee in hrs)
                Console.WriteLine($"\tID - {employee.ID}     \t Name - {employee.Name} ");
        }

        static void DisplayTasks(List<Task> tasks)
        {
            foreach(Task task in tasks)
                Console.WriteLine($" Task Number : {task.ID}\n Employee id : {task.EmployeeID} \n Number Of Leaves Applied :  {task.NumberOfLeaves} \n Leave Type : {task.LeaveType} \n Description : {task.Description}\n");
        }

        static void DisplayLeaves(List<Leave> leaves)
        {
            
            foreach (Leave leave in leaves)
            {
                Console.WriteLine($"Number Of {leave.Type} leaves Available : {leave.NumberOfLeaves}");
            }               
        }
        static Organization CreateOrganization()
        {          
            Organization organization = new Organization();
            Console.WriteLine("Enter Name Of Organization");
            organization.Name = Console.ReadLine();
            Console.WriteLine("Enter Place Of Given Organization");
            organization.Location = Console.ReadLine();
            AssignNumberOfLeaves(organization);
            organizationService.CreateOrganization(organization);
            Console.WriteLine("Organization Created Successfully\n");
            return organization;
        }

        static Employee GetEmployeeDetails()
        {
            Employee employee = new Employee();
            Console.WriteLine("Enter Employee Name");
            employee.Name = Console.ReadLine();
            Console.WriteLine("Enter Employee Salary");
            employee.Salary = CheckIntegerTypeOrNot();
            return employee;
        }
        static void CreateHREmployee(int totalSickLeaves, int totalCausalLeaves, int totalSpecialLeaves)
        {
            Employee employee = GetEmployeeDetails();
            employee.Designation = Designation.HR;
            employee.ReportingManagerID = null;
            employeeService.CreateEmployee(employee, totalSickLeaves, totalCausalLeaves, totalSpecialLeaves);           
            Console.WriteLine("HR Employee Created Successfully\n");
        }

        static void CreateEmployee(int totalSickLeaves, int totalCausalLeaves, int totalSpecialLeaves, List<Employee> hrs)
        {
            Employee employee = GetEmployeeDetails();
            int selectedDesignation;
            bool isValidFormat;
            int enumLength = Enum.GetNames(typeof(Designation)).Length;
            do
            {
                Console.WriteLine("Select Employee Designation");
                DisplayJobDesignations(enumLength);
                isValidFormat = Int32.TryParse(Console.ReadLine(), out selectedDesignation);
            } while ((selectedDesignation >= enumLength) || (selectedDesignation <= 0) || !isValidFormat);
            int selectedManagerId;
            employee.Designation = (Designation)(selectedDesignation);
            do
            {
                Console.WriteLine("Select Reporting Manager ID");
                DisplayEmployees(hrs);
                selectedManagerId = (int)CheckIntegerTypeOrNot();
            } while (!IsExistsWithGivenId(selectedManagerId, hrs));
            employee.ReportingManagerID = selectedManagerId;
            employeeService.CreateEmployee(employee, totalSickLeaves, totalCausalLeaves, totalSpecialLeaves);           
            Console.WriteLine("Employee Created Successfully\n");
        }

        static int GetEmployeeId()
        {
            List<Employee> employees = employeeService.GetEmployees();
            int empId;
            do
            {
                DisplayEmployees(employees);
                Console.WriteLine("Enter Employee ID");
                empId = (int)CheckIntegerTypeOrNot();
            } while (!IsExistsWithGivenId(empId, employees));
            return empId;
        }

        static int GetHRId()
        {
            List<Employee> hrs = employeeService.GetAvailableHRs();
            int hrEmpId;
            do
            {
                DisplayEmployees(hrs);
                Console.WriteLine("Enter HR employee Id");
                hrEmpId = (int)CheckIntegerTypeOrNot();
            } while (!IsExistsWithGivenId(hrEmpId, hrs));
            return hrEmpId;
        }

        static void ApplyLeave(int empId)
        {
            Leave leave = new Leave();
            Task task = new Task();
            leave.EmployeeID = task.EmployeeID = empId;
            int slectedLeaveType;
            int enumLength = Enum.GetNames(typeof(LeaveType)).Length;
            do
            {
                Console.WriteLine("Select Leave type");
                DisplayLeaveTypes(enumLength);
                slectedLeaveType = (int)CheckIntegerTypeOrNot();
            } while ((slectedLeaveType > enumLength )|| (slectedLeaveType <= 0));
            leave.Type = task.LeaveType = (LeaveType)(slectedLeaveType);
            int availableLeaves = leaveService.GetAvailableLeavesOfEmployee(leave.EmployeeID, leave.Type);
            if (availableLeaves == 0)
            {                
                Console.WriteLine($"You have {availableLeaves} {leave.Type} leaves \n Their is No chance to apply this leave type");
                ApplyLeave(empId);
            }
            Console.WriteLine($"You have {availableLeaves} {leave.Type} leaves \n Enter number Of  {leave.Type} leaves you want to take");
            uint appliedNumberOfLeaves = CheckIntegerTypeOrNot();
            if (appliedNumberOfLeaves <= availableLeaves)
            {
                leave.NumberOfLeaves = task.NumberOfLeaves = (int)appliedNumberOfLeaves;              
                Console.WriteLine("Enter the Description For leave");
                task.Description = Console.ReadLine();
                if (employeeService.CheckHREmployeeOrNot(empId) > 0)
                {
                    taskService.AddTask(task);
                }
                leaveService.DeductLeaves(leave);
                Console.WriteLine("Your Leave has applied successfully \n");
            }
            else
            {            
                Console.WriteLine("Your leave application is Unsuccessful due to selection of more number of leaves as you Available.");
                ApplyLeave(empId);
            }
        }

        static void ViewTasks(int hrId)
        {
            Task task = new Task();
            Leave leave = new Leave();
            List<Task> tasks = taskService.GetTasksOfGivenManager(hrId);
            if (tasks.Any())
            {
                int selectedTaskNumber;
                do
                {
                    DisplayTasks(tasks);
                    Console.WriteLine("Select Task Number");
                    selectedTaskNumber = (int)CheckIntegerTypeOrNot();
                    task = GetSelectedTask(tasks, selectedTaskNumber);
                } while (task == null);
                int selectedProcess;
                do
                {
                    Console.WriteLine("Select Process \n \t 1.Approve \n \t 2.Reject");
                    selectedProcess = (int)CheckIntegerTypeOrNot();
                } while ((selectedProcess) != (int)(LeaveStatus.Approve) || (selectedProcess) != (int)(LeaveStatus.Approve));
                if ((selectedProcess) == (int)(LeaveStatus.Reject))
                {
                    leave.NumberOfLeaves = task.NumberOfLeaves;
                    leave.EmployeeID = task.EmployeeID;
                    leave.Type = task.LeaveType;
                    leaveService.ExtendLeaves(leave);
                }
                taskService.RemoveTask(task.ID);
                
                Console.WriteLine("Task has completed \n");
            }
            else
            {
                Console.WriteLine("No Tasks are available \n");
            }
        }
        static void Main(string[] args)
        {
            var container = DependencyInjection.GetContainer();
            var scope = container.BeginLifetimeScope();
            organizationService = scope.Resolve<IOrganizationService>();
            leaveService = scope.Resolve<ILeaveService>();
            taskService = scope.Resolve<ITaskService>();
            employeeService = scope.Resolve<IEmployeeService>();

            try
                {             
                Organization organization = organizationService.GetOrganization();
                if (organization == null)
                {
                    organization = CreateOrganization();
                }
               
                while (true)
                {
                    Console.WriteLine("Select Choice: ");
                    DisplayChoice();
                    uint selectedChoice = CheckIntegerTypeOrNot();
                    Console.Clear();
                    switch (selectedChoice)
                    {
                        case ((int)Choice.CreateHREmployee):
                            CreateHREmployee((int)organization.TotalSickLeaves, (int)organization.TotalCausalLeaves, (int)organization.TotalSpecialLeaves);
                            break;

                        case ((int)Choice.CreateEmployee):
                            List<Employee> hrs = employeeService.GetAvailableHRs();
                            if (hrs.Any())
                            {
                                CreateEmployee((int)organization.TotalSickLeaves, (int)organization.TotalCausalLeaves, (int)organization.TotalSpecialLeaves, hrs);
                            }
                            else
                            {
                                Console.WriteLine("HR Employee should created before creating Technical Employee...");
                            }
                            break;

                        case ((int)Choice.ApplyLeave):
                            int empId = GetEmployeeId();
                            ApplyLeave(empId);                                           
                            break;

                        case ((int)Choice.ViewTasks):
                            int hrId = GetHRId();
                            ViewTasks(hrId);
                            break;

                        case ((int)Choice.CheckLeaveStatus):
                            int selectedEmployeeId = GetEmployeeId();
                            List<Leave> leaves = leaveService.GetLeavesOfEmployee(selectedEmployeeId);
                            DisplayLeaves(leaves);                
                            break;

                        default:
                            Console.WriteLine("Please select valid option ");
                            break;
                    }
                }                
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Message : " + e);
                Console.ReadKey();
            }
        }
    }
}