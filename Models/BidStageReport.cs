using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class BidStageReport
    {
        public int ID { get; set; }

    
        [Display(Name = "Est.Bid")]
        public string EstimatedBid { get; set; }

        [Display(Name = "Actu.DesingHours")]
        public string ActualDesingHours { get; set; }

        [Display(Name = "Est.DesingHours")]
        public string EstimatedDesingHours { get; set; }

        [Display(Name = "Actu.DesingCost")]
        public string ActualDesingCost { get; set; }

        [Display(Name = "Est.DesingCost")]
        public string EstimatedDesingCost { get; set; }

        [Display(Name = "ProjectHours")]
        public string Hours { get; set; }

        [Display(Name = "RemainingAmount")]
        public string Remaining { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

    }
}
