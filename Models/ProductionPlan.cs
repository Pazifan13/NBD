using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProductionPlan
    {
        public ProductionPlan()
        {
            this.ProdPlanLabours = new HashSet<ProdPlanLabour>();
            this.ProdPlanMaterials = new HashSet<ProdPlanMaterial>();
        }

        public int ID { get; set; }

       
        [Display(Name = "Project")]
        public int ProjectID { get; set; }
        
        
        [Display(Name = "Staff")]
        public int TeamID { get; set; }
        
        public Project Project { get; set; }
        public Team Team { get; set; }

        [Display(Name ="Material Requirements")]
        public ICollection<ProdPlanMaterial> ProdPlanMaterials { get; set; }

        [Display(Name ="Labour Requirements")]
        public ICollection<ProdPlanLabour> ProdPlanLabours { get; set; }

        public ICollection<ProductionStageReport> ProductionStageReports { get; set; }


    }
}
