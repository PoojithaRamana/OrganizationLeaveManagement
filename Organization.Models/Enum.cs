using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Models
{
    public enum Choice
    {
        CreateHREmployee = 1,
        CreateEmployee,
        ApplyLeave,
        ViewTasks,
        CheckLeaveStatus
    }

    public enum Designation
    {       
        SoftwareDeveloper = 1,
        SoftwareTester,
        BI,
        SharePoint,
        Migration,
        HR
    }

    public enum LeaveType
    {
        Sick = 1,
        Causal,
        Special
    }

    public enum LeaveStatus
    {    
        Approve = 1,
        Reject
    }
}
