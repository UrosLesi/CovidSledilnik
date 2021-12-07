using System;

namespace CovidSledilnik.Models
{
    public class Cases
    {
        public DateTime Date { get; set; }
        public string Region { get; set; }
        public int NumberActive { get; set; }
        public int VaccinatedFirst { get; set; }
        public int VaccinatedSecond { get; set; }
        public int DeceasedToDate { get; set; }
    }
}
