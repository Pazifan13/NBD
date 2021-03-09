using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        [DataType(DataType.Currency)]
        public float? AvgNet { get; set; }
        public float? List { get; set; }
        public int SizeAmount { get; set; }
        public string SizeUnit { get; set; }
        public int Quantity { get; set; }
        public int MaterialID { get; set; }
        public Material Material { get; set; }

        public ICollection<MaterialRequirement> MaterialRequirements { get; set; }
    }
}
