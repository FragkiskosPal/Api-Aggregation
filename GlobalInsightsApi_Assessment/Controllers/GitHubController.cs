using GlobalInsightsApi_Assessment.Models;
using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalInsightsApi_Assessment.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GitHubController : ControllerBase
{
    private readonly IInsightsService _insightsService;
    private readonly IValidationService _validationService;
    private readonly ILogger<GitHubController> _logger;

    public GitHubController(
        IInsightsService insightsService,
        IValidationService validationService,
        ILogger<GitHubController> logger)
    {
        _insightsService = insightsService;
        _validationService = validationService;
        _logger = logger;
    }

    /// <summary>
    /// Ανακτά πληροφορίες για έναν GitHub χρήστη
    /// </summary>
    /// <param name="username">Το GitHub username</param>
    /// <response code="200">Επιστρέφει τις πληροφορίες του χρήστη</response>
    /// <response code="400">Άκυρο username</response>
    /// <response code="404">Ο χρήστης δεν βρέθηκε</response>
    /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
    [HttpGet("user/{username}")]
    [ProducesResponseType(typeof(GitHubUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GitHubUserResponse>> GetUserInfo(string username)
    {
        _logger.LogInformation("Getting GitHub user info for: {Username}", username);

        _validationService.ValidateGitHubUsername(username);
        var userInfo = await _insightsService.GetGitHubUserInfoAsync(username);
        
        return Ok(userInfo);
    }

    /// <summary>
    /// Ανακτά τα repositories ενός GitHub χρήστη
    /// </summary>
    /// <param name="username">Το GitHub username</param>
    /// <param name="page">Αριθμός σελίδας (προαιρετικό, προεπιλογή: 1)</param>
    /// <param name="perPage">Αριθμός repositories ανά σελίδα (προαιρετικό, προεπιλογή: 10)</param>
    /// <response code="200">Επιστρέφει τα repositories</response>
    /// <response code="400">Άκυρο username</response>
    /// <response code="404">Ο χρήστης δεν βρέθηκε</response>
    /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
    [HttpGet("user/{username}/repos")]
    [ProducesResponseType(typeof(List<GitHubRepoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<GitHubRepoResponse>>> GetUserRepositories(
        string username,
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 10)
    {
        _logger.LogInformation("Getting repositories for user: {Username}, page: {Page}, perPage: {PerPage}", 
            username, page, perPage);

        _validationService.ValidateGitHubUsername(username);

        if (page < 1)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid page number",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "page", Message = "Page number must be greater than 0" }
            });
        }

        if (perPage < 1 || perPage > 100)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid per_page value",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "perPage", Message = "Per page value must be between 1 and 100" }
            });
        }

        var repos = await _insightsService.GetGitHubUserReposAsync(username, page, perPage);
        return Ok(repos);
    }

    /// <summary>
    /// Ανακτά τις συνεισφορές ενός GitHub χρήστη
    /// </summary>
    /// <param name="username">Το GitHub username</param>
    /// <param name="year">Το έτος (προαιρετικό, προεπιλογή: τρέχον έτος)</param>
    /// <response code="200">Επιστρέφει τις συνεισφορές</response>
    /// <response code="400">Άκυρο username ή έτος</response>
    /// <response code="404">Ο χρήστης δεν βρέθηκε</response>
    /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
    [HttpGet("user/{username}/contributions")]
    [ProducesResponseType(typeof(GitHubContributionsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GitHubContributionsResponse>> GetUserContributions(
        string username,
        [FromQuery] int? year = null)
    {
        _logger.LogInformation("Getting contributions for user: {Username}, year: {Year}", 
            username, year ?? DateTime.UtcNow.Year);

        _validationService.ValidateGitHubUsername(username);

        if (year.HasValue && (year.Value < 2008 || year.Value > DateTime.UtcNow.Year))
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid year",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "year", Message = $"Year must be between 2008 and {DateTime.UtcNow.Year}" }
            });
        }

        var contributions = await _insightsService.GetGitHubUserContributionsAsync(username, year);
        return Ok(contributions);
    }
} 