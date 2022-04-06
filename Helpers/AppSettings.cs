using System.Collections.Generic;

namespace CovidSledilnik.Helpers
{
    public class AppSettings
    {
        public string URL { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Regions { get; set; }

        public AppSettings()
        {
            Regions = new List<string>();
        }    
    }
}
