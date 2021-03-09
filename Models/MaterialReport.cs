using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class MaterialReport
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int Costs { get; set; }

        public int ExtCosts
        {
            get
            {
                return Quantity * Costs;

            }
        }

        public DateTime? Date { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

        public int MaterialID { get; set; }

        public Material Material  { get; set; }
    }
}
