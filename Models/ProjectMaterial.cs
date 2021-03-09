using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProjectMaterial
    {
        public int MaterialReqID { get; set; }
        public MaterialRequirement MaterialRequirement { get; set; }

        public int ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
