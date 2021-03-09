using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class LabourSummary
    {
        public int ID { get; set; }

        [Display(Name = "Hours Worked")]
        public int Hours { get; set; }

        public int ProjectID {get;set;}
        public Project Project { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        
           }
}
