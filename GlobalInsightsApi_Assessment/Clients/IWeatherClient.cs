// Clients/IWeatherClient.cs
using GlobalInsightsApi_Assessment.Models_Settings.Weather;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GlobalInsightsApi_Assessment.Clients
{
    /// <summary>
    /// Ορίζει τη μέθοδο που φέρνει τα ιστορικά δεδομένα καιρού (One Call 3.0 Time Machine).
    /// </summary>
    public interface IWeatherClient
    {
        /// <summary>
        /// Ανακτά τα ιστορικά δεδομένα για μια συγκεκριμένη ημερομηνία (UTC), latitude και longitude.
        /// </summary>
        /// <param name="lat">Latitude (π.χ. 39.099724).</param>
        /// <param name="lon">Longitude (π.χ. -94.578331).</param>
        /// <param name="dateUtc">Ημερομηνία UTC (π.χ. 2022-02-02T00:00:00Z).</param>
        /// <param name="ct">CancellationToken.</param>
        /// <returns>Ένα αντικείμενο HistoricalWeatherData που περιέχει το current + hourly snapshots.</returns>
        Task<HistoricalWeatherData> FetchHistoricalAsync(
            double lat,
            double lon,
            DateTime dateUtc,
            CancellationToken ct = default);

        /// <summary>
        /// Ανακτά τα τρέχοντα καιρικά δεδομένα για μια πόλη
        /// </summary>
        /// <param name="city">Το όνομα της πόλης</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Τα τρέχοντα καιρικά δεδομένα</returns>
        Task<WeatherResponse> GetWeatherAsync(string city, CancellationToken ct = default);
    }
}
