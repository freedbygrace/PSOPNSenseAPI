using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Provides centralized mapping of API endpoints for different OPNSense versions
    /// </summary>
    public class OPNSenseApiEndpoints
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;
        private OPNSenseVersion? _detectedVersion;


        // Dictionary of endpoint mappings
        private static readonly Dictionary<string, Dictionary<OPNSenseVersion, string>> EndpointMappings = new Dictionary<string, Dictionary<OPNSenseVersion, string>>
        {
            // DHCP endpoints
            { "dhcp.leases.list", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "dhcpd/leases/searchLease" },
                { OPNSenseVersion.Modern, "dhcpd/service/searchLease" }
            }},
            { "dhcp.leases.getByMac", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "dhcpd/leases/getLeaseByMac/{0}" },
                { OPNSenseVersion.Modern, "dhcpd/service/getLeaseByMac/{0}" }
            }},
            { "dhcp.leases.delete", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "dhcpd/leases/delLease/{0}" },
                { OPNSenseVersion.Modern, "dhcpd/service/delLease/{0}" }
            }},

            // Config/Backup endpoints
            { "config.backup.list", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/backup/getBackups" },
                { OPNSenseVersion.Modern, "core/backup/getBackups" }
            }},
            { "config.backup.create", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/backup/backup" },
                { OPNSenseVersion.Modern, "core/backup/backup" }
            }},
            { "config.backup.download", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/backup/download/{0}" },
                { OPNSenseVersion.Modern, "core/backup/download/{0}" }
            }},
            { "config.backup.restore", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/backup/restore" },
                { OPNSenseVersion.Modern, "core/backup/restore" }
            }},
            { "config.backup.delete", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/backup/deleteBackup" },
                { OPNSenseVersion.Modern, "core/backup/deleteBackup" }
            }},
            { "config.export", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/config/download" },
                { OPNSenseVersion.Modern, "core/backup/download" }
            }},
            { "config.import", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/config/upload" },
                { OPNSenseVersion.Modern, "core/backup/upload" }
            }},

            // Firewall/Alias endpoints
            { "firewall.alias.list", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/alias/searchItem" },
                { OPNSenseVersion.Modern, "firewall/alias/searchItem" }
            }},
            { "firewall.alias.get", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/alias/getItem/{0}" },
                { OPNSenseVersion.Modern, "firewall/alias/getItem/{0}" }
            }},
            { "firewall.alias.add", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/alias/addItem" },
                { OPNSenseVersion.Modern, "firewall/alias/addItem" }
            }},
            { "firewall.alias.update", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/alias/setItem/{0}" },
                { OPNSenseVersion.Modern, "firewall/alias/setItem/{0}" }
            }},
            { "firewall.alias.delete", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/alias/delItem/{0}" },
                { OPNSenseVersion.Modern, "firewall/alias/delItem/{0}" }
            }},
            { "firewall.alias.reconfigure", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/alias/reconfigure" },
                { OPNSenseVersion.Modern, "firewall/alias/reconfigure" }
            }},

            // Interface endpoints
            { "interface.list", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "interfaces/overview/searchItem" },
                { OPNSenseVersion.Modern, "interfaces/overview/searchItem" }
            }},
            { "interface.get", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "interfaces/overview/getItem/{0}" },
                { OPNSenseVersion.Modern, "interfaces/overview/getItem/{0}" }
            }},

            // System endpoints
            { "system.status", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/system/status" },
                { OPNSenseVersion.Modern, "core/system/status" }
            }},
            { "system.version", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/firmware/status" },
                { OPNSenseVersion.Modern, "core/firmware/status" }
            }},

            // Plugin endpoints
            { "plugin.list", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/firmware/plugins" },
                { OPNSenseVersion.Modern, "core/firmware/plugins" }
            }},
            { "plugin.install", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "core/firmware/install/{0}" },
                { OPNSenseVersion.Modern, "core/firmware/install/{0}" }
            }},

            // Tailscale endpoints
            { "tailscale.status", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "os-tailscale/service/status" },
                { OPNSenseVersion.Modern, "os-tailscale/service/status" }
            }},
            { "tailscale.enable", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "os-tailscale/service/start" },
                { OPNSenseVersion.Modern, "os-tailscale/service/start" }
            }},
            { "tailscale.disable", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "os-tailscale/service/stop" },
                { OPNSenseVersion.Modern, "os-tailscale/service/stop" }
            }},
            { "tailscale.restart", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "os-tailscale/service/restart" },
                { OPNSenseVersion.Modern, "os-tailscale/service/restart" }
            }},

            // Port forwarding endpoints
            { "portforward.list", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/nat/searchRule" },
                { OPNSenseVersion.Modern, "firewall/nat/searchRule" }
            }},
            { "portforward.get", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/nat/getRule/{0}" },
                { OPNSenseVersion.Modern, "firewall/nat/getRule/{0}" }
            }},
            { "portforward.add", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/nat/addRule" },
                { OPNSenseVersion.Modern, "firewall/nat/addRule" }
            }},
            { "portforward.update", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/nat/setRule/{0}" },
                { OPNSenseVersion.Modern, "firewall/nat/setRule/{0}" }
            }},
            { "portforward.delete", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/nat/delRule/{0}" },
                { OPNSenseVersion.Modern, "firewall/nat/delRule/{0}" }
            }},
            { "portforward.apply", new Dictionary<OPNSenseVersion, string> {
                { OPNSenseVersion.Legacy, "firewall/nat/apply" },
                { OPNSenseVersion.Modern, "firewall/nat/apply" }
            }},
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="OPNSenseApiEndpoints"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public OPNSenseApiEndpoints(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the appropriate endpoint for the given key
        /// </summary>
        /// <param name="endpointKey">The endpoint key</param>
        /// <param name="parameters">Optional parameters to format into the endpoint</param>
        /// <returns>The endpoint</returns>
        public string GetEndpoint(string endpointKey, params object[] parameters)
        {
            if (!EndpointMappings.TryGetValue(endpointKey, out var versionMappings))
            {
                throw new ArgumentException($"Unknown endpoint key: {endpointKey}");
            }

            var version = GetOPNSenseVersion();
            if (!versionMappings.TryGetValue(version, out var endpoint))
            {
                throw new ArgumentException($"No endpoint mapping found for key {endpointKey} and version {version}");
            }

            // Format the endpoint with the provided parameters
            return parameters.Length > 0 ? string.Format(endpoint, parameters) : endpoint;
        }

        /// <summary>
        /// Gets the full URL for the given endpoint key
        /// </summary>
        /// <param name="endpointKey">The endpoint key</param>
        /// <param name="parameters">Optional parameters to format into the endpoint</param>
        /// <returns>The full URL</returns>
        public string GetFullUrl(string endpointKey, params object[] parameters)
        {
            var endpoint = GetEndpoint(endpointKey, parameters);
            return $"{_apiClient.BaseUrl}/api/{endpoint}";
        }

        /// <summary>
        /// Gets the OPNSense version
        /// </summary>
        /// <returns>The OPNSense version</returns>
        public OPNSenseVersion GetOPNSenseVersion()
        {
            // If already detected, return the cached version
            if (_detectedVersion.HasValue)
            {
                return _detectedVersion.Value;
            }

            // Try to detect the version
            try
            {
                _logger.Information("Detecting OPNSense version");

                // Try to access the firmware status endpoint
                var endpoint = "core/firmware/status";
                var response = _apiClient.Get<ApiVersionResponse>(endpoint);

                // If we get here, the API is available
                var version = response.GetVersion();
                _logger.Information($"Detected OPNSense version: {version}");

                // Parse the version to determine if it's modern or legacy
                if (!string.IsNullOrEmpty(version) && Version.TryParse(version, out var parsedVersion))
                {
                    // OPNSense 21.7 and later use the modern API
                    if (parsedVersion.Major > 21 || (parsedVersion.Major == 21 && parsedVersion.Minor >= 7))
                    {
                        _detectedVersion = OPNSenseVersion.Modern;
                    }
                    else
                    {
                        _detectedVersion = OPNSenseVersion.Legacy;
                    }
                }
                else
                {
                    // Try alternative detection methods
                    _detectedVersion = DetectVersionByEndpointAvailability();
                }
            }
            catch (Exception ex)
            {
                _logger.Warning($"Failed to detect OPNSense version from firmware status: {ex.Message}. Trying alternative methods.");
                _detectedVersion = DetectVersionByEndpointAvailability();
            }

            return _detectedVersion.Value;
        }

        /// <summary>
        /// Detects the OPNSense version by checking the availability of specific endpoints
        /// </summary>
        /// <returns>The detected OPNSense version</returns>
        private OPNSenseVersion DetectVersionByEndpointAvailability()
        {
            _logger.Information("Detecting OPNSense version by endpoint availability");

            try
            {
                // Try a modern API endpoint first (dhcpd/service/searchLease)
                var modernEndpoint = "dhcpd/service/searchLease";
                _apiClient.Get<object>(modernEndpoint);

                // If we get here, the modern API is available
                _logger.Information("Modern API endpoint is available");
                return OPNSenseVersion.Modern;
            }
            catch (Exception)
            {
                try
                {
                    // Try a legacy API endpoint (dhcpd/leases/searchLease)
                    var legacyEndpoint = "dhcpd/leases/searchLease";
                    _apiClient.Get<object>(legacyEndpoint);

                    // If we get here, the legacy API is available
                    _logger.Information("Legacy API endpoint is available");
                    return OPNSenseVersion.Legacy;
                }
                catch (Exception)
                {
                    // If both fail, default to legacy
                    _logger.Warning("Could not determine API version by endpoint availability. Defaulting to legacy API.");
                    return OPNSenseVersion.Legacy;
                }
            }
        }
    }

    /// <summary>
    /// OPNSense API version
    /// </summary>
    public enum OPNSenseVersion
    {
        /// <summary>
        /// Legacy API (OPNSense 21.6 and earlier)
        /// </summary>
        Legacy,

        /// <summary>
        /// Modern API (OPNSense 21.7 and later)
        /// </summary>
        Modern
    }

    // Internal classes for version detection
    internal class ApiVersionResponse
    {
        [JsonProperty("status")]
        public object Status { get; set; }

        [JsonProperty("firmware")]
        public ApiVersionFirmware Firmware { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("product_version")]
        public string ProductVersion { get; set; }

        [JsonProperty("product_copyright")]
        public string ProductCopyright { get; set; }

        [JsonProperty("os_version")]
        public string OsVersion { get; set; }

        public string GetVersion()
        {
            // Try to get version from different possible locations
            if (!string.IsNullOrEmpty(ProductVersion))
            {
                return ProductVersion;
            }

            if (Status is Newtonsoft.Json.Linq.JObject statusObj)
            {
                if (statusObj["firmware"] is Newtonsoft.Json.Linq.JObject firmwareObj &&
                    firmwareObj["version"] != null)
                {
                    return firmwareObj["version"].ToString();
                }
            }

            if (Firmware != null && !string.IsNullOrEmpty(Firmware.Version))
            {
                return Firmware.Version;
            }

            return null;
        }
    }

    internal class ApiVersionFirmware
    {
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
