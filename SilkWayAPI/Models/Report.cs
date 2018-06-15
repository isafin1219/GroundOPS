using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SilkwayAPI.Models
{
    public class Report
    {
        [Key]
        public int Reportid { get; set; }
        public int Flightid { get; set; }
        public Flight Flight { get; set; } 
        public DateTime Date { get; set; }      
        
        internal string _ZFW { get; set; }
        internal string _Loading { get; set; }
        internal string _Fueling { get; set; }
        internal string _Catering { get; set; }
        internal string _OFP { get; set; }
        internal string _WnB { get; set; }
        internal string _Doors { get; set; }
        internal string _Status { get; set; }
        internal string _Delays { get; set; }

        [NotMapped]
        public ZFWItem ZFW
        {
            get { return _ZFW == null ? null : JsonConvert.DeserializeObject<ZFWItem>(_ZFW); }
            set { _ZFW = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public TimeItem Loading
        {
            get { return _Loading == null ? null : JsonConvert.DeserializeObject<TimeItem>(_Loading); }
            set { _Loading = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public TimeItem Fueling
        {
            get { return _Fueling == null ? null : JsonConvert.DeserializeObject<TimeItem>(_Fueling); }
            set { _Fueling = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public TimeItem Catering
        {
            get { return _Catering == null ? null : JsonConvert.DeserializeObject<TimeItem>(_Catering); }
            set { _Catering = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public OFPItem OFP
        {
            get { return _OFP == null ? null : JsonConvert.DeserializeObject<OFPItem>(_OFP); }
            set { _OFP = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public WnBItem WnB
        {
            get { return _WnB == null ? null : JsonConvert.DeserializeObject<WnBItem>(_WnB); }
            set { _WnB = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public DoorsItem Doors
        {
            get { return _Doors == null ? null : JsonConvert.DeserializeObject<DoorsItem>(_Doors); }
            set { _Doors = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public StatusItem Status
        {
            get { return _Status == null ? null : JsonConvert.DeserializeObject<StatusItem>(_Status); }
            set { _Status = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public DelaysList Delays
        {
            get { return _Delays == null ? null : JsonConvert.DeserializeObject<DelaysList>(_Delays); }
            set { _Delays = JsonConvert.SerializeObject(value); }
        }
    }

    public class ZFWItem
    {
        public int Value { get; set; }
        public double CG { get; set; }
    }

    public class TimeItem
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }

    public class OFPItem
    {
        public DateTime Issued { get; set; }
        public DateTime Confirmed { get; set; }
    }

    public class WnBItem
    {
        public DateTime Released { get; set; }
        public DateTime Signed { get; set; }
    }

    public class DoorsItem
    {
        public DateTime Cargo { get; set; }
        public DateTime All { get; set; }
    }

    public class StatusItem
    {
        public DateTime Blocked { get; set; }
        public DateTime Airborne { get; set; }
    }

    public class DelaysList
    {        
        public List<DelaysItem> Delay { get; set; }
    }

    public class DelaysItem
    {
        public string Code { get; set; }
        public string Duration { get; set; }
    }
}
