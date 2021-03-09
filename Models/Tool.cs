using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class Tool
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public ICollection<Project> Projects { get; set; }

    }
}
