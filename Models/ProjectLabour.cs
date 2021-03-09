using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProjectLabour
    {
        public int? ProjectID { get; set; }
        public Project Project { get; set; }

        public int? LabourReqID { get; set; }
        public LabourRequirement LabourRequirement {get;set;}
    }
}
