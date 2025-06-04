using GlobalInsightsApi_Assessment.Services;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace GlobalInsightsApi_Assessment.Tests.Services;

public class ValidationServiceTests
{
    private readonly IValidationService _validationService;
    private readonly Mock<ILogger<ValidationService>> _loggerMock;

    public ValidationServiceTests()
    {
        _loggerMock = new Mock<ILogger<ValidationService>>();
        _validationService = new ValidationService(_loggerMock.Object);
    }

    [Theory]
    [InlineData("Athens")]
    [InlineData("London")]
    [InlineData("New York")]
    public void ValidateCity_ValidCity_DoesNotThrowException(string city)
    {
        // Act & Assert
        var exception = Record.Exception(() => _validationService.ValidateCity(city));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ValidateCity_InvalidCity_ThrowsValidationException(string city)
    {
        // Act & Assert
        Assert.Throws<ValidationException>(() => _validationService.ValidateCity(city));
    }

    [Theory]
    [InlineData("technology")]
    [InlineData("business")]
    [InlineData("sports")]
    public void ValidateNewsCategory_ValidCategory_DoesNotThrowException(string category)
    {
        // Act & Assert
        var exception = Record.Exception(() => _validationService.ValidateNewsCategory(category));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("invalid-category")]
    public void ValidateNewsCategory_InvalidCategory_ThrowsValidationException(string category)
    {
        // Act & Assert
        Assert.Throws<ValidationException>(() => _validationService.ValidateNewsCategory(category));
    }

    [Theory]
    [InlineData("octocat")]
    [InlineData("microsoft")]
    [InlineData("dotnet")]
    public void ValidateGitHubUsername_ValidUsername_DoesNotThrowException(string username)
    {
        // Act & Assert
        var exception = Record.Exception(() => _validationService.ValidateGitHubUsername(username));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("user@name")]
    [InlineData("user name")]
    public void ValidateGitHubUsername_InvalidUsername_ThrowsValidationException(string username)
    {
        // Act & Assert
        Assert.Throws<ValidationException>(() => _validationService.ValidateGitHubUsername(username));
    }

    [Theory]
    [InlineData("valid query")]
    [InlineData("technology news")]
    [InlineData("weather forecast")]
    public void ValidateNewsQuery_ValidQuery_DoesNotThrowException(string query)
    {
        // Act & Assert
        var exception = Record.Exception(() => _validationService.ValidateNewsQuery(query));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ValidateNewsQuery_InvalidQuery_ThrowsValidationException(string query)
    {
        // Act & Assert
        Assert.Throws<ValidationException>(() => _validationService.ValidateNewsQuery(query));
    }
} 