using System;
using System.Collections.Generic;
using Organizations.Models;
using PetaPoco;

namespace Organizations.Services
{  
    public class LeaveService : ILeaveService
    {
        public Database DB { get; set; }
        public LeaveService(Database db)
        {
            this.DB = db;
        }
        public void AssignLeaves(int empId, int numOfSickLeaves,int numOfCasualLeaves,int numOfSpecialLeaves)
        {
            Leave leave = new Leave();
            leave.EmployeeID = empId;
            foreach (LeaveType leaveType in Enum.GetValues(typeof(LeaveType)))
            {
                leave.Type = leaveType;
                if (leaveType == LeaveType.Sick)
                    leave.NumberOfLeaves = numOfSickLeaves;
                else if (leaveType == LeaveType.Causal)
                    leave.NumberOfLeaves = numOfCasualLeaves;
                else
                    leave.NumberOfLeaves = numOfSpecialLeaves;
                this.AddLeaves(leave);
            }
        }
        public void AddLeaves(Leave leave)
        {
            OrganizationLeaveManagement.Leave leaveDB = AutoMapper.mapper.Map<Leave, OrganizationLeaveManagement.Leave>(leave);
            leaveDB.AuditFieldsFillingOnCreating();
            this.DB.Insert(leaveDB);     
        }

        public int GetAvailableLeavesOfEmployee(int empId, LeaveType leaveType)
        {        
            return this.DB.SingleOrDefault<int>("SELECT NumberOfLeaves FROM Leave WHERE  EmployeeID = @0 AND  Type = @1", empId, (int)leaveType);
        }

        public void DeductLeaves(Leave leave)
        {
           this.DB.Update<OrganizationLeaveManagement.Leave>("SET NumberOfLeaves= NumberOfLeaves -@0,ModifiedBy = @1, ModifiedDate = @2 WHERE EmployeeID=@3 AND Type=@4", leave.NumberOfLeaves, "Poojitha", DateTime.Now, leave.EmployeeID, (int)(leave.Type));
        }

        public void ExtendLeaves(Leave leave)
        {
            this.DB.Update<OrganizationLeaveManagement.Leave>("SET NumberOfLeaves= NumberOfLeaves +@0, ModifiedBy = @1, ModifiedDate = @2 WHERE EmployeeID=@3 AND Type=@4", leave.NumberOfLeaves, "Poojitha", DateTime.Now, leave.EmployeeID, (int)(leave.Type));
        }

        public List<Leave> GetLeavesOfEmployee(int empId)
        {
            List<Leave> Leaves = new List<Leave>();
            var leaves = this.DB.Fetch<OrganizationLeaveManagement.Leave>("select * from Leave where [EmployeeID] = @0", empId);
            foreach (OrganizationLeaveManagement.Leave Leave in leaves)
            {
                Leave leave = AutoMapper.mapper.Map<OrganizationLeaveManagement.Leave, Leave>(Leave);
                Leaves.Add(leave);
            }
            return Leaves;
        }
    }
}