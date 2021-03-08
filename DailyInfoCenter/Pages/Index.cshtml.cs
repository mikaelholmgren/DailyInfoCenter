using DailyInfoCenter.Models.SL;
using DailyInfoCenter.Models.SL.Departure;
using DailyInfoCenter.Models.SMHI;
using DailyInfoCenter.Models.SR;
using DailyInfoCenter.QuotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DailyInfoCenter.Pages
{
    public class IndexModel : PageModel
    {
        private HttpClient _client = new();
        private string _slStopsKey;
        private string _slDeparturesKey;
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
            _slStopsKey = _config["SLStopsKey"];
            _slDeparturesKey = _config["SLDeparturesKey"];
        }
        public Quote DailyQuote { get; set; }
        public SMHIModel SMHIWeather { get; set; }
        public SRTrafficInfo TrafficInfo { get; set; }
        public Stops Stops { get; set; }
        public string SiteId { get; set; }
        public Departures Departures { get; private set; }

        public async Task<IActionResult> OnGetAsync(string stopSearch, string siteId)
        {
            DailyQuote = await GetQuote();
            SMHIWeather = await GetSMHIWeather();
            TrafficInfo = await GetSRTrafficInfo();
            if (stopSearch != null)
                Stops = await GetSLStops(stopSearch);
            SiteId = siteId;
            if (SiteId != null)
                Departures = await GetSLDepartures(siteId);
            return Page();
        }
        private async Task<Quote> GetQuote()
        {
            var baseAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToString()}";
            string responseString = await _client.GetStringAsync(baseAddress + "/api/quotes");
            return JsonSerializer.Deserialize<Quote>(responseString);
        }
        private async Task<SMHIModel> GetSMHIWeather()
        {
            
            string smhiApiAdress = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/18.063240/lat/59.334591/data.json";
            string responseString = await _client.GetStringAsync(smhiApiAdress);
            return JsonSerializer.Deserialize<SMHIModel>(responseString);
        }
        private async Task<SRTrafficInfo> GetSRTrafficInfo()
        {

            string trafficLink = "https://api.sr.se/api/v2/traffic/messages?format=json&trafficareaname=Stockholm";
            string responseString = await _client.GetStringAsync(trafficLink);
            return JsonSerializer.Deserialize<SRTrafficInfo>(responseString);
        }
        private async Task<Stops> GetSLStops(string searchTerm)
        {
            string slLink = $"https://api.sl.se/api2/typeahead.json?key={_slStopsKey}&searchstring={searchTerm}&maxresults=10";

            string responseString = await _client.GetStringAsync(slLink);
            return JsonSerializer.Deserialize<Stops>(responseString);
        }
        private async Task<Departures> GetSLDepartures(string siteId)
        {
            string departuresLink = $"https://api.sl.se/api2/realtimedeparturesV4.json?key={_slDeparturesKey}&siteid={siteId}&timewindow=15";

            string responseString = await _client.GetStringAsync(departuresLink);
            return JsonSerializer.Deserialize<Departures>(responseString);
        }

    }
}
