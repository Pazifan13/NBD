using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class DesignReport
    {
        public int ID { get; set; }
        public int Hour { get; set; }

        public DateTime? Date { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

        public int TaskID { get; set; }

        public Task Task { get; set; }

        public int StageID { get; set; }

        public Stage Stage { get; set; }
    }
}
