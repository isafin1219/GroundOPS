using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Newtonsoft.Json;
using SilkwayAPI.Data;
using SilkwayAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkwayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Flights")]
    public class FlightsController : Controller
    {
        private readonly SilkwayAPIContext _context;

        public FlightsController(SilkwayAPIContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> GetFlight([FromHeader] RFlight request)
        {
            if ((request.Back > 0 || request.Fwd > 0) && request.Date == null)
            {
                return from flight in _context.FlightList
                       where flight.Est_blocktime.Value > DateTime.UtcNow.AddHours(-request.Back) && flight.Est_blocktime < DateTime.UtcNow.AddHours(request.Fwd)
                       select flight;
            }
            else if (request.Date != null && (request.Back == 0 && request.Fwd == 0)) {
                return from flight in _context.FlightList
                       where flight.Est_blocktime > request.Date && flight.Est_blocktime < request.Date.Value.AddDays(1)
                       select flight;
            }
            else
            {
                return _context.FlightList.Where(f => f.Est_blocktime > DateTime.UtcNow.AddDays(-2));
            }            
        }

        // GET: api/Flights
        [HttpGet("station")]
        public IEnumerable<Flight> GetFlightbyStation([FromHeader] RFlight request)
        {
            if ((request.Back > 0 || request.Fwd > 0) && request.DepartureIata != null)
            {
                return from flight in _context.FlightList
                       where (flight.Apt_dep.Iata == request.DepartureIata || flight.Apt_arr_actual.Iata == request.DepartureIata) && 
                       (flight.Est_blocktime > DateTime.UtcNow.AddHours(-request.Back) && flight.Est_blocktime < DateTime.UtcNow.AddHours(request.Fwd))
                       orderby flight.Est_blocktime
                       select flight;
            }
            else
            {
                return from flight in _context.FlightList
                       where (flight.Apt_dep.Iata == request.DepartureIata || flight.Apt_arr_actual.Iata == request.DepartureIata) && flight.Est_blocktime > DateTime.UtcNow.AddDays(-1)
                       orderby flight.Est_blocktime
                       select flight;
            }            
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = await _context.FlightList.SingleOrDefaultAsync(m => m.Uid == id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        // Post: api/Flights/Uids
        [HttpPost("Uids")]
        public IEnumerable<Flight> QFlights([FromBody] RFlight Request)
        {
            var QFlightsList = new List<Flight>();
            Request.Uid.ForEach((int uid) => {
                var flight = _context.FlightList.SingleOrDefault(m => m.Uid == uid);
                Console.WriteLine(uid);
                QFlightsList.Add(flight);
            });

            return QFlightsList;
        }

        [HttpGet("/testdate")]
        public string Testdate()
        {
            //RecurringJob.AddOrUpdate("BlueoneData", () => RecurrentJob(), "*/1 * * * *");
            return DateTime.UtcNow.AddDays(-1).ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }

        [HttpGet("/addcronjob")]
        public string AddCronJob()
        {
            //RecurringJob.AddOrUpdate("BlueoneData", () => RecurrentJob(), "*/1 * * * *");
            RecurringJob.AddOrUpdate("BlueoneData", () => RecurrentJob(), "0 */1 * * *");
            return "Cron Job added";
        }

        [HttpGet("/removecronjobs")]
        public string RemoveCronJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            return "Cron Jobs removed";
        }

        [HttpGet("/updatedb")]
        public string UpdateDb()
        {
            RecurrentJob();
            return "DB updated";
        }

        public void RecurrentJob()
        {
            Console.WriteLine(JsonConvert.SerializeObject(new WebServiceData(_context).ReadWebservice()));
        }
        
        [HttpPost("/testpost")]
        public string TestPost([FromBody] RFlight Request)
        {
            string test = "";
            Request.Uid.ForEach((int a) => { test += a.ToString(); });
            return test;
        }
    }
}