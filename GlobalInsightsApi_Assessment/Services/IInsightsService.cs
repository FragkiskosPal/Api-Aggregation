using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;

namespace GlobalInsightsApi_Assessment.Services;

public interface IInsightsService
{
    Task<WeatherResponse> GetWeatherInsightsAsync(string city);
    Task<NewsResponse> GetNewsInsightsAsync(string query);
    Task<GitHubResponse> GetGitHubInsightsAsync(string username);
    Task<AggregatedInsights> GetAggregatedInsightsAsync(string city, string query, string username);

    // --- Missing methods for controllers ---
    // Task<HistoricalWeatherData> GetHistoricalWeatherAsync(double lat, double lon, DateTime date);
    // Task<GitHubUserResponse> GetGitHubUserInfoAsync(string username); // Removed (if not strictly required) to avoid type mismatch
    // Task<List<GitHubRepoResponse>> GetGitHubUserReposAsync(string username, int page, int perPage);
    // Task<GitHubContributionsResponse> GetGitHubUserContributionsAsync(string username, int? year);
    // Task<NewsResponse> GetNewsInsightsAsync(string query, int page, int pageSize);
    // Task<NewsResponse> GetNewsByCategoryAsync(string category, int page, int pageSize);

    // Re-adding (uncommenting) the missing methods so that controllers can call them.
    // (Also, GetGitHubUserInfoAsync now returns Task<GitHubResponse> to match the client.)
    Task<HistoricalWeatherData> GetHistoricalWeatherAsync(double lat, double lon, DateTime date);
    Task<GitHubResponse> GetGitHubUserInfoAsync(string username);
    Task<List<GitHubRepoResponse>> GetGitHubUserReposAsync(string username, int page, int perPage);
    Task<GitHubContributionsResponse> GetGitHubUserContributionsAsync(string username, int? year);
    Task<NewsResponse> GetNewsInsightsAsync(string query, int page, int pageSize);
    Task<NewsResponse> GetNewsByCategoryAsync(string category, int page, int pageSize);
}

public class AggregatedInsights
{
    public WeatherResponse Weather { get; set; } = new();
    public NewsResponse News { get; set; } = new();
    public GitHubResponse GitHub { get; set; } = new();
} 