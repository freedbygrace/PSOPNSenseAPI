using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense firmware API
    /// </summary>
    public class FirmwareService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirmwareService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public FirmwareService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets the firmware status
        /// </summary>
        /// <returns>The firmware status</returns>
        public async Task<FirmwareStatusResponse> GetStatusAsync()
        {
            _logger.Information("Getting firmware status");
            
            var endpoint = "core/firmware/status";
            return await _apiClient.GetAsync<FirmwareStatusResponse>(endpoint);
        }

        /// <summary>
        /// Checks for firmware updates
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<FirmwareCheckResponse> CheckForUpdatesAsync()
        {
            _logger.Information("Checking for firmware updates");
            
            var endpoint = "core/firmware/check";
            return await _apiClient.PostAsync<FirmwareCheckResponse>(endpoint);
        }

        /// <summary>
        /// Updates the firmware
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<FirmwareUpdateResponse> UpdateAsync()
        {
            _logger.Information("Updating firmware");
            
            var endpoint = "core/firmware/update";
            return await _apiClient.PostAsync<FirmwareUpdateResponse>(endpoint);
        }

        /// <summary>
        /// Upgrades the firmware
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<FirmwareUpgradeResponse> UpgradeAsync()
        {
            _logger.Information("Upgrading firmware");
            
            var endpoint = "core/firmware/upgrade";
            return await _apiClient.PostAsync<FirmwareUpgradeResponse>(endpoint);
        }

        /// <summary>
        /// Gets the firmware changelog
        /// </summary>
        /// <returns>The firmware changelog</returns>
        public async Task<FirmwareChangelogResponse> GetChangelogAsync()
        {
            _logger.Information("Getting firmware changelog");
            
            var endpoint = "core/firmware/changelog";
            return await _apiClient.GetAsync<FirmwareChangelogResponse>(endpoint);
        }

        /// <summary>
        /// Gets the firmware audit
        /// </summary>
        /// <returns>The firmware audit</returns>
        public async Task<FirmwareAuditResponse> GetAuditAsync()
        {
            _logger.Information("Getting firmware audit");
            
            var endpoint = "core/firmware/audit";
            return await _apiClient.GetAsync<FirmwareAuditResponse>(endpoint);
        }

        /// <summary>
        /// Gets the firmware health
        /// </summary>
        /// <returns>The firmware health</returns>
        public async Task<FirmwareHealthResponse> GetHealthAsync()
        {
            _logger.Information("Getting firmware health");
            
            var endpoint = "core/firmware/health";
            return await _apiClient.GetAsync<FirmwareHealthResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting firmware status
    /// </summary>
    public class FirmwareStatusResponse
    {
        /// <summary>
        /// Gets or sets the status of the firmware
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the log of the firmware
        /// </summary>
        [JsonProperty("log")]
        public string Log { get; set; }

        /// <summary>
        /// Gets or sets the upgrade message
        /// </summary>
        [JsonProperty("upgrade_message")]
        public string UpgradeMessage { get; set; }

        /// <summary>
        /// Gets or sets the connection status
        /// </summary>
        [JsonProperty("connection")]
        public string Connection { get; set; }

        /// <summary>
        /// Gets or sets the download size
        /// </summary>
        [JsonProperty("download_size")]
        public string DownloadSize { get; set; }

        /// <summary>
        /// Gets or sets the last check
        /// </summary>
        [JsonProperty("last_check")]
        public string LastCheck { get; set; }

        /// <summary>
        /// Gets or sets the updates
        /// </summary>
        [JsonProperty("updates")]
        public List<FirmwareUpdate> Updates { get; set; }
    }

    /// <summary>
    /// Firmware update information
    /// </summary>
    public class FirmwareUpdate
    {
        /// <summary>
        /// Gets or sets the name of the update
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version of the update
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the comment of the update
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the repository of the update
        /// </summary>
        [JsonProperty("repository")]
        public string Repository { get; set; }

        /// <summary>
        /// Gets or sets the origin of the update
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the size of the update
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }
    }

    /// <summary>
    /// Response for checking for firmware updates
    /// </summary>
    public class FirmwareCheckResponse
    {
        /// <summary>
        /// Gets or sets the status of the check
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for updating firmware
    /// </summary>
    public class FirmwareUpdateResponse
    {
        /// <summary>
        /// Gets or sets the status of the update
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for upgrading firmware
    /// </summary>
    public class FirmwareUpgradeResponse
    {
        /// <summary>
        /// Gets or sets the status of the upgrade
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting firmware changelog
    /// </summary>
    public class FirmwareChangelogResponse
    {
        /// <summary>
        /// Gets or sets the changelog
        /// </summary>
        [JsonProperty("changelog")]
        public string Changelog { get; set; }
    }

    /// <summary>
    /// Response for getting firmware audit
    /// </summary>
    public class FirmwareAuditResponse
    {
        /// <summary>
        /// Gets or sets the audit
        /// </summary>
        [JsonProperty("audit")]
        public List<FirmwareAuditItem> Audit { get; set; }
    }

    /// <summary>
    /// Firmware audit item
    /// </summary>
    public class FirmwareAuditItem
    {
        /// <summary>
        /// Gets or sets the name of the audit item
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version of the audit item
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the installed version of the audit item
        /// </summary>
        [JsonProperty("installed")]
        public string Installed { get; set; }

        /// <summary>
        /// Gets or sets the vulnerabilities of the audit item
        /// </summary>
        [JsonProperty("vulnerabilities")]
        public List<FirmwareVulnerability> Vulnerabilities { get; set; }
    }

    /// <summary>
    /// Firmware vulnerability
    /// </summary>
    public class FirmwareVulnerability
    {
        /// <summary>
        /// Gets or sets the name of the vulnerability
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the vulnerability
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the CVE of the vulnerability
        /// </summary>
        [JsonProperty("cve")]
        public string Cve { get; set; }

        /// <summary>
        /// Gets or sets the WWW of the vulnerability
        /// </summary>
        [JsonProperty("www")]
        public string Www { get; set; }
    }

    /// <summary>
    /// Response for getting firmware health
    /// </summary>
    public class FirmwareHealthResponse
    {
        /// <summary>
        /// Gets or sets the health
        /// </summary>
        [JsonProperty("health")]
        public string Health { get; set; }
    }
}
