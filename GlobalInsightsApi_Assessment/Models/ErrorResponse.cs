namespace GlobalInsightsApi_Assessment.Models;

/// <summary>
/// Αντιπροσωπεύει μια τυποποιημένη απάντηση σφάλματος
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Το μήνυμα σφάλματος
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Ο κωδικός σφάλματος
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Περισσότερες λεπτομέρειες για το σφάλμα
    /// </summary>
    public object? Details { get; set; }

    /// <summary>
    /// Η χρονοσήμανση του σφάλματος
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
} 