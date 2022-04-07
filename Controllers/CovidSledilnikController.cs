using CovidSledilnik.Helpers;
using CovidSledilnik.Models;
using CovidSledilnik.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace CovidSledilnik.Controllers
{
    /// <summary>
    /// Manage data regarding Covid.
    /// </summary>
    [ApiController]
    [Route("api/region")]
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
        /// <param name="region">Regions: LJ, CE, KR, NM, KK, KP, MB, MS, NG, PO, SG, ZA</param>
        /// <param name="fromDate">E.g.: 2022-03-28</param>
        /// <param name="toDate">E.g.: 2022-03-28</param>
        /// <returns>List of Cases for specific region in specific timeline.</returns>
        /// <response code="200">Cases found</response>
        /// <response code="500">Unknown server error</response>
        [BasicAuthorization]
        [HttpGet("cases")]
        [ProducesResponseType(typeof(IEnumerable<Cases>), 200)]
        [ProducesResponseType(500)]
        public IEnumerable<Cases> GetFromToDate([BindRequired] string region, DateTime? fromDate, DateTime? toDate)
        {
            return _covidSledilnikService.FromToDate(region, fromDate, toDate);
        }

        /// <summary>
        /// On this endpoint User gets a data from CSV file for last seven days and the are order descendingly.
        /// </summary>
        /// <returns>Descendingly orderd list of objects with number of active cases in past week for each region.</returns>
        /// <response code="200">Last week active cases found</response>
        /// <response code="500">Unknown server error</response>
        [BasicAuthorization]
        [HttpGet("lastweek")]
        [ProducesResponseType(typeof(IEnumerable<LastWeekRegion>), 200)]
        [ProducesResponseType(500)]
        public IEnumerable<LastWeekRegion> GetFromLastWeek()
        {
            return _covidSledilnikService.FromLastWeek();
        }
    }
}
