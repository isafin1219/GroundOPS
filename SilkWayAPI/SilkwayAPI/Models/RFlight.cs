using SilkwayAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkwayAPI.Models
{
    public class RFlight
    {        
        public int Back { get; set; }
        public int Fwd { get; set; } 
        public DateTime? Date { get; set; }
        public string DepartureIata { get; set; }
        public List<int> Uid { get; set; }
    }
}
