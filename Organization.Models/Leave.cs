using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Organizations.Models
{
    public class Leave
    {
        public LeaveType Type { get; set; }
        public int NumberOfLeaves { get; set; }
        public int EmployeeID { get; set; }
    }
}
