namespace GlobalInsightsApi_Assessment.Models;

/// <summary>
/// Data Transfer Object για τα στατιστικά των API
/// </summary>
public class ApiStatsDto
{
    /// <summary>
    /// Το όνομα του API
    /// </summary>
    public string ApiName { get; set; } = string.Empty;

    /// <summary>
    /// Ο αριθμός των επιτυχών κλήσεων
    /// </summary>
    public int SuccessfulCalls { get; set; }

    /// <summary>
    /// Ο αριθμός των αποτυχημένων κλήσεων
    /// </summary>
    public int FailedCalls { get; set; }

    /// <summary>
    /// Ο μέσος χρόνος απόκρισης σε milliseconds
    /// </summary>
    public double AverageResponseTime { get; set; }

    /// <summary>
    /// Ο συνολικός αριθμός των κλήσεων
    /// </summary>
    public int TotalCalls => SuccessfulCalls + FailedCalls;

    /// <summary>
    /// Το ποσοστό επιτυχίας
    /// </summary>
    public double SuccessRate => TotalCalls == 0 ? 0 : (double)SuccessfulCalls / TotalCalls * 100;
} 