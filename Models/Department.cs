using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;


namespace NBD.Models
{
    public class Department
    {
        public int ID { get; set; }

        public string Description { get; set; }
        public float? Price { get; set; }
        public float? Cost { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<LabourSummary> LabourSummaries { get; set; }

    }
}
