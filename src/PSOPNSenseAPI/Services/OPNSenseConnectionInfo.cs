using System;
using System.Management.Automation;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Provides detailed information about an OPNSense connection
    /// </summary>
    public class OPNSenseConnectionInfo
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;
        private readonly OPNSenseApiEndpoints _apiEndpoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="OPNSenseConnectionInfo"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public OPNSenseConnectionInfo(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _apiEndpoints = OPNSenseSessionState.Instance.ApiEndpoints;
        }

        /// <summary>
        /// Gets a PSObject containing detailed connection information
        /// </summary>
        /// <returns>A PSObject with connection details</returns>
        public PSObject GetConnectionInfo()
        {
            var connectionInfo = new PSObject();

            // Basic connection info
            connectionInfo.Properties.Add(new PSNoteProperty("Server", _apiClient.BaseUrl));
            connectionInfo.Properties.Add(new PSNoteProperty("Connected", _apiClient.IsConnected));
            connectionInfo.Properties.Add(new PSNoteProperty("ApiVersion", _apiEndpoints.GetOPNSenseVersion().ToString()));

            // Initialize properties with null values
            connectionInfo.Properties.Add(new PSNoteProperty("ProductName", null));
            connectionInfo.Properties.Add(new PSNoteProperty("ProductVersion", null));
            connectionInfo.Properties.Add(new PSNoteProperty("OsVersion", null));
            connectionInfo.Properties.Add(new PSNoteProperty("Hostname", null));
            connectionInfo.Properties.Add(new PSNoteProperty("SystemTime", null));
            connectionInfo.Properties.Add(new PSNoteProperty("Uptime", null));
            connectionInfo.Properties.Add(new PSNoteProperty("CpuUsage", null));
            connectionInfo.Properties.Add(new PSNoteProperty("MemoryUsage", null));
            connectionInfo.Properties.Add(new PSNoteProperty("LastUpdate", null));

            // If not connected, return the object with null values
            if (!_apiClient.IsConnected)
            {
                return connectionInfo;
            }

            try
            {
                // Try to get system information
                var systemInfo = GetSystemInfo();
                if (systemInfo != null)
                {
                    connectionInfo.Properties["ProductName"].Value = systemInfo.ProductName;
                    connectionInfo.Properties["ProductVersion"].Value = systemInfo.ProductVersion;
                    connectionInfo.Properties["OsVersion"].Value = systemInfo.OsVersion;
                    connectionInfo.Properties["Hostname"].Value = systemInfo.Hostname;
                    connectionInfo.Properties["SystemTime"].Value = systemInfo.SystemTime;
                    connectionInfo.Properties["Uptime"].Value = systemInfo.Uptime;
                }

                // Try to get system status
                var systemStatus = GetSystemStatus();
                if (systemStatus != null)
                {
                    connectionInfo.Properties["CpuUsage"].Value = systemStatus.CpuUsage;
                    connectionInfo.Properties["MemoryUsage"].Value = systemStatus.MemoryUsage;
                }

                // Try to get firmware status
                var firmwareStatus = GetFirmwareStatus();
                if (firmwareStatus != null)
                {
                    connectionInfo.Properties["LastUpdate"].Value = firmwareStatus.LastCheck;
                }
            }
            catch (Exception ex)
            {
                _logger.Warning($"Failed to get detailed firewall information: {ex.Message}");
            }

            return connectionInfo;
        }

        private SystemInfo GetSystemInfo()
        {
            try
            {
                var response = _apiClient.Get<ApiVersionResponse>("core/firmware/status");

                return new SystemInfo
                {
                    ProductName = response.ProductName,
                    ProductVersion = response.ProductVersion,
                    OsVersion = response.OsVersion,
                    Hostname = GetHostname(),
                    SystemTime = GetSystemTime(),
                    Uptime = GetUptime()
                };
            }
            catch (Exception ex)
            {
                _logger.Warning($"Failed to get system information: {ex.Message}");
                return null;
            }
        }

        private string GetHostname()
        {
            try
            {
                var response = _apiClient.Get<HostnameResponse>("core/system/hostname");
                return response.Hostname;
            }
            catch
            {
                return null;
            }
        }

        private string GetSystemTime()
        {
            try
            {
                var response = _apiClient.Get<SystemTimeResponse>("core/system/time");
                return response.Time;
            }
            catch
            {
                return null;
            }
        }

        private string GetUptime()
        {
            try
            {
                var response = _apiClient.Get<UptimeResponse>("core/system/status");
                return response.Uptime;
            }
            catch
            {
                return null;
            }
        }

        private SystemStatus GetSystemStatus()
        {
            try
            {
                var response = _apiClient.Get<SystemResourcesResponse>("core/system/status");

                return new SystemStatus
                {
                    CpuUsage = response.CpuUsage,
                    MemoryUsage = response.MemoryUsage
                };
            }
            catch (Exception ex)
            {
                _logger.Warning($"Failed to get system status: {ex.Message}");
                return null;
            }
        }

        private FirmwareStatus GetFirmwareStatus()
        {
            try
            {
                var response = _apiClient.Get<FirmwareStatusResponse>("core/firmware/status");

                return new FirmwareStatus
                {
                    LastCheck = response.LastCheck
                };
            }
            catch (Exception ex)
            {
                _logger.Warning($"Failed to get firmware status: {ex.Message}");
                return null;
            }
        }

        private class SystemInfo
        {
            public string ProductName { get; set; }
            public string ProductVersion { get; set; }
            public string OsVersion { get; set; }
            public string Hostname { get; set; }
            public string SystemTime { get; set; }
            public string Uptime { get; set; }
        }

        private class SystemStatus
        {
            public string CpuUsage { get; set; }
            public string MemoryUsage { get; set; }
        }

        private class FirmwareStatus
        {
            public string LastCheck { get; set; }
        }
    }

    internal class HostnameResponse
    {
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
    }

    internal class SystemTimeResponse
    {
        [JsonProperty("time")]
        public string Time { get; set; }
    }

    internal class UptimeResponse
    {
        [JsonProperty("uptime")]
        public string Uptime { get; set; }
    }

    internal class SystemResourcesResponse
    {
        [JsonProperty("cpu")]
        public string CpuUsage { get; set; }

        [JsonProperty("memory")]
        public string MemoryUsage { get; set; }
    }
}
