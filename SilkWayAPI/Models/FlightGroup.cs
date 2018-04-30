using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkwayAPI.Models
{
    public class FlightGroup
    {
        public string GroupID { get; set; }
        public List<Flight> Flights { get; set; }
    }
}