using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense system DNS API
    /// </summary>
    public class SystemDNSService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemDNSService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public SystemDNSService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets the system DNS configuration
        /// </summary>
        /// <returns>The system DNS configuration</returns>
        public async Task<SystemDNSResponse> GetSystemDNSAsync()
        {
            _logger.Information("Getting system DNS configuration");
            
            var endpoint = "system/settings/get";
            return await _apiClient.GetAsync<SystemDNSResponse>(endpoint);
        }

        /// <summary>
        /// Updates the system DNS configuration
        /// </summary>
        /// <param name="config">The updated system DNS configuration</param>
        /// <returns>The response indicating success</returns>
        public async Task<SystemDNSUpdateResponse> UpdateSystemDNSAsync(SystemDNSConfig config)
        {
            _logger.Information("Updating system DNS configuration");
            
            var endpoint = "system/settings/set";
            var data = new { system = config };
            return await _apiClient.PostAsync<SystemDNSUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Applies system DNS changes
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<SystemDNSApplyResponse> ApplySystemDNSChangesAsync()
        {
            _logger.Information("Applying system DNS changes");
            
            var endpoint = "system/settings/reconfigure";
            return await _apiClient.PostAsync<SystemDNSApplyResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting system DNS configuration
    /// </summary>
    public class SystemDNSResponse
    {
        /// <summary>
        /// Gets or sets the system DNS configuration
        /// </summary>
        [JsonProperty("system")]
        public SystemDNSConfig System { get; set; }
    }

    /// <summary>
    /// System DNS configuration
    /// </summary>
    public class SystemDNSConfig
    {
        /// <summary>
        /// Gets or sets the hostname
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the domain
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers
        /// </summary>
        [JsonProperty("dnsserver")]
        public List<string> DnsServers { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the DNS server options
        /// </summary>
        [JsonProperty("dnsallowoverride")]
        public string DnsAllowOverride { get; set; } = "1";

        /// <summary>
        /// Gets or sets whether to disable DNS forwarder
        /// </summary>
        [JsonProperty("disablenatreflection")]
        public string DisableNatReflection { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether to disable DNS rebinding protection
        /// </summary>
        [JsonProperty("dnssecstripped")]
        public string DnssecStripped { get; set; } = "0";

        /// <summary>
        /// Gets or sets the timezone
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        /// <summary>
        /// Gets or sets the time servers
        /// </summary>
        [JsonProperty("timeservers")]
        public string TimeServers { get; set; }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }
    }

    /// <summary>
    /// Response for updating system DNS configuration
    /// </summary>
    public class SystemDNSUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for applying system DNS changes
    /// </summary>
    public class SystemDNSApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
