using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class Stage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<DesignReport> DesignReports { get; set; }

    }
}
