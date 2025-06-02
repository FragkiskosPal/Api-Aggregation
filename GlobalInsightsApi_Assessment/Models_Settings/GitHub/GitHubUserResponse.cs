using System.Text.Json.Serialization;

namespace GlobalInsightsApi_Assessment.Models_Settings.GitHub;

/// <summary>
/// Αντιπροσωπεύει την απάντηση από το GitHub API για έναν χρήστη
/// </summary>
public class GitHubUserResponse
{
    /// <summary>
    /// Το login όνομα του χρήστη
    /// </summary>
    [JsonPropertyName("login")]
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Το ID του χρήστη
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Το URL του avatar του χρήστη
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; } = string.Empty;

    /// <summary>
    /// Το URL του προφίλ του χρήστη
    /// </summary>
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    /// <summary>
    /// Ο αριθμός των public repositories
    /// </summary>
    [JsonPropertyName("public_repos")]
    public int PublicRepos { get; set; }

    /// <summary>
    /// Ο αριθμός των followers
    /// </summary>
    [JsonPropertyName("followers")]
    public int Followers { get; set; }

    /// <summary>
    /// Ο αριθμός των following
    /// </summary>
    [JsonPropertyName("following")]
    public int Following { get; set; }

    /// <summary>
    /// Η ημερομηνία δημιουργίας του λογαριασμού
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Η τελευταία ενημέρωση του προφίλ
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
} 