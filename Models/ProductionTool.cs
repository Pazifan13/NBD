using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class ProductionTool
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public DateTime? EarliestDelivery { get; set; }
        public DateTime? LatestDelivery { get; set; }

        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }
        public int ToolID { get; set; }
        public virtual Tool Tool { get; set; }
    }

}
