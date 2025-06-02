namespace GlobalInsightsApi_Assessment.Models;

/// <summary>
/// Οι κωδικοί σφάλματος που χρησιμοποιούνται στην εφαρμογή
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    /// Σφάλμα επικύρωσης
    /// </summary>
    public const string ValidationError = "VALIDATION_ERROR";

    /// <summary>
    /// Σφάλμα εξωτερικής υπηρεσίας
    /// </summary>
    public const string ExternalServiceError = "EXTERNAL_SERVICE_ERROR";

    /// <summary>
    /// Σφάλμα εύρεσης πόρου
    /// </summary>
    public const string NotFoundError = "NOT_FOUND_ERROR";

    /// <summary>
    /// Σφάλμα εξουσιοδότησης
    /// </summary>
    public const string AuthorizationError = "AUTHORIZATION_ERROR";

    /// <summary>
    /// Σφάλμα διακομιστή
    /// </summary>
    public const string ServerError = "SERVER_ERROR";
} 