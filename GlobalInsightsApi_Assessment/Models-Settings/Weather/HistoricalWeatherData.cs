using Newtonsoft.Json;
using System.Collections.Generic;

namespace GlobalInsightsApi_Assessment.Models_Settings.Weather
{
    /// <summary>
    /// Αντιπροσωπεύει την απάντηση του One Call 3.0 "timemachine".
    /// Περιέχει το αντικείμενο "current" και τον πίνακα "hourly".
    /// </summary>
    public class HistoricalWeatherData
    {
        [JsonProperty("current")]
        public WeatherSnapshot Current { get; set; } = null!;

        [JsonProperty("hourly")]
        public List<WeatherSnapshot> Hourly { get; set; } = new();
    }
}
