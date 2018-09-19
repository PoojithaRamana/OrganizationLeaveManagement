using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Organizations.Models
{
    public class Organization
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public uint TotalSickLeaves { get; set; }
        public uint TotalCausalLeaves { get; set; }
        public uint TotalSpecialLeaves { get; set; }
    }
}
