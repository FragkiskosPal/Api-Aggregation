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
        public long Timestamp { get; set; }      // UNIX timestamp σε δευτερόλεπτα

        [JsonProperty("temp")]
        public double Temperature { get; set; }  // Θερμοκρασία ( Kelvin ή Celsius αν έχεις units=metric )

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; } = new();
    }
}
