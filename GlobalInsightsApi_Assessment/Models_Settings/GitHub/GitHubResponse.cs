using System.Text.Json.Serialization;

namespace GlobalInsightsApi_Assessment.Models_Settings.GitHub;

/// <summary>
/// Αντιπροσωπεύει την απάντηση από το GitHub API για πληροφορίες χρήστη
/// </summary>
public class GitHubResponse
{
    /// <summary>
    /// Το login name του χρήστη
    /// </summary>
    [JsonPropertyName("login")]
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Το ID του χρήστη
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Το avatar URL του χρήστη
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

    /// <summary>
    /// Η χρονοσήμανση της απάντησης
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Αντιπροσωπεύει την απάντηση από το GitHub API για τα repositories ενός χρήστη
/// </summary>
public class GitHubRepoResponse
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

/// <summary>
/// Αντιπροσωπεύει την απάντηση από το GitHub API για τις συνεισφορές ενός χρήστη
/// </summary>
public class GitHubContributionsResponse
{
    /// <summary>
    /// Το username του χρήστη
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Το έτος των συνεισφορών
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Ο συνολικός αριθμός των συνεισφορών
    /// </summary>
    public int TotalContributions { get; set; }

    /// <summary>
    /// Οι συνεισφορές ανά ημέρα
    /// </summary>
    public List<ContributionDay> Contributions { get; set; } = new();

    /// <summary>
    /// Η χρονοσήμανση της απάντησης
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Αντιπροσωπεύει τις συνεισφορές μιας ημέρας
/// </summary>
public class ContributionDay
{
    /// <summary>
    /// Η ημερομηνία
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Ο αριθμός των συνεισφορών
    /// </summary>
    public int Count { get; set; }
} 