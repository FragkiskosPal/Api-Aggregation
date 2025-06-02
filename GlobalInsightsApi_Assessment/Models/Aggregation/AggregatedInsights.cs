using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using GlobalInsightsApi_Assessment.Models_Settings.News;
using GlobalInsightsApi_Assessment.Models_Settings.Weather;

namespace GlobalInsightsApi_Assessment.Models.Aggregation;

/// <summary>
/// Αντιπροσωπεύει τα συγκεντρωτικά insights από όλες τις υπηρεσίες
/// </summary>
public class AggregatedInsights
{
    /// <summary>
    /// Τα καιρικά δεδομένα
    /// </summary>
    public WeatherResponse Weather { get; set; } = new();

    /// <summary>
    /// Τα δεδομένα ειδήσεων
    /// </summary>
    public NewsResponse News { get; set; } = new();

    /// <summary>
    /// Τα δεδομένα GitHub
    /// </summary>
    public GitHubResponse GitHub { get; set; } = new();

    /// <summary>
    /// Η χρονοσήμανση δημιουργίας των aggregated insights
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
} 