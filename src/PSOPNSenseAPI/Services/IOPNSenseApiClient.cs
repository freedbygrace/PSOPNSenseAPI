using System;
using System.Threading.Tasks;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Interface for interacting with the OPNSense API
    /// </summary>
    public interface IOPNSenseApiClient : IDisposable
    {
        /// <summary>
        /// Gets the base URL of the OPNSense API
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Gets the API key
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Gets the API secret
        /// </summary>
        string ApiSecret { get; }

        /// <summary>
        /// Sends a GET request to the OPNSense API
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response to</typeparam>
        /// <param name="endpoint">The API endpoint</param>
        /// <returns>The deserialized response</returns>
        Task<T> GetAsync<T>(string endpoint);

        /// <summary>
        /// Sends a POST request to the OPNSense API
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response to</typeparam>
        /// <param name="endpoint">The API endpoint</param>
        /// <param name="data">The data to send</param>
        /// <returns>The deserialized response</returns>
        Task<T> PostAsync<T>(string endpoint, object data = null);
    }
}
