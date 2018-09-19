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
    public interface ILeaveService
    {
        void AssignLeaves(int empId, int numOfSickLeaves, int numOfCasualLeaves, int numOfSpecialLeaves);
        void AddLeaves(Leave leave);
        int GetAvailableLeavesOfEmployee(int empId, LeaveType leaveType);
        void DeductLeaves(Leave leave);
        void ExtendLeaves(Leave leave);
        List<Leave> GetLeavesOfEmployee(int empId);
    }
}
