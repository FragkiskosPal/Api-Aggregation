using System.Text.Json.Serialization;

namespace GlobalInsightsApi_Assessment.Models_Settings.GitHub;

/// <summary>
/// Αντιπροσωπεύει ένα GitHub repository
/// </summary>
public class Repository
{
    /// <summary>
    /// Το όνομα του repository
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Η περιγραφή του repository
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Το URL του repository
    /// </summary>
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    /// <summary>
    /// Ο αριθμός των stars
    /// </summary>
    [JsonPropertyName("stargazers_count")]
    public int Stars { get; set; }

    /// <summary>
    /// Ο αριθμός των forks
    /// </summary>
    [JsonPropertyName("forks_count")]
    public int Forks { get; set; }

    /// <summary>
    /// Η κύρια γλώσσα προγραμματισμού
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// Αν το repository είναι fork
    /// </summary>
    [JsonPropertyName("fork")]
    public bool IsFork { get; set; }

    /// <summary>
    /// Η ημερομηνία δημιουργίας του repository
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Η τελευταία ενημέρωση του repository
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
} 