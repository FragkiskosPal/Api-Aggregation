using System.Threading.Tasks;
using FluentAssertions;
using GlobalInsightsApi_Assessment.Controllers;
using GlobalInsightsApi_Assessment.Models;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GlobalInsightsApi_Assessment.Tests
{
    public class InsightsControllerTests
    {
        private readonly Mock<IInsightsService> _mockInsightsService;
        private readonly InsightsController _controller;

        public InsightsControllerTests()
        {
            _mockInsightsService = new Mock<IInsightsService>();
            _controller = new InsightsController(_mockInsightsService.Object);
        }

        [Fact]
        public async Task GetAggregatedInsightsAsync_WithValidInput_ShouldReturnOkWithAggregatedData()
        {
            // Arrange
            var city = "Athens";
            var query = "news";
            var username = "testuser";
            var expectedAggregatedData = new AggregatedInsights { /* populate as needed */ };
            _mockInsightsService.Setup(s => s.GetAggregatedInsightsAsync(city, query, username)).ReturnsAsync(expectedAggregatedData);

            // Act
            var result = await _controller.GetAggregatedInsightsAsync(city, query, username);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedAggregatedData);
        }

        [Fact]
        public async Task GetAggregatedInsightsAsync_WithMissingRequiredInput_ShouldReturnBadRequest()
        {
            // Arrange: missing city (or query or username)
            string city = null;
            var query = "news";
            var username = "testuser";

            // Act
            var result = await _controller.GetAggregatedInsightsAsync(city, query, username);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result;
            badRequestResult.Value.Should().Be("City, query, and username are required.");
        }

        [Fact]
        public async Task GetAggregatedInsightsAsync_WhenAggregatedDataIsNull_ShouldReturnNotFound()
        {
            // Arrange
            var city = "Athens";
            var query = "news";
            var username = "testuser";
            AggregatedInsights nullData = null;
            _mockInsightsService.Setup(s => s.GetAggregatedInsightsAsync(city, query, username)).ReturnsAsync(nullData);

            // Act
            var result = await _controller.GetAggregatedInsightsAsync(city, query, username);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = (NotFoundObjectResult)result;
            notFoundResult.Value.Should().Be("Aggregation failed (or no data available).");
        }

        [Fact]
        public async Task GetWeatherInsightsAsync_WithValidCity_ShouldReturnOkWithWeatherData()
        {
            // Arrange
            var city = "Athens";
            var expectedWeatherData = new WeatherResponse { /* populate as needed */ };
            _mockInsightsService.Setup(s => s.GetWeatherInsightsAsync(city)).ReturnsAsync(expectedWeatherData);

            // Act
            var result = await _controller.GetWeatherInsightsAsync(city);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedWeatherData);
        }

        [Fact]
        public async Task GetWeatherInsightsAsync_WithMissingCity_ShouldReturnBadRequest()
        {
            // Arrange: missing city
            string city = null;

            // Act
            var result = await _controller.GetWeatherInsightsAsync(city);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result;
            badRequestResult.Value.Should().Be("City is required.");
        }

        [Fact]
        public async Task GetNewsInsightsAsync_WithValidQuery_ShouldReturnOkWithNewsData()
        {
            // Arrange
            var query = "news";
            var expectedNewsData = new NewsResponse { /* populate as needed */ };
            _mockInsightsService.Setup(s => s.GetNewsInsightsAsync(query)).ReturnsAsync(expectedNewsData);

            // Act
            var result = await _controller.GetNewsInsightsAsync(query);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedNewsData);
        }

        [Fact]
        public async Task GetNewsInsightsAsync_WithMissingQuery_ShouldReturnBadRequest()
        {
            // Arrange: missing query
            string query = null;

            // Act
            var result = await _controller.GetNewsInsightsAsync(query);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result;
            badRequestResult.Value.Should().Be("Query is required.");
        }

        [Fact]
        public async Task GetGitHubInsightsAsync_WithValidUsername_ShouldReturnOkWithGitHubData()
        {
            // Arrange
            var username = "testuser";
            var expectedGitHubData = new GitHubResponse { /* populate as needed */ };
            _mockInsightsService.Setup(s => s.GetGitHubInsightsAsync(username)).ReturnsAsync(expectedGitHubData);

            // Act
            var result = await _controller.GetGitHubInsightsAsync(username);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().Be(expectedGitHubData);
        }

        [Fact]
        public async Task GetGitHubInsightsAsync_WithMissingUsername_ShouldReturnBadRequest()
        {
            // Arrange: missing username
            string username = null;

            // Act
            var result = await _controller.GetGitHubInsightsAsync(username);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = (BadRequestObjectResult)result;
            badRequestResult.Value.Should().Be("Username is required.");
        }
    }
} 