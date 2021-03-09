using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProdPlanMaterial
    {
        public int ProdPlanID { get; set; }
        public ProductionPlan ProductionPlan { get; set; }

        public int MaterialReqID { get; set; }
        public MaterialRequirement MaterialRequirement { get; set; }
    }
}
