using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Bike
    {
        [Key]
        public int Id { get; set; }
        public string BikeName { get; set; }
        public string BikeType { get; set; }
        public double BikePrice { get; set; }
    }
}
