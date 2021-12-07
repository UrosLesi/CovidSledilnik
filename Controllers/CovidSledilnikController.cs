using CovidSledilnik.Models;
using CovidSledilnik.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CovidSledilnik.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CovidSledilnikController : ControllerBase
    {

        private ICovidSledilnikService _covidSledilnikService;

        public CovidSledilnikController(ICovidSledilnikService covidSledilnikService)
        {
            _covidSledilnikService = covidSledilnikService;
        }
        /// <summary>
        /// On this endpoint User gets data from specific time frame and based on region that he specified.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet("region/cases")]
        public IActionResult GetFromToDate([BindRequired, FromQuery(Name = "Regions: LJ, CE, KR, NM, KK, KP, MB, MS, NG, PO, SG, ZA")] string region,
            [BindRequired, FromQuery] DateTime fromDate,
            [BindRequired, FromQuery] DateTime toDate)
        {
            var regUpper = region.ToUpperInvariant();
            string[] regions = { "LJ", "CE", "KR", "NM", "KK", "KP", "MB", "MS", "NG", "PO", "SG", "ZA" };
            if (regions.Contains(regUpper))
            {
                var regLower = region.ToLowerInvariant();
                return Ok(_covidSledilnikService.FromToDate(regLower, fromDate, toDate));
            }
            return new JsonResult(new { message = "Region does not exist." }) { StatusCode = StatusCodes.Status400BadRequest };
        }
        /// <summary>
        /// On this endpoint User gets a data from CSV file for last seven days and the are order descendingly.
        /// </summary>
        /// <returns></returns>
        [HttpGet("region/lastweek")]
        public IEnumerable<LastWeek> GetFromLastWeek()
        {
            return _covidSledilnikService.FromLastWeek();
        }
    }
}
