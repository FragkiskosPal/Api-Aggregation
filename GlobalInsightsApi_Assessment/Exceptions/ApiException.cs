namespace GlobalInsightsApi_Assessment.Exceptions;

public class ApiException : Exception
{
    public int StatusCode { get; }
    public string ErrorCode { get; }
    public object? Details { get; }

    public ApiException(string message, int statusCode = 500, string errorCode = "INTERNAL_ERROR", object? details = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Details = details;
    }

    public ApiException(string message, Exception innerException, int statusCode = 500, string errorCode = "INTERNAL_ERROR", object? details = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Details = details;
    }
}

public static class ErrorCodes
{
    public const string ValidationError = "VALIDATION_ERROR";
    public const string NotFound = "NOT_FOUND";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Forbidden = "FORBIDDEN";
    public const string RateLimitExceeded = "RATE_LIMIT_EXCEEDED";
    public const string ExternalApiError = "EXTERNAL_API_ERROR";
    public const string InternalError = "INTERNAL_ERROR";
} 