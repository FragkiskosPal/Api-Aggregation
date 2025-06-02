using System.Net.Http.Json;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.Settings;
using Microsoft.Extensions.Options;

namespace GlobalInsightsApi_Assessment.Clients;

public class NewsClient : INewsClient
{
    private readonly HttpClient _httpClient;
    private readonly NewsApiSettings _settings;
    private readonly ILogger<NewsClient> _logger;

    public NewsClient(
        HttpClient httpClient,
        IOptions<NewsApiSettings> settings,
        ILogger<NewsClient> logger)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<NewsResponse> GetNewsAsync(string query, CancellationToken ct = default)
    {
        try
        {
            var url = $"{_settings.BaseUrl}/everything?q={Uri.EscapeDataString(query)}&apiKey={_settings.ApiKey}&language=en&sortBy=publishedAt";
            _logger.LogInformation("Fetching news for query: {Query}", query);

            var response = await _httpClient.GetFromJsonAsync<NewsApiResponse>(url, ct);
            if (response == null)
            {
                throw new Exception("Failed to deserialize news response");
            }

            return new NewsResponse
            {
                Status = response.Status,
                TotalResults = response.TotalResults,
                Articles = response.Articles.Select(MapToArticle).ToList(),
                Query = query,
                Timestamp = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching news for query: {Query}", query);
            throw;
        }
    }

    private static Article MapToArticle(NewsApiArticle apiArticle)
    {
        return new Article
        {
            Title = apiArticle.Title,
            Description = apiArticle.Description,
            Url = apiArticle.Url,
            PublishedAt = apiArticle.PublishedAt,
            Source = apiArticle.Source.Name,
            Author = apiArticle.Author,
            Content = apiArticle.Content
        };
    }
}

// News API Response Models
internal class NewsApiResponse
{
    public string Status { get; set; } = string.Empty;
    public int TotalResults { get; set; }
    public List<NewsApiArticle> Articles { get; set; } = new();
}

internal class NewsApiArticle
{
    public NewsApiSource Source { get; set; } = new();
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string UrlToImage { get; set; } = string.Empty;
    public DateTime PublishedAt { get; set; }
    public string Content { get; set; } = string.Empty;
}

internal class NewsApiSource
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
