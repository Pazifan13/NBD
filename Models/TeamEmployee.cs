using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class TeamEmployee
    {
        public int TeamID { get; set; }
        public Team Team { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
