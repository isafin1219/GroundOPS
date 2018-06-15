using System;
namespace SilkwayAPI.Models
{
    public class ReportAPIModel
    {
        public int Reportid { get; set; }
        public int Flightid { get; set; }
        public DateTime Date { get; set; }

        public string ZFW { get; set; }
        public string Loading { get; set; }
        public string Fueling { get; set; }
        public string Catering { get; set; }
        public string OFP { get; set; }
        public string WnB { get; set; }
        public string Doors { get; set; }
        public string Status { get; set; }
        public string Delays { get; set; }
    }
}
