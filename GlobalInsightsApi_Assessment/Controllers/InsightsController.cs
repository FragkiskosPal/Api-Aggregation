using System.Threading.Tasks;
using GlobalInsightsApi_Assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalInsightsApi_Assessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsightsController : ControllerBase
{
    private readonly IInsightsService _insightsService;

    public InsightsController(IInsightsService insightsService)
    {
        _insightsService = insightsService;
    }

    // Aggregation endpoint (GET /api/insights/aggregate) that consolidates data from all external APIs.
    // Optional query parameters (e.g. date, category, sort) can be added for filtering/sorting.
    [HttpGet("aggregate")]
    public async Task<IActionResult> GetAggregatedInsightsAsync([FromQuery] string city, [FromQuery] string query, [FromQuery] string username, [FromQuery] string date = null, [FromQuery] string category = null, [FromQuery] string sort = null)
    {
        // (Optional) Validate input (e.g. if city, query, username are required, throw BadRequest if missing)
        if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(query) || string.IsNullOrEmpty(username))
        {
            return BadRequest("City, query, and username are required.");
        }

        // Call the aggregation service (which internally uses Task.WhenAll for parallel calls).
        // (Note: Filtering/sorting (e.g. via date, category, sort) is assumed to be handled inside the service if needed.)
        var aggregatedData = await _insightsService.GetAggregatedInsightsAsync(city, query, username);

        // (Optional) If aggregatedData is null (e.g. if all clients failed), return NotFound or a custom error.
        if (aggregatedData == null)
        {
            return NotFound("Aggregation failed (or no data available).");
        }

        return Ok(aggregatedData);
    }

    // (Optional) Endpoint for individual weather data (GET /api/insights/weather?city=...)
    [HttpGet("weather")]
    public async Task<IActionResult> GetWeatherInsightsAsync([FromQuery] string city)
    {
        if (string.IsNullOrEmpty(city))
        {
            return BadRequest("City is required.");
        }
        var weatherData = await _insightsService.GetWeatherInsightsAsync(city);
        if (weatherData == null)
        {
            return NotFound("Weather data not found.");
        }
        return Ok(weatherData);
    }

    // (Optional) Endpoint for individual news data (GET /api/insights/news?query=...)
    [HttpGet("news")]
    public async Task<IActionResult> GetNewsInsightsAsync([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query is required.");
        }
        var newsData = await _insightsService.GetNewsInsightsAsync(query);
        if (newsData == null)
        {
            return NotFound("News data not found.");
        }
        return Ok(newsData);
    }

    // (Optional) Endpoint for individual GitHub data (GET /api/insights/github?username=...)
    [HttpGet("github")]
    public async Task<IActionResult> GetGitHubInsightsAsync([FromQuery] string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return BadRequest("Username is required.");
        }
        var gitHubData = await _insightsService.GetGitHubInsightsAsync(username);
        if (gitHubData == null)
        {
            return NotFound("GitHub data not found.");
        }
        return Ok(gitHubData);
    }
} 