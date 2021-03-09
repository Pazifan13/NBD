using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProdPlanLabour
    {
        public int ProdPlanID { get; set; }
        public ProductionPlan ProductionPlan { get; set; }

        public int LabourReqID {get;set;}
        public LabourRequirement LabourRequirement { get; set; }
    }
}
