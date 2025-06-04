using System.Text.RegularExpressions;
using System.Collections.Generic;
using GlobalInsightsApi_Assessment.Exceptions;

namespace GlobalInsightsApi_Assessment.Services;

public class ValidationService : IValidationService
{
    private readonly ILogger<ValidationService> _logger;
    private static readonly Regex CityRegex = new(@"^[a-zA-Z\s\-']{2,50}$", RegexOptions.Compiled);
    private static readonly Regex UsernameRegex = new(@"^[a-zA-Z0-9](?:[a-zA-Z0-9]|-(?=[a-zA-Z0-9])){0,38}$", RegexOptions.Compiled);
    private static readonly HashSet<string> AllowedCategories = new(
        new[] { "technology", "business", "sports" },
        StringComparer.OrdinalIgnoreCase);

    public ValidationService(ILogger<ValidationService> logger)
    {
        _logger = logger;
    }

    public void ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ApiException(
                "City name is required",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "city", Message = "City name cannot be empty" });
        }

        if (!CityRegex.IsMatch(city))
        {
            throw new ApiException(
                "Invalid city name format",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "city", Message = "City name must be 2-50 characters long and contain only letters, spaces, hyphens, and apostrophes" });
        }

        _logger.LogDebug("City name validated: {City}", city);
    }

    public void ValidateNewsQuery(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ApiException(
                "Search query is required",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "query", Message = "Search query cannot be empty" });
        }

        if (query.Length < 2 || query.Length > 100)
        {
            throw new ApiException(
                "Invalid search query length",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "query", Message = "Search query must be between 2 and 100 characters" });
        }

        _logger.LogDebug("News query validated: {Query}", query);
    }

    public void ValidateNewsCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category) || !AllowedCategories.Contains(category))
        {
            throw new ApiException(
                "Invalid news category",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "category", Message = "Unsupported news category" });
        }

        _logger.LogDebug("News category validated: {Category}", category);
    }

    public void ValidateGitHubUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ApiException(
                "GitHub username is required",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "username", Message = "GitHub username cannot be empty" });
        }

        if (!UsernameRegex.IsMatch(username))
        {
            throw new ApiException(
                "Invalid GitHub username format",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "username", Message = "GitHub username must be 1-39 characters long and can only contain alphanumeric characters and single hyphens" });
        }

        _logger.LogDebug("GitHub username validated: {Username}", username);
    }

    public void ValidateCoordinates(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new ApiException(
                "Invalid latitude value",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "latitude", Message = "Latitude must be between -90 and 90 degrees" });
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new ApiException(
                "Invalid longitude value",
                StatusCodes.Status400BadRequest,
                ErrorCodes.ValidationError,
                new { Field = "longitude", Message = "Longitude must be between -180 and 180 degrees" });
        }

        _logger.LogDebug("Coordinates validated: {Latitude}, {Longitude}", latitude, longitude);
    }
} 