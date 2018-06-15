using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SilkwayAPI.Data;
using SilkwayAPI.Models;

namespace SilkwayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/reports")]
    //[Authorize]
    public class ReportsController : Controller
    {
        private readonly SilkwayAPIContext _context;

        public ReportsController(SilkwayAPIContext context)
        {
            _context = context;
        }

        // GET api/reports
        [HttpGet]
        public IEnumerable<Report> Get()
        {
            return _context.ReportList.ToArray();
        }

        // GET api/reports/5
        [HttpGet("{id}", Name = "GetReport")]
        public IActionResult GetById(long id)
        {
            var item = _context.ReportList.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/reports
        [HttpPost]
        public string Create([FromBody] ReportAPIModel item)
        {
            if (item == null)
            {
                return "NULL Object";//BadRequest();
            }

            var newreport = new Report
            {
                Reportid = item.Reportid,
                Flightid = item.Flightid,
                Date = item.Date,
                ZFW = JsonConvert.DeserializeObject<ZFWItem>(item.ZFW),
                Loading = JsonConvert.DeserializeObject<TimeItem>(item.Loading),
                Fueling = JsonConvert.DeserializeObject<TimeItem>(item.Fueling),
                Catering = JsonConvert.DeserializeObject<TimeItem>(item.Catering),
                OFP = JsonConvert.DeserializeObject<OFPItem>(item.OFP),
                WnB = JsonConvert.DeserializeObject<WnBItem>(item.WnB),
                Doors = JsonConvert.DeserializeObject<DoorsItem>(item.Doors),
                Status = JsonConvert.DeserializeObject<StatusItem>(item.Status),
                Delays = JsonConvert.DeserializeObject<DelaysList>(item.Delays)
            };

            //_context.ReportList.Add(newreport);
            //_context.SaveChanges();

            //return CreatedAtRoute("GetReport", new { id = newreport.Reportid },  newreport);
            return newreport.ToString();
        }

        // PUT api/reports/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Report item)
        {
            if (item == null || item.Reportid != id)
            {
                return BadRequest();
            }

            var report = _context.ReportList.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            report = item;

            _context.ReportList.Update(report);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/reports/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var report = _context.ReportList.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.ReportList.Remove(report);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
