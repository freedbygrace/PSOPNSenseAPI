using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Base class for all services
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Gets the API client
        /// </summary>
        protected OPNSenseApiClient ApiClient { get; }

        /// <summary>
        /// Gets the logger
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class
        /// </summary>
        /// <param name="apiClient">The API client to use for requests</param>
        protected ServiceBase(OPNSenseApiClient apiClient)
        {
            ApiClient = apiClient;
            Logger = new NullLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class
        /// </summary>
        /// <param name="apiClient">The API client to use for requests</param>
        /// <param name="logger">The logger to use</param>
        protected ServiceBase(OPNSenseApiClient apiClient, ILogger logger)
        {
            ApiClient = apiClient;
            Logger = logger ?? new NullLogger();
        }
    }
}
