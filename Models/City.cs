using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
