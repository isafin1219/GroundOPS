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
            var item = _context.ReportList.Find(id);
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

            return CreatedAtRoute("GetReport", new { id = item.Reportid }, item);
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
