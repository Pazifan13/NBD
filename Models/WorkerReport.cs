using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class WorkerReport
    {
        public int ID { get; set; }
        public int Hours { get; set; }
        public int Costs { get; set; }

        public int ExtCosts
        {
            get
            {
                return Hours*Costs;

            }
        }

        public DateTime? Date { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

        public int TaskID { get; set; }

        public Task Task { get; set; }

    }
}
