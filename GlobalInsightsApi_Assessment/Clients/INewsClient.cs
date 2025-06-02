using GlobalInsightsApi_Assessment.Models_Settings.News;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalInsightsApi_Assessment.Clients;

/// <summary>
/// Interface για το News API client
/// </summary>
public interface INewsClient
{
    /// <summary>
    /// Ανακτά ειδήσεις βάσει ενός query
    /// </summary>
    /// <param name="query">Το query αναζήτησης</param>
    /// <param name="ct">CancellationToken</param>
    /// <returns>Τα αποτελέσματα της αναζήτησης ειδήσεων</returns>
    Task<NewsResponse> GetNewsAsync(string query, CancellationToken ct = default);
}
