using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace NBD.Models
{
    public class Project : Auditable
    {
       public Project()
        {
            this.ProjectLabours = new HashSet<ProjectLabour>();
            this.ProjectMaterials = new HashSet<ProjectMaterial>();
            this.Teams = new HashSet<Team>();
        }

        public int ID { get; set; }
        
        [Required(ErrorMessage ="Project Name is required.")]
         public string Name { get; set; }

        [Display(Name = "Project Site")]
        public string ProjSite { get; set; }

        [Display(Name = "Project Bid Date")]
        [Required(ErrorMessage = "Estimated Start Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProjBidDate { get; set; }

        [Display(Name = "Estimated Start Date")]
        [Required(ErrorMessage = "Estimated Start Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstStartDate { get; set; }

        [Display(Name = "Estimated End Date")]
        [Required(ErrorMessage = "Estimated End Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EstEndDate { get; set; }
       
        [Display(Name = " Actual Start Date")]
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Actual End Date")]
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Actual Project Cost")]
        
        [DataType(DataType.Currency)]
        public float? ActAmount { get; set; }

        [Display(Name = "Estimated Project Cost")]
        [Required(ErrorMessage = "Bid Amount is required.")]
        [DataType(DataType.Currency)]
        public float? EstAmount { get; set; }

        [Display(Name = "Client-Approved")]
        [Required(ErrorMessage = "Please verify if client approved bid.")]
        public bool ClientApproval { get; set; }

        [Display(Name = "Administration-Approved")]
        [Required(ErrorMessage = "Please verify if administration approved bid.")]
        public bool AdminApproval { get; set; }

        

        [Display(Name="Project Phase")]
        public string ProjCurrentPhase { get; set; }

        [Required(ErrorMessage = "Please choose a client.")]
        [Display(Name ="Client")]
        public int? ClientID { get; set; }

        [Display(Name = "Is Project flagged?")]
        public bool ProjIsFlagged { get; set; }

        public Client Client { get; set; }

        public int? TeamID { get; set; }

        public Team Team { get; set; }

        public ICollection<Team> Teams { get; set; }

        [Display(Name ="Labour Requirements")]
        public ICollection<ProjectLabour> ProjectLabours { get; set; }

        [Display(Name ="Material Requirements")]
        public ICollection<ProjectMaterial>ProjectMaterials { get; set; }

        public ICollection<ProductionPlan> ProductionPlans { get; set; }

        public ICollection<WorkerReport> WorkerReports { get; set; }

        public ICollection<MaterialReport> MaterialReports { get; set; }

        public ICollection<ProductionStageReport> ProductionStageReports { get; set; }

        public ICollection<BidStageReport> BidStageReports { get; set; }

        public ICollection<DesignReport> DesignReports { get; set; }



    }
}
