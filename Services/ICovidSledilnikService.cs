using CovidSledilnik.Models;
using System;
using System.Collections.Generic;

namespace CovidSledilnik.Services
{
    public interface ICovidSledilnikService
    {
        /// <summary>
        /// Get a list of Cases based on region and time window between selected dates.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>List of Cases for specific region in specific time window.</returns>
        IEnumerable<Cases> FromToDateRegion(string region, DateTime? fromDate, DateTime? toDate);

        /// <summary>
        /// Get a list of objects that contains region and active cases in the past week.
        /// </summary>
        /// <returns>Descendingly orderd list of objects with number of active cases in past week for each region.</returns>
        IEnumerable<LastWeekRegion> FromLastWeek();

        /// <summary>
        /// Get a list of Cases based on time window between selected dates.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>List of Cases for each region in specific time window.</returns>
        public IEnumerable<Cases> FromToDate(DateTime? fromDate, DateTime? toDate);
    }
}
