using System;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Singleton class to maintain the OPNSense session state
    /// </summary>
    public class OPNSenseSessionState
    {
        private static readonly Lazy<OPNSenseSessionState> _instance = new Lazy<OPNSenseSessionState>(() => new OPNSenseSessionState());

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        public static OPNSenseSessionState Instance => _instance.Value;

        /// <summary>
        /// Gets or sets the API client
        /// </summary>
        public OPNSenseApiClient ApiClient { get; set; }

        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private OPNSenseSessionState()
        {
        }
    }
}
