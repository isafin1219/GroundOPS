using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SilkwayAPI.Models
{
    public class Flight
    {
        [Key]
        public int Flightid { get; set; }
        public int Uid { get; set; }
        public string Status { get; set; }
        public string Opsstatus { get; set; }
        public string Call_sign_number { get; set; }
        public string Call_sign_code { get; set; }
        public string Aircraft_reg { get; set; }
        public string Aircraft_type { get; set; }
        public DateTime? Std { get; set; }
        public DateTime? Sta { get; set; }
        public DateTime? Est_blocktime { get; set; }
        public DateTime? Est_takeofftime { get; set; }
        public DateTime? Est_touchdowntime { get; set; }
        public DateTime? Est_blockintime { get; set; }
        public DateTime? Mvt_blocktime { get; set; }
        public DateTime? Mvt_takeofftime { get; set; }
        public DateTime? Mvt_touchdowntime { get; set; }
        public DateTime? Mvt_blockintime { get; set; }
        public DateTime? Acars_est_blockintime { get; set; }
        public DateTime? Acars_blocktime { get; set; }
        public DateTime? Acars_takeofftime { get; set; }
        public DateTime? Acars_touchdowntime { get; set; }
        public DateTime? Acars_blockintime { get; set; }
        public DateTime? Revised_departure { get; set; }
        public DateTime? Revised_arrival { get; set; }
        public string Remarks { get; set; }


        internal string _Apt_dep { get; set; }
        internal string _Apt_arr_planned { get; set; }
        internal string _Apt_arr_actual { get; set; }
        internal string _Fuel { get; set; }
        internal string _Crew_compo { get; set; }
        internal string _Delays { get; set; }

        [NotMapped]
        public AirportData Apt_dep
        {
            get { return _Apt_dep == null ? null : JsonConvert.DeserializeObject<AirportData>(_Apt_dep); }
            set { _Apt_dep = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public AirportData Apt_arr_planned
        {
            get { return _Apt_arr_planned == null ? null : JsonConvert.DeserializeObject<AirportData>(_Apt_arr_planned); }
            set { _Apt_arr_planned = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public AirportData Apt_arr_actual
        {
            get { return _Apt_arr_actual == null ? null : JsonConvert.DeserializeObject<AirportData>(_Apt_arr_actual); }
            set { _Apt_arr_actual = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public FuelInfo Fuel
        {
            get { return _Fuel == null ? null : JsonConvert.DeserializeObject<FuelInfo>(_Fuel); }
            set { _Fuel = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public CrewInfo Crew_compo
        {
            get { return _Crew_compo == null ? null : JsonConvert.DeserializeObject<CrewInfo>(_Crew_compo); }
            set { _Crew_compo = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public DelayList Delays
        {
            get { return _Delays == null ? null : JsonConvert.DeserializeObject<DelayList>(_Delays); }
            set { _Delays = JsonConvert.SerializeObject(value); }
        }

        public DateTime? Modified_at { get; set; }        
    }    
}
