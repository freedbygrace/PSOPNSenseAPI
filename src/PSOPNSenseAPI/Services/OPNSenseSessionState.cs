using System;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Singleton class to maintain the OPNSense session state
    /// </summary>
    public class OPNSenseSessionState
    {
        private static readonly Lazy<OPNSenseSessionState> _instance = new Lazy<OPNSenseSessionState>(() => new OPNSenseSessionState());
        private OPNSenseApiEndpoints _apiEndpoints;

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static OPNSenseSessionState Instance => _instance.Value;

        /// <summary>
        /// Gets or sets the API client
        /// </summary>
        public OPNSenseApiClient ApiClient { get; set; }

        /// <summary>
        /// Gets the API endpoints
        /// </summary>
        public OPNSenseApiEndpoints ApiEndpoints
        {
            get
            {
                if (_apiEndpoints == null && ApiClient != null)
                {
                    _apiEndpoints = new OPNSenseApiEndpoints(ApiClient, new NullLogger());
                }
                return _apiEndpoints;
            }
        }



        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private OPNSenseSessionState()
        {
        }

        /// <summary>
        /// Resets the API endpoints
        /// </summary>
        public void ResetApiEndpoints()
        {
            if (ApiClient != null)
            {
                _apiEndpoints = new OPNSenseApiEndpoints(ApiClient, new NullLogger());
            }
            else
            {
                _apiEndpoints = null;
            }
        }
    }
}

