using System.Text.Json.Serialization;

namespace GlobalInsightsApi_Assessment.Models_Settings.News;

/// <summary>
/// Αντιπροσωπεύει την απάντηση από το News API
/// </summary>
public class NewsResponse
{
    /// <summary>
    /// Η κατάσταση της απάντησης
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Ο συνολικός αριθμός των αποτελεσμάτων
    /// </summary>
    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }

    /// <summary>
    /// Τα άρθρα ειδήσεων
    /// </summary>
    [JsonPropertyName("articles")]
    public List<Article> Articles { get; set; } = new();

    /// <summary>
    /// Το query αναζήτησης
    /// </summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// Η χρονοσήμανση της απάντησης
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Αντιπροσωπεύει ένα άρθρο ειδήσεων
/// </summary>
public class Article
{
    /// <summary>
    /// Η πηγή της είδησης
    /// </summary>
    [JsonPropertyName("source")]
    public Source Source { get; set; } = new();

    /// <summary>
    /// Ο συγγραφέας του άρθρου
    /// </summary>
    [JsonPropertyName("author")]
    public string? Author { get; set; }

    /// <summary>
    /// Ο τίτλος του άρθρου
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Η περιγραφή του άρθρου
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Το URL του άρθρου
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Το URL της εικόνας του άρθρου
    /// </summary>
    [JsonPropertyName("urlToImage")]
    public string? UrlToImage { get; set; }

    /// <summary>
    /// Η ημερομηνία δημοσίευσης
    /// </summary>
    [JsonPropertyName("publishedAt")]
    public DateTime PublishedAt { get; set; }

    /// <summary>
    /// Το περιεχόμενο του άρθρου
    /// </summary>
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}

/// <summary>
/// Αντιπροσωπεύει την πηγή μιας είδησης
/// </summary>
public class Source
{
    /// <summary>
    /// Το ID της πηγής
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Το όνομα της πηγής
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Αντιπροσωπεύει τις ρυθμίσεις του News API
/// </summary>
public class NewsApiSettings
{
    /// <summary>
    /// Το βασικό URL του API
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Το API key
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
} 