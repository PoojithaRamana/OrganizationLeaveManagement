using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Organizations.Models;

namespace Organizations.Services
{
    public interface IEmployeeService
    {
        void CreateEmployee(Employee employee, int totalSickLeaves, int totalCausalLeaves, int totalSpecialLeaves);
        List<Employee> GetAvailableHRs();
        List<Employee> GetEmployees();
        int CheckHREmployeeOrNot(int empID);
    }
}
