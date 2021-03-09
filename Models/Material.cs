using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class Material
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public ICollection<Inventory> Inventories { get; set; }

        public ICollection<MaterialReport> MaterialReports { get; set; }
    }
}
