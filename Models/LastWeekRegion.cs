namespace CovidSledilnik.Models
{
    /// <summary>
    /// Model for presenting number of cases in specific refion for past week.
    /// </summary>
    public class LastWeekRegion
    {
        /// <summary>
        /// Specific region for which data applies.
        /// </summary>
        public string Region { get; set; } 

        /// <summary>
        /// Number of active cases for past week in specific region.
        /// </summary>
        public int ActiveCases { get; set; }
    }
}
