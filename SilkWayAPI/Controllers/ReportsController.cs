using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var item = _context.ReportList.SingleOrDefault(m => m.Reportid == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/reports
        [HttpPost]
        public IActionResult Create([FromBody] ReportAPIModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var newreport = new Report
            {
                Reportid = item.Reportid,
                Flightid = item.Flightid,
                Date = item.Date,
                ZFW = item.ZFW,
                Loading = item.Loading,
                Fueling = item.Fueling,
                Catering = item.Catering,
                OFP = item.OFP,
                WnB = item.WnB,
                Doors = item.Doors,
                Status = item.Status,
                Delays = item.Delays
            };

            _context.ReportList.Add(newreport);
            _context.SaveChanges();

            return CreatedAtRoute("GetReport", new { id = newreport.Reportid },  newreport);
        }

        // PUT api/reports/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] ReportAPIModel item)
        {
            if (item == null || item.Reportid != id)
            {
                return BadRequest();
            }

            var report = _context.ReportList.SingleOrDefault(m => m.Reportid == id);
            if (report == null)
            {
                return NotFound();
            }

            report.Flightid = item.Flightid;
            report.Date = item.Date;
            report.ZFW = item.ZFW;
            report.Loading = item.Loading;
            report.Fueling = item.Fueling;
            report.Catering = item.Catering;
            report.OFP = item.OFP;
            report.WnB = item.WnB;
            report.Doors = item.Doors;
            report.Status = item.Status;
            report.Delays = item.Delays;

            _context.ReportList.Update(report);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/reports/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var report = _context.ReportList.SingleOrDefault(m => m.Reportid == id);
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
