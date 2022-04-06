using System;

namespace CovidSledilnik.Models
{
    /// <summary>
    /// Model for presenting data about COVID for specific date.
    /// </summary>
    public class Cases
    {
        public DateTime Date { get; set; }

        /// <summary>
        /// Specific region for which data applies.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Number of active cases on spcific date.
        /// </summary>
        public int NumberActive { get; set; }

        /// <summary>
        /// Number of people that recived first dose of vaccine until specific date.
        /// </summary>
        public int VaccinatedFirst { get; set; }

        /// <summary>
        /// Number of people that recived second dose of vaccine until specific date.
        /// </summary>
        public int VaccinatedSecond { get; set; }

        /// <summary>
        /// Number of people that were decased until specific date.
        /// </summary>
        public int DeceasedToDate { get; set; }
    }
}
