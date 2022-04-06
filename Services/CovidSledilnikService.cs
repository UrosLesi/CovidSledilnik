using CovidSledilnik.Helpers;
using CovidSledilnik.Models;
using CsvHelper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace CovidSledilnik.Services
{
    public class CovidSledilnikService : ICovidSledilnikService
    {
        private readonly AppSettings _appSettings;
        private CsvReader _csvReader;
        private StreamReader _streamReader;

        public CovidSledilnikService(
            IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            GetCSV();
        }

        ///  <inheritdoc />
        public IEnumerable<Cases> FromToDate(string region, DateTime fromDate, DateTime toDate)
        {
            if (!_appSettings.Regions.Contains(region.ToUpperInvariant()))
                throw new ArgumentException($"Region {region} don't exists.", region);

            if (fromDate > toDate)
                throw new ArgumentException($"{fromDate} cannot be greater then {toDate}", "fromDate");

            var results = new List<Cases>();

            _csvReader.Read();
            _csvReader.ReadHeader();

            while (_csvReader.Read())
            {
                var date = _csvReader.GetField("date");
                var activeCases = _csvReader.GetField("region." + region.ToLowerInvariant() + ".cases.active");
                var vaccinatedFirst = _csvReader.GetField("region." + region.ToLowerInvariant() + ".vaccinated.1st.todate");
                var vaccinatedSecond = _csvReader.GetField("region." + region.ToLowerInvariant() + ".vaccinated.2nd.todate");
                var deceased = _csvReader.GetField("region." + region.ToLowerInvariant() + ".deceased.todate");

                results.Add(new Cases()
                {
                    Date = DateTime.Parse(date),
                    Region = region,
                    NumberActive = activeCases == "" ? 0 : int.Parse(activeCases),
                    VaccinatedFirst = vaccinatedFirst == "" ? 0 : int.Parse(vaccinatedFirst),
                    VaccinatedSecond = vaccinatedSecond == "" ? 0 : int.Parse(vaccinatedSecond),
                    DeceasedToDate = deceased == "" ? 0 : int.Parse(deceased),
                });
            }

            if (!results.Any(r => r.Date == fromDate) || !results.Any(r => r.Date == toDate))
                throw new ArgumentException($"Parameter {fromDate} or {toDate} dosen't exist in a file.");

            var cases = results
                        .Where(c => c.Date >= fromDate && c.Date <= toDate)
                        .ToList();

            return cases;
        }

        ///  <inheritdoc />
        public IEnumerable<LastWeekRegion> FromLastWeek()
        {
            Dictionary<string, List<int>> results = new Dictionary<string, List<int>>();

            _csvReader.Read();
            _csvReader.ReadHeader();
            
            while (_csvReader.Read())
            {
                foreach (var region in _appSettings.Regions)
                {
                    var activeCases = _csvReader.GetField("region." + region.ToLowerInvariant() + ".cases.confirmed.todate");
                    var activeCasesParsed = activeCases == "" ? 0 : int.Parse(activeCases);

                    if (results.ContainsKey(region))
                        results[region].Add(activeCasesParsed);
                    else
                        results[region] = new List<int>() { activeCasesParsed };
                }
            }

            int count = results.ElementAt(0).Value.Count();
            var lastWeekActive = new List<LastWeekRegion>();

            foreach (var region in _appSettings.Regions)
            {
                lastWeekActive.Add(new LastWeekRegion()
                {
                    Region = region,
                    ActiveCases = results[region].ElementAt(count - 1) - results[region].ElementAt(count - 8)
                });
            }

            return lastWeekActive.OrderByDescending(x => x.ActiveCases).ToList();
        }
        private void GetCSV()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_appSettings.URL);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            _streamReader = new StreamReader(resp.GetResponseStream());
            _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);
        }
    }
}
