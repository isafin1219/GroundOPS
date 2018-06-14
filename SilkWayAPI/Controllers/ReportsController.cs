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
    [Authorize]
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
            //return new string[] { "value1", "value2" };
        }

        // GET api/reports/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/reports
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/reports/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reports/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
