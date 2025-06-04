using Newtonsoft.Json;
using System.Collections.Generic;

namespace GlobalInsightsApi_Assessment.Models_Settings.Weather
{/// <summary>
 /// Αντιπροσωπεύει ένα snapshot καιρού (παρέχονται πεδία dt, temp, weather κ.λπ.).
 /// Ισχύει για το "current" και για κάθε στοιχείο μέσα στο "hourly".
 /// </summary>
    public class WeatherSnapshot
    {
        [JsonProperty("dt")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; } = new();
    }
}
