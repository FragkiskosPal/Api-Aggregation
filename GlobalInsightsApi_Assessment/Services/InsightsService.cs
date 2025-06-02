using GlobalInsightsApi_Assessment.Clients;
using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalInsightsApi_Assessment.Services;

public class InsightsService : IInsightsService
{
    private readonly IWeatherClient _weatherClient;
    private readonly INewsClient _newsClient;
    private readonly IGitHubClient _githubClient;

    public InsightsService(
        IWeatherClient weatherClient,
        INewsClient newsClient,
        IGitHubClient githubClient)
    {
        _weatherClient = weatherClient;
        _newsClient = newsClient;
        _githubClient = githubClient;
    }

    public async Task<WeatherResponse> GetWeatherInsightsAsync(string city)
        => await _weatherClient.GetWeatherAsync(city);

    public async Task<NewsResponse> GetNewsInsightsAsync(string query)
        => await _newsClient.GetNewsAsync(query);

    public async Task<GitHubResponse> GetGitHubInsightsAsync(string username)
        => await _githubClient.GetUserInfoAsync(username);

    public async Task<AggregatedInsights> GetAggregatedInsightsAsync(string city, string query, string username)
    {
        var weatherTask = GetWeatherInsightsAsync(city);
        var newsTask = GetNewsInsightsAsync(query);
        var githubTask = GetGitHubInsightsAsync(username);
        await Task.WhenAll(weatherTask, newsTask, githubTask);
        return new AggregatedInsights
        {
            Weather = await weatherTask,
            News = await newsTask,
            GitHub = await githubTask
        };
    }

    // --- Missing methods for controllers ---
    public async Task<HistoricalWeatherData> GetHistoricalWeatherAsync(double lat, double lon, DateTime date) => await _weatherClient.FetchHistoricalAsync(lat, lon, date);
    public async Task<GitHubResponse> GetGitHubUserInfoAsync(string username) => await _githubClient.GetUserInfoAsync(username);
    public async Task<List<GitHubRepoResponse>> GetGitHubUserReposAsync(string username, int page, int perPage) => await _githubClient.GetUserReposAsync(username, page, perPage);
    public Task<GitHubContributionsResponse> GetGitHubUserContributionsAsync(string username, int? year) => throw new NotImplementedException("Implement GitHub user contributions retrieval if needed");
    public Task<NewsResponse> GetNewsInsightsAsync(string query, int page, int pageSize) => throw new NotImplementedException("Implement paged news search if needed");
    public Task<NewsResponse> GetNewsByCategoryAsync(string category, int page, int pageSize) => throw new NotImplementedException("Implement news by category if needed");
}
