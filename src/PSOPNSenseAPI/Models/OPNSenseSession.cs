using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Models
{
    /// <summary>
    /// Represents a session with an OPNSense firewall
    /// </summary>
    public static class OPNSenseSession
    {
        /// <summary>
        /// Gets or sets the current API client
        /// </summary>
        public static OPNSenseApiClient Current { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is an active connection
        /// </summary>
        public static bool IsConnected => Current != null && Current.IsConnected;

        /// <summary>
        /// Gets the base URL of the current connection
        /// </summary>
        public static string BaseUrl => Current?.BaseUrl;
    }
}
