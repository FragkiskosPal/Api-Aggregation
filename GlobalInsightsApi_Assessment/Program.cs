using GlobalInsightsApi_Assessment.Clients;
using GlobalInsightsApi_Assessment.Middleware;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.Settings;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using GlobalInsightsApi_Assessment.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Settings (bind των sections από appsettings.json)
builder.Services.Configure<OpenWeatherSettings>(
    builder.Configuration.GetSection("OpenWeatherMap"));
builder.Services.Configure<NewsApiSettings>(
    builder.Configuration.GetSection("NewsApi"));
builder.Services.Configure<GitHubSettings>(
    builder.Configuration.GetSection("GitHub"));

// 2. Register HttpClients ως typed clients
builder.Services
    .AddHttpClient<IWeatherClient, WeatherClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services
    .AddHttpClient<INewsClient, NewsClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

builder.Services
    .AddHttpClient<IGitHubClient, GitHubClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));

// 3. Register Services
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IInsightsService, InsightsService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

// 4. Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 5. Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// 6. Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    // Στο Development ενεργοποιούμε το Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Αν χρειάζεσαι στατικά αρχεία (π.χ. wwwroot), τότε βάλ' το app.UseStaticFiles() εδώ.
// Για καθαρό API που απλώς τρέχει JSON endpoints, δεν είναι υποχρεωτικό.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
