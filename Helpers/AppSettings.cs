using System.Collections.Generic;

namespace CovidSledilnik.Helpers
{
    public class AppSettings
    {
        /// <summary>
        /// URL to file source.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Username for Basic Authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for Basic Authentication.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// List of regions that are avalible to choose.
        /// </summary>
        public List<string> Regions { get; set; }
    }
}
