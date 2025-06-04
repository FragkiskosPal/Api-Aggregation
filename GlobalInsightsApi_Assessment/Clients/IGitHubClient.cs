using GlobalInsightsApi_Assessment.Models_Settings.GitHub;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GlobalInsightsApi_Assessment.Clients;

/// <summary>
/// Interface για το GitHub API client
/// </summary>
public interface IGitHubClient
{
    /// <summary>
    /// Ανακτά πληροφορίες για ένα GitHub χρήστη
    /// </summary>
    /// <param name="username">Το username του GitHub χρήστη</param>
    /// <param name="ct">CancellationToken</param>
    /// <returns>Οι πληροφορίες του χρήστη</returns>
    Task<GitHubResponse> GetUserInfoAsync(string username, CancellationToken ct = default);

    Task<List<GitHubRepoResponse>> GetUserReposAsync(
        string username,
        int page = 1,
        int perPage = 5,
        CancellationToken ct = default);
}
