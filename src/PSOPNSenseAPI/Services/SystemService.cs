using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense system API
    /// </summary>
    public class SystemService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public SystemService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Reboots the firewall
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<SystemRebootResponse> RebootAsync()
        {
            _logger.Information("Rebooting firewall");
            
            var endpoint = "core/system/reboot";
            return await _apiClient.PostAsync<SystemRebootResponse>(endpoint);
        }

        /// <summary>
        /// Gets the system status
        /// </summary>
        /// <returns>The system status</returns>
        public async Task<SystemStatusResponse> GetStatusAsync()
        {
            _logger.Information("Getting system status");
            
            var endpoint = "core/system/status";
            return await _apiClient.GetAsync<SystemStatusResponse>(endpoint);
        }

        /// <summary>
        /// Gets the system information
        /// </summary>
        /// <returns>The system information</returns>
        public async Task<SystemInfoResponse> GetInfoAsync()
        {
            _logger.Information("Getting system information");
            
            var endpoint = "core/system/info";
            return await _apiClient.GetAsync<SystemInfoResponse>(endpoint);
        }

        /// <summary>
        /// Halts the firewall
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<SystemHaltResponse> HaltAsync()
        {
            _logger.Information("Halting firewall");
            
            var endpoint = "core/system/halt";
            return await _apiClient.PostAsync<SystemHaltResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for rebooting the firewall
    /// </summary>
    public class SystemRebootResponse
    {
        /// <summary>
        /// Gets or sets the status of the reboot
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting the system status
    /// </summary>
    public class SystemStatusResponse
    {
        /// <summary>
        /// Gets or sets the system status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the system uptime
        /// </summary>
        [JsonProperty("uptime")]
        public string Uptime { get; set; }

        /// <summary>
        /// Gets or sets the system date
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the system time
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the system load
        /// </summary>
        [JsonProperty("load")]
        public string Load { get; set; }
    }

    /// <summary>
    /// Response for getting the system information
    /// </summary>
    public class SystemInfoResponse
    {
        /// <summary>
        /// Gets or sets the system product name
        /// </summary>
        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the system product version
        /// </summary>
        [JsonProperty("product_version")]
        public string ProductVersion { get; set; }

        /// <summary>
        /// Gets or sets the system product architecture
        /// </summary>
        [JsonProperty("product_arch")]
        public string ProductArchitecture { get; set; }

        /// <summary>
        /// Gets or sets the system platform
        /// </summary>
        [JsonProperty("platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the system CPU model
        /// </summary>
        [JsonProperty("cpu_model")]
        public string CpuModel { get; set; }

        /// <summary>
        /// Gets or sets the system CPU count
        /// </summary>
        [JsonProperty("cpu_count")]
        public string CpuCount { get; set; }

        /// <summary>
        /// Gets or sets the system memory
        /// </summary>
        [JsonProperty("memory")]
        public string Memory { get; set; }
    }

    /// <summary>
    /// Response for halting the firewall
    /// </summary>
    public class SystemHaltResponse
    {
        /// <summary>
        /// Gets or sets the status of the halt
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
