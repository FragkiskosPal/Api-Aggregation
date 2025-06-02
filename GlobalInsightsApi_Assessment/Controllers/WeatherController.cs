// Controllers/WeatherController.cs
using GlobalInsightsApi_Assessment.Models;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using GlobalInsightsApi_Assessment.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalInsightsApi_Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherController : ControllerBase
    {
        private readonly IInsightsService _insightsService;
        private readonly IValidationService _validationService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(
            IInsightsService insightsService,
            IValidationService validationService,
            ILogger<WeatherController> logger)
        {
            _insightsService = insightsService;
            _validationService = validationService;
            _logger = logger;
        }

        /// <summary>
        /// Ανακτά τα τρέχοντα καιρικά δεδομένα για μια πόλη
        /// </summary>
        /// <param name="city">Το όνομα της πόλης</param>
        /// <response code="200">Επιστρέφει τα καιρικά δεδομένα</response>
        /// <response code="400">Άκυρο όνομα πόλης</response>
        /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
        [HttpGet("current/{city}")]
        [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<WeatherResponse>> GetCurrentWeather(string city)
        {
            _logger.LogInformation("Getting current weather for city: {City}", city);
            
            _validationService.ValidateCity(city);
            var weather = await _insightsService.GetWeatherInsightsAsync(city);
            
            return Ok(weather);
        }

        /// <summary>
        /// Ανακτά ιστορικά καιρικά δεδομένα για συγκεκριμένες συντεταγμένες και ημερομηνία
        /// </summary>
        /// <param name="lat">Γεωγραφικό πλάτος</param>
        /// <param name="lon">Γεωγραφικό μήκος</param>
        /// <param name="date">Ημερομηνία (UTC)</param>
        /// <response code="200">Επιστρέφει τα ιστορικά καιρικά δεδομένα</response>
        /// <response code="400">Άκυρες συντεταγμένες ή ημερομηνία</response>
        /// <response code="500">Σφάλμα εξωτερικής υπηρεσίας</response>
        [HttpGet("historical")]
        [ProducesResponseType(typeof(HistoricalWeatherData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HistoricalWeatherData>> GetHistoricalWeather(
            [FromQuery] double lat,
            [FromQuery] double lon,
            [FromQuery] DateTime date)
        {
            _logger.LogInformation("Getting historical weather for coordinates: {Lat}, {Lon} at {Date}", lat, lon, date);

            _validationService.ValidateCoordinates(lat, lon);
            
            if (date > DateTime.UtcNow)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Date cannot be in the future",
                    ErrorCode = "VALIDATION_ERROR",
                    Details = new { Field = "date", Message = "Historical weather data is only available for past dates" }
                });
            }

            var weather = await _insightsService.GetHistoricalWeatherAsync(lat, lon, date);
            return Ok(weather);
        }
    }
}
