using GlobalInsightsApi_Assessment.Exceptions;

namespace GlobalInsightsApi_Assessment.Services;

public interface IValidationService
{
    /// <summary>
    /// Επικυρώνει το όνομα μιας πόλης
    /// </summary>
    /// <param name="city">Το όνομα της πόλης</param>
    /// <exception cref="ApiException">Όταν το όνομα της πόλης είναι άκυρο</exception>
    void ValidateCity(string city);

    /// <summary>
    /// Επικυρώνει ένα query αναζήτησης ειδήσεων
    /// </summary>
    /// <param name="query">Το query αναζήτησης</param>
    /// <exception cref="ApiException">Όταν το query είναι άκυρο</exception>
    void ValidateNewsQuery(string query);

    /// <summary>
    /// Επικυρώνει ένα GitHub username
    /// </summary>
    /// <param name="username">Το GitHub username</param>
    /// <exception cref="ApiException">Όταν το username είναι άκυρο</exception>
    void ValidateGitHubUsername(string username);

    /// <summary>
    /// Επικυρώνει τις συντεταγμένες γεωγραφικού πλάτους και μήκους
    /// </summary>
    /// <param name="latitude">Το γεωγραφικό πλάτος</param>
    /// <param name="longitude">Το γεωγραφικό μήκος</param>
    /// <exception cref="ApiException">Όταν οι συντεταγμένες είναι άκυρες</exception>
    void ValidateCoordinates(double latitude, double longitude);
} 