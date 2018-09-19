using System;
using System.Collections.Generic;
using Organizations.Models;

namespace Organizations.Services
{
    public interface ITaskService
    {
        void AddTask(Task task);
        List<Task> GetTasksOfGivenManager(int hrId);
        void RemoveTask(int taskId);
    }
}
