using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GlobalInsightsApi_Assessment.Clients;
using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using GlobalInsightsApi_Assessment.Services;
using Moq;
using Xunit;

namespace GlobalInsightsApi_Assessment.Tests
{
    public class InsightsServiceTests
    {
        private readonly Mock<IWeatherClient> _mockWeatherClient;
        private readonly Mock<INewsClient> _mockNewsClient;
        private readonly Mock<IGitHubClient> _mockGitHubClient;
        private readonly IInsightsService _insightsService;

        public InsightsServiceTests()
        {
            _mockWeatherClient = new Mock<IWeatherClient>();
            _mockNewsClient = new Mock<INewsClient>();
            _mockGitHubClient = new Mock<IGitHubClient>();
            _insightsService = new InsightsService(_mockWeatherClient.Object, _mockNewsClient.Object, _mockGitHubClient.Object);
        }

        [Fact]
        public async Task GetAggregatedInsightsAsync_ShouldAggregateDataFromAllClients()
        {
            // Arrange
            var city = "Athens";
            var query = "test";
            var username = "testuser";

            var weatherResponse = new WeatherResponse { City = city, Temperature = 25, Humidity = 60, WindSpeed = 5.2, FeelsLike = 26 };
            var newsResponse = new NewsResponse
            {
                Articles = new List<Article>
                {
                    new Article
                    {
                        Title = "Sample News",
                        Source = new Source { Name = "NewsAPI" },
                        PublishedAt = DateTime.UtcNow
                    }
                }
            };
            var gitHubResponse = new GitHubResponse
            {
                Login = username,
                PublicRepos = 1
            };

            _mockWeatherClient
                .Setup(x => x.GetWeatherAsync(city, It.IsAny<CancellationToken>()))
                .ReturnsAsync(weatherResponse);
            _mockNewsClient
                .Setup(x => x.GetNewsAsync(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newsResponse);
            _mockGitHubClient
                .Setup(x => x.GetUserInfoAsync(username, It.IsAny<CancellationToken>()))
                .ReturnsAsync(gitHubResponse);

            // Act
            var result = await _insightsService.GetAggregatedInsightsAsync(city, query, username);

            // Assert (using FluentAssertions)
            result.Should().NotBeNull();
            result.Weather.Should().BeEquivalentTo(weatherResponse);
            result.News.Should().BeEquivalentTo(newsResponse.Articles);
            result.GitHub.Should().BeEquivalentTo(gitHubResponse);
        }

        [Fact]
        public async Task GetWeatherInsightsAsync_ShouldReturnWeatherData()
        {
            // Arrange
            var city = "Athens";
            var weatherResponse = new WeatherResponse { City = city, Temperature = 25, Humidity = 60, WindSpeed = 5.2, FeelsLike = 26 };
            _mockWeatherClient
                .Setup(x => x.GetWeatherAsync(city, It.IsAny<CancellationToken>()))
                .ReturnsAsync(weatherResponse);

            // Act
            var result = await _insightsService.GetWeatherInsightsAsync(city);

            // Assert
            result.Should().BeEquivalentTo(weatherResponse);
        }

        [Fact]
        public async Task GetNewsInsightsAsync_ShouldReturnNewsData()
        {
            // Arrange
            var query = "test";
            var newsResponse = new NewsResponse
            {
                Articles = new List<Article>
                {
                    new Article
                    {
                        Title = "Sample News",
                        Source = new Source { Name = "NewsAPI" },
                        PublishedAt = DateTime.UtcNow
                    }
                }
            };
            _mockNewsClient
                .Setup(x => x.GetNewsAsync(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newsResponse);

            // Act
            var result = await _insightsService.GetNewsInsightsAsync(query);

            // Assert
            result.Should().BeEquivalentTo(newsResponse);
        }

        [Fact]
        public async Task GetGitHubInsightsAsync_ShouldReturnGitHubData()
        {
            // Arrange
            var username = "testuser";
            var gitHubResponse = new GitHubResponse
            {
                Login = username,
                PublicRepos = 1
            };
            _mockGitHubClient
                .Setup(x => x.GetUserInfoAsync(username, It.IsAny<CancellationToken>()))
                .ReturnsAsync(gitHubResponse);

            // Act
            var result = await _insightsService.GetGitHubInsightsAsync(username);

            // Assert
            result.Should().BeEquivalentTo(gitHubResponse);
        }

        // (Optional) Test for fallback (e.g. if one client throws an exception, aggregation still returns partial data)
        [Fact]
        public async Task GetAggregatedInsightsAsync_WhenOneClientFails_ShouldReturnPartialData()
        {
            // Arrange
            var city = "Athens";
            var query = "test";
            var username = "testuser";

            var weatherResponse = new WeatherResponse { City = city, Temperature = 25, Humidity = 60, WindSpeed = 5.2, FeelsLike = 26 };
            var newsResponse = new NewsResponse
            {
                Articles = new List<Article>
                {
                    new Article
                    {
                        Title = "Sample News",
                        Source = new Source { Name = "NewsAPI" },
                        PublishedAt = DateTime.UtcNow
                    }
                }
            };
            // Simulate GitHub client throwing an exception
            _mockWeatherClient
                .Setup(x => x.GetWeatherAsync(city, It.IsAny<CancellationToken>()))
                .ReturnsAsync(weatherResponse);
            _mockNewsClient
                .Setup(x => x.GetNewsAsync(query, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newsResponse);
            _mockGitHubClient
                .Setup(x => x.GetUserInfoAsync(username, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("GitHub API error"));

            // Act
            var result = await _insightsService.GetAggregatedInsightsAsync(city, query, username);

            // Assert (partial data is returned)
            result.Should().NotBeNull();
            result.Weather.Should().BeEquivalentTo(weatherResponse);
            result.News.Should().BeEquivalentTo(newsResponse.Articles);
            result.GitHub.Should().BeNull(); // (or a fallback value, depending on your implementation)
        }
    }
} 