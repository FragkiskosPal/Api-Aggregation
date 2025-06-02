using Newtonsoft.Json;

namespace GlobalInsightsApi_Assessment.Models_Settings.Weather
{
    /// <summary>
    /// Αντιπροσωπεύει το αντικείμενο μέσα στο array "weather" 
    /// (π.χ. id, main, description, icon).
    /// </summary>
    public class WeatherInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("icon")]
        public string Icon { get; set; } = "";
    }
}
