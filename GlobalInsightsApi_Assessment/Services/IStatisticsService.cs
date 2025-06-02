using GlobalInsightsApi_Assessment.Models;

namespace GlobalInsightsApi_Assessment.Services
{
    /// <summary>
    /// Interface για την υπηρεσία στατιστικών
    /// </summary>
    public interface IStatisticsService
    {
        /// <summary>
        /// Καταγράφει μια κλήση API
        /// </summary>
        /// <param name="apiName">Το όνομα του API</param>
        /// <param name="responseTimeMs">Ο χρόνος απόκρισης σε milliseconds</param>
        /// <param name="isSuccess">Αν η κλήση ήταν επιτυχής</param>
        void Record(string apiName, long responseTimeMs, bool isSuccess);

        /// <summary>
        /// Επιστρέφει τα στατιστικά για ένα API
        /// </summary>
        /// <param name="apiName">Το όνομα του API</param>
        /// <returns>Τα στατιστικά του API</returns>
        ApiStatsDto GetStats(string apiName);
    }
} 