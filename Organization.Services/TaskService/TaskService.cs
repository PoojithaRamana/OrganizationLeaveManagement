using System;
using System.Collections.Generic;
using Organizations.Models;
using PetaPoco;

namespace Organizations.Services
{
    public class TaskService : ITaskService
    {
        public Database DB { get; set; }
        public TaskService(Database db)
        {
            this.DB = db;
        }
        public void AddTask(Task task)
        {
            OrganizationLeaveManagement.Task taskDB = AutoMapper.mapper.Map<Task, OrganizationLeaveManagement.Task>(task);
            taskDB.AuditFieldsFillingOnCreating();
            this.DB.Insert(taskDB);                             
        }

        public List<Task> GetTasksOfGivenManager(int hrId)
        {
            List<Task> Tasks = new List<Task>();
            var tasks = this.DB.Fetch<OrganizationLeaveManagement.Task>("select Task.ID, Task.Description, Task.NumberOfLeaves, Task.EmployeeID, Task.LeaveType from Task inner join Employee ON Task.EmployeeID = Employee.ID where Employee.ReportingManagerID = @0", hrId);
            foreach(OrganizationLeaveManagement.Task task in tasks)
            {
                Task Task = AutoMapper.mapper.Map<OrganizationLeaveManagement.Task, Task>(task);
                Tasks.Add(Task);
            }
            return Tasks;
        }
      
        public void RemoveTask(int taskId)
        {
            this.DB.Delete<OrganizationLeaveManagement.Task>("WHERE ID = @0", taskId);
        }
    }
}