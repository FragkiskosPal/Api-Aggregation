namespace GlobalInsightsApi_Assessment.Models_Settings.Settings
{
    /// <summary>
    /// POCO που αντιστοιχεί στο configuration section "OpenWeatherMap" στο appsettings.json.
    /// </summary>
    public class OpenWeatherSettings
    {
        public string BaseUrl { get; set; } = "";
        public string ApiKey { get; set; } = "";
    }
}
