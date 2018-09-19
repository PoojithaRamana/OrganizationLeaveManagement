using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Organizations.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public uint Salary { get; set; }
        public int? ReportingManagerID { get; set; }
        public Designation Designation { get; set; }    
    }
}
