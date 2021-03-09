using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int StdTimeAmnt { get; set; }
        public string stdTimeUnit { get; set; }

        public ICollection<LabourRequirement> LabourRequirements { get; set; }

        public ICollection<WorkerReport> WorkerReports { get; set; }

        public ICollection<DesignReport> DesignReports { get; set; }
    }
}
