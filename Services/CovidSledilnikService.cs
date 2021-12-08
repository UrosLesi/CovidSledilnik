using CovidSledilnik.Helpers;
using CovidSledilnik.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
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
    public interface ICovidSledilnikService
    {
        IEnumerable<Cases> FromToDate(string region, DateTime fromDate, DateTime toDate);
        IEnumerable<LastWeekRegion> FromLastWeek();
    }

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

        public IEnumerable<Cases> FromToDate(string region, DateTime fromDate, DateTime toDate)
        {
            var results = new List<Cases>();
            _csvReader.Read();
            _csvReader.ReadHeader();
            while (_csvReader.Read())
            {
                var date = _csvReader.GetField("date");
                var activeCases = _csvReader.GetField("region." + region + ".cases.active");
                var vaccinatedFirst = _csvReader.GetField("region." + region + ".vaccinated.1st.todate");
                var vaccinatedSecond = _csvReader.GetField("region." + region + ".vaccinated.2nd.todate");
                var deceased = _csvReader.GetField("region." + region + ".deceased.todate");
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
            var cases = results
                    .Where(c => c.Date >= fromDate && c.Date <= toDate)
                    .ToList();
            return cases;

        }
        public IEnumerable<LastWeekRegion> FromLastWeek()
        {
            var results = new List<LastWeek>();
            _csvReader.Read();
            _csvReader.ReadHeader();
            while (_csvReader.Read())
            {
                var ljActive = _csvReader.GetField("region.lj.cases.confirmed.todate");
                var ceActive = _csvReader.GetField("region.ce.cases.confirmed.todate");
                var krActive = _csvReader.GetField("region.kr.cases.confirmed.todate");
                var nmActive = _csvReader.GetField("region.nm.cases.confirmed.todate");
                var kkActive = _csvReader.GetField("region.kk.cases.confirmed.todate");
                var kpActive = _csvReader.GetField("region.kp.cases.confirmed.todate");
                var mbActive = _csvReader.GetField("region.mb.cases.confirmed.todate");
                var msActive = _csvReader.GetField("region.ms.cases.confirmed.todate");
                var ngActive = _csvReader.GetField("region.ng.cases.confirmed.todate");
                var poActive = _csvReader.GetField("region.po.cases.confirmed.todate");
                var sgActive = _csvReader.GetField("region.sg.cases.confirmed.todate");
                var zaActive = _csvReader.GetField("region.za.cases.confirmed.todate");

                results.Add(new LastWeek()
                {
                    LjActiveCases = ljActive == "" ? 0 : int.Parse(ljActive),
                    CeActiveCases = ceActive == "" ? 0 : int.Parse(ceActive),
                    KrActiveCases = krActive == "" ? 0 : int.Parse(krActive),
                    NmActiveCases = nmActive == "" ? 0 : int.Parse(nmActive),
                    KkActiveCases = kkActive == "" ? 0 : int.Parse(kkActive),
                    KpActiveCases = kpActive == "" ? 0 : int.Parse(kpActive),
                    MbActiveCases = mbActive == "" ? 0 : int.Parse(mbActive),
                    MsActiveCases = msActive == "" ? 0 : int.Parse(msActive),
                    NgActiveCases = ngActive == "" ? 0 : int.Parse(ngActive),
                    PoActiveCases = poActive == "" ? 0 : int.Parse(poActive),
                    SgActiveCases = sgActive == "" ? 0 : int.Parse(sgActive),
                    ZaActiveCases = zaActive == "" ? 0 : int.Parse(zaActive),

                });
            }
            int count = results.Count();
            var resultWeekAgo = results.ElementAt(count - 8);
            var resultToday = results.ElementAt(count - 1);
            var lastWeekActive = new List<LastWeekRegion>();
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "LJ",
                ActiveCases = resultToday.LjActiveCases - resultWeekAgo.LjActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "CE",
                ActiveCases = resultToday.CeActiveCases - resultWeekAgo.CeActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "KR",
                ActiveCases = resultToday.KrActiveCases - resultWeekAgo.KrActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "NM",
                ActiveCases = resultToday.NmActiveCases - resultWeekAgo.NmActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "KK",
                ActiveCases = resultToday.KkActiveCases - resultWeekAgo.KkActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "KP",
                ActiveCases = resultToday.KpActiveCases - resultWeekAgo.KpActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "MB",
                ActiveCases = resultToday.MbActiveCases - resultWeekAgo.MbActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "MS",
                ActiveCases = resultToday.MsActiveCases - resultWeekAgo.MsActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "NG",
                ActiveCases = resultToday.NgActiveCases - resultWeekAgo.NgActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "PO",
                ActiveCases = resultToday.PoActiveCases - resultWeekAgo.PoActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "SG",
                ActiveCases = resultToday.SgActiveCases - resultWeekAgo.SgActiveCases,
            });
            lastWeekActive.Add(new LastWeekRegion()
            {
                Region = "ZA",
                ActiveCases = resultToday.ZaActiveCases - resultWeekAgo.ZaActiveCases,
            });
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
