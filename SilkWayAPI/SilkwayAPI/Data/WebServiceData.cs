using SilkwayAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SilkwayAPI.Data
{
    public class WebServiceData
    {
        private readonly SilkwayAPIContext _context;
        private readonly FlightsResponse _data;
        private readonly NotificationList _changesNotes;

        public WebServiceData(SilkwayAPIContext context)
        {
            _context = context;
            _data = new BlueoneData().FlightInfo;
            _changesNotes = new NotificationList { updates = new List<NotifyItem>(), newitems = new List<NotifyItem>() };
        }

        public NotificationList ReadWebservice() {
            DateTime TempDate;
            foreach (var item in _data.Flights)
            {
                var flightToUpdate = _context.FlightList.SingleOrDefault(f => f.Uid == Int32.Parse(item.Uid));
                if ((flightToUpdate != null))
                {
                    if (flightToUpdate.Modified_at < (DateTime.TryParse(item.Modified_at, out TempDate) ? TempDate : (DateTime?)null))
                    {
                        NotifyItem updateItem = new NotifyItem(flightToUpdate.Uid) { fields = new List<string>() };
                        if (flightToUpdate.Status != item.Status)
                        {
                            flightToUpdate.Status = item.Status;
                            updateItem.fields.Add("Status");
                        }
                        if (flightToUpdate.Opsstatus != item.Opsstatus)
                        {
                            flightToUpdate.Opsstatus = item.Opsstatus;
                            updateItem.fields.Add("Opsstatus");
                        }
                        if (flightToUpdate.Call_sign_number != item.Call_sign_number || flightToUpdate.Call_sign_number != item.Number)
                        {
                            flightToUpdate.Call_sign_number = item.Call_sign_number.Length < 4 && item.Number.Length >= 4 ? item.Number : item.Call_sign_number;
                            updateItem.fields.Add("Call_sign_number");
                        }
                        if (flightToUpdate.Call_sign_code != item.Call_sign_code || flightToUpdate.Call_sign_code != item.Carrier_code)
                        {
                            flightToUpdate.Call_sign_code = item.Call_sign_code != item.Carrier_code ? item.Carrier_code : item.Call_sign_code;
                            updateItem.fields.Add("Call_sign_code");
                        }
                        if (flightToUpdate.Aircraft_reg != item.Aircraft_reg)
                        {
                            flightToUpdate.Aircraft_reg = item.Aircraft_reg;
                            updateItem.fields.Add("Aircraft_reg");
                        }
                        if (flightToUpdate.Aircraft_type != item.Aircraft_type)
                        {
                            flightToUpdate.Aircraft_type = item.Aircraft_type;
                            updateItem.fields.Add("Aircraft_type");
                        }

                        flightToUpdate.Std = DateTime.TryParse(item.Std, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Sta = DateTime.TryParse(item.Sta, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Est_blocktime = DateTime.TryParse(item.Est_blocktime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Est_takeofftime = DateTime.TryParse(item.Est_takeofftime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Est_touchdowntime = DateTime.TryParse(item.Est_touchdowntime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Est_blockintime = DateTime.TryParse(item.Est_blockintime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Mvt_blocktime = DateTime.TryParse(item.Mvt_blocktime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Mvt_takeofftime = DateTime.TryParse(item.Mvt_takeofftime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Mvt_touchdowntime = DateTime.TryParse(item.Mvt_touchdowntime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Mvt_blockintime = DateTime.TryParse(item.Mvt_blockintime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Acars_est_blockintime = DateTime.TryParse(item.Acars_est_blockintime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Acars_blocktime = DateTime.TryParse(item.Acars_blocktime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Acars_takeofftime = DateTime.TryParse(item.Acars_takeofftime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Acars_touchdowntime = DateTime.TryParse(item.Acars_touchdowntime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Acars_blockintime = DateTime.TryParse(item.Acars_blockintime, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Revised_departure = DateTime.TryParse(item.Revised_departure, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Revised_arrival = DateTime.TryParse(item.Revised_arrival, out TempDate) ? TempDate : (DateTime?)null;
                        flightToUpdate.Remarks = item.Remarks;
                        flightToUpdate.Apt_dep = item.Apt_dep;
                        flightToUpdate.Apt_arr_planned = item.Apt_arr_planned;
                        flightToUpdate.Apt_arr_actual = item.Apt_arr_actual;
                        flightToUpdate.Fuel = item.Fuel;
                        flightToUpdate.Crew_compo = item.Crew_compo;
                        flightToUpdate.Delays = item.Delays;
                        flightToUpdate.Modified_at = DateTime.TryParse(item.Modified_at, out TempDate) ? TempDate : (DateTime?)null;
                        _context.FlightList.Update(flightToUpdate);
                        _changesNotes.updates.Add(updateItem);                        
                    }
                }
                else
                {
                    Flight NewFlight = new Flight
                    {
                        Uid = Int32.Parse(item.Uid),
                        Status = item.Status,
                        Opsstatus = item.Opsstatus,
                        Call_sign_number = item.Call_sign_number,
                        Call_sign_code = item.Call_sign_code,
                        Aircraft_reg = item.Aircraft_reg,
                        Aircraft_type = item.Aircraft_type,
                        Std = (DateTime.TryParse(item.Std, out TempDate) ? TempDate : (DateTime?)null),
                        Sta = (DateTime.TryParse(item.Sta, out TempDate) ? TempDate : (DateTime?)null),
                        Est_blocktime = (DateTime.TryParse(item.Est_blocktime, out TempDate) ? TempDate : (DateTime?)null),
                        Est_takeofftime = (DateTime.TryParse(item.Est_takeofftime, out TempDate) ? TempDate : (DateTime?)null),
                        Est_touchdowntime = (DateTime.TryParse(item.Est_touchdowntime, out TempDate) ? TempDate : (DateTime?)null),
                        Est_blockintime = (DateTime.TryParse(item.Est_blockintime, out TempDate) ? TempDate : (DateTime?)null),
                        Mvt_blocktime = (DateTime.TryParse(item.Mvt_blocktime, out TempDate) ? TempDate : (DateTime?)null),
                        Mvt_takeofftime = (DateTime.TryParse(item.Mvt_takeofftime, out TempDate) ? TempDate : (DateTime?)null),
                        Mvt_touchdowntime = (DateTime.TryParse(item.Mvt_touchdowntime, out TempDate) ? TempDate : (DateTime?)null),
                        Mvt_blockintime = (DateTime.TryParse(item.Mvt_blockintime, out TempDate) ? TempDate : (DateTime?)null),
                        Acars_est_blockintime = (DateTime.TryParse(item.Acars_est_blockintime, out TempDate) ? TempDate : (DateTime?)null),
                        Acars_blocktime = (DateTime.TryParse(item.Acars_blocktime, out TempDate) ? TempDate : (DateTime?)null),
                        Acars_takeofftime = (DateTime.TryParse(item.Acars_takeofftime, out TempDate) ? TempDate : (DateTime?)null),
                        Acars_touchdowntime = (DateTime.TryParse(item.Acars_touchdowntime, out TempDate) ? TempDate : (DateTime?)null),
                        Acars_blockintime = (DateTime.TryParse(item.Acars_blockintime, out TempDate) ? TempDate : (DateTime?)null),
                        Revised_departure = (DateTime.TryParse(item.Revised_departure, out TempDate) ? TempDate : (DateTime?)null),
                        Revised_arrival = (DateTime.TryParse(item.Revised_arrival, out TempDate) ? TempDate : (DateTime?)null),
                        Remarks = item.Remarks,
                        Apt_dep = item.Apt_dep,
                        Apt_arr_planned = item.Apt_arr_planned,
                        Apt_arr_actual = item.Apt_arr_actual,
                        Fuel = item.Fuel,
                        Crew_compo = item.Crew_compo,
                        Delays = item.Delays,
                        Modified_at = (DateTime.TryParse(item.Modified_at, out TempDate) ? TempDate : (DateTime?)null)
                    };
                    _context.FlightList.Add(NewFlight);
                    _changesNotes.newitems.Add(new NotifyItem(NewFlight.Uid) { fields = new List<string>() });
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return _changesNotes;
        }
    }

    public class NotificationList
    {
        public List<NotifyItem> updates;
        public List<NotifyItem> newitems;
    }

    public class NotifyItem
    {
        public int uid;
        public List<string> fields;

        public NotifyItem(int id)
        {
            uid = id;
        }
    }
}
