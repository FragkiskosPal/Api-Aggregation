namespace GlobalInsightsApi_Assessment.Models_Settings.Weather;

public class WeatherResponse
{
    public string City { get; set; } = string.Empty;
    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public DateTime Timestamp { get; set; }
    public WeatherInfo Weather { get; set; } = new();
}
