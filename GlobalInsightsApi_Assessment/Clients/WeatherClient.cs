using System.Net.Http.Json;
using System.Text.Json;
using GlobalInsightsApi_Assessment.Models_Settings.Settings;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using Microsoft.Extensions.Options;

namespace GlobalInsightsApi_Assessment.Clients;

public class WeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherSettings _settings;
    private readonly ILogger<WeatherClient> _logger;

    public WeatherClient(
        HttpClient httpClient,
        IOptions<OpenWeatherSettings> settings,
        ILogger<WeatherClient> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<WeatherResponse> GetWeatherAsync(string city, CancellationToken ct = default)
    {
        try
        {
            var url = $"{_settings.BaseUrl}/weather?q={Uri.EscapeDataString(city)}&appid={_settings.ApiKey}&units=metric";
            _logger.LogInformation("Fetching current weather for city: {City}", city);

            var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url, ct);
            if (response == null)
            {
                throw new Exception("Failed to deserialize weather response");
            }

            return new WeatherResponse
            {
                City = response.Name,
                Temperature = response.Main.Temp,
                Description = response.Weather.FirstOrDefault()?.Description ?? string.Empty,
                Humidity = response.Main.Humidity,
                WindSpeed = response.Wind.Speed,
                Timestamp = DateTime.UtcNow,
                Weather = new WeatherInfo
                {
                    Main = response.Weather.FirstOrDefault()?.Main ?? string.Empty,
                    Description = response.Weather.FirstOrDefault()?.Description ?? string.Empty,
                    Icon = response.Weather.FirstOrDefault()?.Icon ?? string.Empty
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather for city: {City}", city);
            throw;
        }
    }

    public async Task<HistoricalWeatherData> FetchHistoricalAsync(
        double lat,
        double lon,
        DateTime dateUtc,
        CancellationToken ct = default)
    {
        try
        {
            var timestamp = ((DateTimeOffset)dateUtc.ToUniversalTime()).ToUnixTimeSeconds();
            var url = $"{_settings.BaseUrl}/onecall/timemachine?lat={lat}&lon={lon}&dt={timestamp}&appid={_settings.ApiKey}&units=metric";
            _logger.LogInformation("Fetching historical weather for coordinates: {Lat}, {Lon} at {Date}", lat, lon, dateUtc);

            var response = await _httpClient.GetFromJsonAsync<OpenWeatherHistoricalResponse>(url, ct);
            if (response == null)
            {
                throw new Exception("Failed to deserialize historical weather response");
            }

            return new HistoricalWeatherData
            {
                Current = MapToWeatherSnapshot(response.Current),
                Hourly = response.Hourly.Select(MapToWeatherSnapshot).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching historical weather for coordinates: {Lat}, {Lon} at {Date}", lat, lon, dateUtc);
            throw;
        }
    }

    private static WeatherSnapshot MapToWeatherSnapshot(OpenWeatherSnapshot snapshot)
    {
        return new WeatherSnapshot
        {
            Timestamp = DateTimeOffset.FromUnixTimeSeconds(snapshot.Dt).UtcDateTime,
            Temperature = snapshot.Temp,
            FeelsLike = snapshot.FeelsLike,
            Humidity = snapshot.Humidity,
            WindSpeed = snapshot.WindSpeed,
            Weather = new WeatherInfo
            {
                Main = snapshot.Weather.FirstOrDefault()?.Main ?? string.Empty,
                Description = snapshot.Weather.FirstOrDefault()?.Description ?? string.Empty,
                Icon = snapshot.Weather.FirstOrDefault()?.Icon ?? string.Empty
            }
        };
    }
}

// OpenWeather API Response Models
internal class OpenWeatherResponse
{
    public string Name { get; set; } = string.Empty;
    public OpenWeatherMain Main { get; set; } = new();
    public OpenWeatherWind Wind { get; set; } = new();
    public List<OpenWeatherInfo> Weather { get; set; } = new();
}

internal class OpenWeatherMain
{
    public double Temp { get; set; }
    public double Humidity { get; set; }
}

internal class OpenWeatherWind
{
    public double Speed { get; set; }
}

internal class OpenWeatherInfo
{
    public string Main { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

internal class OpenWeatherHistoricalResponse
{
    public OpenWeatherSnapshot Current { get; set; } = new();
    public List<OpenWeatherSnapshot> Hourly { get; set; } = new();
}

internal class OpenWeatherSnapshot
{
    public long Dt { get; set; }
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public double Humidity { get; set; }
    public double WindSpeed { get; set; }
    public List<OpenWeatherInfo> Weather { get; set; } = new();
} 