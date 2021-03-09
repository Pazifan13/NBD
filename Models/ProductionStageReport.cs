using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProductionStageReport
    {
        public int Id { get; set; }

       

        [Display(Name = "BidAmount")]
        public string Bid { get; set; }

        [Display(Name = "Est.ProdPlanAmount")]
        public string EstProdPlan { get; set; }

        [Display(Name = "TotalCost")]
        public string TotalCosttoDate { get; set; }

        [Display(Name = "ActualMtl")]
        public string ActualMtl { get; set; }

        [Display(Name = "Est.Mtl")]
        public string EstimatedDesingCost { get; set; }

        [Display(Name = "ActualLaborProdCost")]
        public string ActuLaborPro { get; set; }

        [Display(Name = "Est.LaborProdCost")]
        public string EstLaborProdCost { get; set; }

        [Display(Name = "ActualLaborDesingCost")]
        public string ActuLaborDesingCost { get; set; }

        [Display(Name = "Est.LaborDesingCost")]
        public string EstLaborDesingCost { get; set; }

        public int ProductionPlanID { get; set; }

        public ProductionPlan ProductionPlan{ get; set; }
        public int ProjectID { get; set; }
        public Project Project { get; set; }


    }
}
