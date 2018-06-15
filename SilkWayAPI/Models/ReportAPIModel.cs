using System;
namespace SilkwayAPI.Models
{
    public class ReportAPIModel
    {
        public int Reportid { get; set; }
        public int Flightid { get; set; }
        public DateTime Date { get; set; }

        public ZFWItem ZFW { get; set; }
        public TimeItem Loading { get; set; }
        public TimeItem Fueling { get; set; }
        public TimeItem Catering { get; set; }
        public OFPItem OFP { get; set; }
        public WnBItem WnB { get; set; }
        public DoorsItem Doors { get; set; }
        public StatusItem Status { get; set; }
        public DelaysList Delays { get; set; }
    }
}
