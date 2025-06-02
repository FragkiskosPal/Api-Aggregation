using GlobalInsightsApi_Assessment.Models;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalInsightsApi_Assessment.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NewsController : ControllerBase
{
    private readonly IInsightsService _insightsService;
    private readonly IValidationService _validationService;
    private readonly ILogger<NewsController> _logger;

    public NewsController(
        IInsightsService insightsService,
        IValidationService validationService,
        ILogger<NewsController> logger)
    {
        _insightsService = insightsService;
        _validationService = validationService;
        _logger = logger;
    }

    /// <summary>
    /// Ανακτά ειδήσεις για ένα συγκεκριμένο θέμα
    /// </summary>
    /// <param name="query">Το θέμα αναζήτησης</param>
    /// <param name="page">Αριθμός σελίδας (προαιρετικό, προεπιλογή: 1)</param>
    /// <param name="pageSize">Αριθμός αποτελεσμάτων ανά σελίδα (προαιρετικό, προεπιλογή: 10)</param>
    /// <response code="200">Επιστρέφει τις ειδήσεις</response>
    /// <response code="400">Άκυρο query αναζήτησης</response>
    /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
    [HttpGet("search")]
    [ProducesResponseType(typeof(NewsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<NewsResponse>> SearchNews(
        [FromQuery] string query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Searching news for query: {Query}, page: {Page}, pageSize: {PageSize}", 
            query, page, pageSize);

        _validationService.ValidateNewsQuery(query);

        if (page < 1)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid page number",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "page", Message = "Page number must be greater than 0" }
            });
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid page size",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "pageSize", Message = "Page size must be between 1 and 100" }
            });
        }

        var news = await _insightsService.GetNewsInsightsAsync(query, page, pageSize);
        return Ok(news);
    }

    /// <summary>
    /// Ανακτά τις τελευταίες ειδήσεις για μια συγκεκριμένη κατηγορία
    /// </summary>
    /// <param name="category">Η κατηγορία ειδήσεων (π.χ. business, technology, sports)</param>
    /// <param name="page">Αριθμός σελίδας (προαιρετικό, προεπιλογή: 1)</param>
    /// <param name="pageSize">Αριθμός αποτελεσμάτων ανά σελίδα (προαιρετικό, προεπιλογή: 10)</param>
    /// <response code="200">Επιστρέφει τις ειδήσεις</response>
    /// <response code="400">Άκυρη κατηγορία</response>
    /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(NewsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<NewsResponse>> GetNewsByCategory(
        string category,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting news for category: {Category}, page: {Page}, pageSize: {PageSize}", 
            category, page, pageSize);

        if (string.IsNullOrWhiteSpace(category))
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Category is required",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "category", Message = "Category cannot be empty" }
            });
        }

        if (page < 1)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid page number",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "page", Message = "Page number must be greater than 0" }
            });
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Invalid page size",
                ErrorCode = ErrorCodes.ValidationError,
                Details = new { Field = "pageSize", Message = "Page size must be between 1 and 100" }
            });
        }

        var news = await _insightsService.GetNewsByCategoryAsync(category, page, pageSize);
        return Ok(news);
    }
} 