using System.Net.Http.Json;
using System.Net.Http.Headers;
using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Models_Settings.Settings;
using Microsoft.Extensions.Options;

namespace GlobalInsightsApi_Assessment.Clients;

public class GitHubClient : IGitHubClient
{
    private readonly HttpClient _httpClient;
    private readonly GitHubSettings _settings;
    private readonly ILogger<GitHubClient> _logger;

    public GitHubClient(
        HttpClient httpClient,
        IOptions<GitHubSettings> settings,
        ILogger<GitHubClient> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;

        // Set required headers for GitHub API
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(_settings.UserAgent, "1.0"));
        
        if (!string.IsNullOrEmpty(_settings.Token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _settings.Token);
        }
    }

    public async Task<GitHubResponse> GetUserInfoAsync(string username, CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching GitHub user info for: {Username}", username);
        var userUrl = $"{_settings.BaseUrl}/users/{Uri.EscapeDataString(username)}";
        var userResponse = await _httpClient.GetFromJsonAsync<GitHubResponse>(userUrl, ct);
        if (userResponse == null)
        {
            throw new Exception("Failed to deserialize GitHub user response");
        }
        return userResponse;
    }

    public async Task<List<GitHubRepoResponse>> GetUserReposAsync(string username, int page = 1, int perPage = 5, CancellationToken ct = default)
    {
        _logger.LogInformation("Fetching GitHub repos for: {Username}, page: {Page}, perPage: {PerPage}", username, page, perPage);
        var reposUrl = $"{_settings.BaseUrl}/users/{Uri.EscapeDataString(username)}/repos?sort=updated&page={page}&per_page={perPage}";
        var reposResponse = await _httpClient.GetFromJsonAsync<List<GitHubRepoResponse>>(reposUrl, ct);
        if (reposResponse == null)
        {
            throw new Exception("Failed to deserialize GitHub repositories response");
        }
        return reposResponse;
    }
}
