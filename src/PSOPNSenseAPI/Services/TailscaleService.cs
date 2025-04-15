using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense Tailscale API
    /// </summary>
    public class TailscaleService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;
        private readonly PluginService _pluginService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TailscaleService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public TailscaleService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
            _pluginService = new PluginService(apiClient, logger);
        }

        /// <summary>
        /// Checks if the Tailscale plugin is installed
        /// </summary>
        /// <returns>True if the plugin is installed, false otherwise</returns>
        public async Task<bool> IsPluginInstalledAsync()
        {
            _logger.Information("Checking if Tailscale plugin is installed");

            try
            {
                var plugins = await _pluginService.GetInstalledPluginsAsync();
                return plugins != null;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error checking if Tailscale plugin is installed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Installs the Tailscale plugin
        /// </summary>
        /// <returns>True if the installation was successful, false otherwise</returns>
        public async Task<bool> InstallPluginAsync()
        {
            _logger.Information("Installing Tailscale plugin");

            try
            {
                var result = await _pluginService.InstallPluginAsync("os-tailscale");
                return result != null && result.Status == "ok";
            }
            catch (Exception ex)
            {
                _logger.Error($"Error installing Tailscale plugin: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the Tailscale settings
        /// </summary>
        /// <returns>The Tailscale settings</returns>
        public async Task<TailscaleSettingsResponse> GetSettingsAsync()
        {
            _logger.Information("Getting Tailscale settings");

            var endpoint = "tailscale/general/get";
            return await _apiClient.GetAsync<TailscaleSettingsResponse>(endpoint);
        }

        /// <summary>
        /// Updates the Tailscale settings
        /// </summary>
        /// <param name="settings">The updated settings</param>
        /// <returns>The response indicating success</returns>
        public async Task<TailscaleUpdateResponse> UpdateSettingsAsync(TailscaleSettings settings)
        {
            _logger.Information("Updating Tailscale settings");

            var endpoint = "tailscale/general/set";
            var data = new { general = settings };
            return await _apiClient.PostAsync<TailscaleUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Gets the Tailscale status
        /// </summary>
        /// <returns>The Tailscale status</returns>
        public async Task<TailscaleStatusResponse> GetStatusAsync()
        {
            _logger.Information("Getting Tailscale status");

            var endpoint = "tailscale/service/status";
            return await _apiClient.GetAsync<TailscaleStatusResponse>(endpoint);
        }

        /// <summary>
        /// Starts the Tailscale service
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<TailscaleServiceResponse> StartServiceAsync()
        {
            _logger.Information("Starting Tailscale service");

            var endpoint = "tailscale/service/start";
            return await _apiClient.PostAsync<TailscaleServiceResponse>(endpoint);
        }

        /// <summary>
        /// Stops the Tailscale service
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<TailscaleServiceResponse> StopServiceAsync()
        {
            _logger.Information("Stopping Tailscale service");

            var endpoint = "tailscale/service/stop";
            return await _apiClient.PostAsync<TailscaleServiceResponse>(endpoint);
        }

        /// <summary>
        /// Restarts the Tailscale service
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<TailscaleServiceResponse> RestartServiceAsync()
        {
            _logger.Information("Restarting Tailscale service");

            var endpoint = "tailscale/service/restart";
            return await _apiClient.PostAsync<TailscaleServiceResponse>(endpoint);
        }

        /// <summary>
        /// Connects to the Tailscale network using an auth key
        /// </summary>
        /// <param name="authKey">The auth key to use</param>
        /// <returns>The response indicating success</returns>
        public async Task<TailscaleConnectResponse> ConnectAsync(string authKey)
        {
            _logger.Information("Connecting to Tailscale network");

            var endpoint = "tailscale/service/connect";
            var data = new { authkey = authKey };
            return await _apiClient.PostAsync<TailscaleConnectResponse>(endpoint, data);
        }

        /// <summary>
        /// Disconnects from the Tailscale network
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<TailscaleServiceResponse> DisconnectAsync()
        {
            _logger.Information("Disconnecting from Tailscale network");

            var endpoint = "tailscale/service/disconnect";
            return await _apiClient.PostAsync<TailscaleServiceResponse>(endpoint);
        }

        /// <summary>
        /// Gets the Tailscale interfaces
        /// </summary>
        /// <returns>The Tailscale interfaces</returns>
        public async Task<TailscaleInterfacesResponse> GetInterfacesAsync()
        {
            _logger.Information("Getting Tailscale interfaces");

            var endpoint = "tailscale/service/interfaces";
            return await _apiClient.GetAsync<TailscaleInterfacesResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting Tailscale settings
    /// </summary>
    public class TailscaleSettingsResponse
    {
        /// <summary>
        /// Gets or sets the Tailscale settings
        /// </summary>
        [JsonProperty("general")]
        public TailscaleSettings General { get; set; }
    }

    /// <summary>
    /// Tailscale settings
    /// </summary>
    public class TailscaleSettings
    {
        /// <summary>
        /// Gets or sets whether Tailscale is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether to accept DNS configuration from Tailscale
        /// </summary>
        [JsonProperty("accept_dns")]
        public string AcceptDns { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether to accept routes from Tailscale
        /// </summary>
        [JsonProperty("accept_routes")]
        public string AcceptRoutes { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether to advertise exit node
        /// </summary>
        [JsonProperty("advertise_exit_node")]
        public string AdvertiseExitNode { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether to advertise routes
        /// </summary>
        [JsonProperty("advertise_routes")]
        public string AdvertiseRoutes { get; set; } = "0";

        /// <summary>
        /// Gets or sets the routes to advertise
        /// </summary>
        [JsonProperty("routes_to_advertise")]
        public string RoutesToAdvertise { get; set; } = "";

        /// <summary>
        /// Gets or sets the hostname
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; } = "";

        /// <summary>
        /// Gets or sets the login server
        /// </summary>
        [JsonProperty("login_server")]
        public string LoginServer { get; set; } = "";

        /// <summary>
        /// Gets or sets whether to use SSH
        /// </summary>
        [JsonProperty("ssh")]
        public string Ssh { get; set; } = "0";

        /// <summary>
        /// Gets or sets the ephemeral node key
        /// </summary>
        [JsonProperty("ephemeral")]
        public string Ephemeral { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether to reset on start
        /// </summary>
        [JsonProperty("reset_on_start")]
        public string ResetOnStart { get; set; } = "0";
    }

    /// <summary>
    /// Response for updating Tailscale settings
    /// </summary>
    public class TailscaleUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for Tailscale service operations
    /// </summary>
    public class TailscaleServiceResponse
    {
        /// <summary>
        /// Gets or sets the status of the operation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting Tailscale status
    /// </summary>
    public class TailscaleStatusResponse
    {
        /// <summary>
        /// Gets or sets whether the service is running
        /// </summary>
        [JsonProperty("running")]
        public bool Running { get; set; }

        /// <summary>
        /// Gets or sets whether the service is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the status message
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for connecting to Tailscale
    /// </summary>
    public class TailscaleConnectResponse
    {
        /// <summary>
        /// Gets or sets the status of the connection
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    /// <summary>
    /// Response for getting Tailscale interfaces
    /// </summary>
    public class TailscaleInterfacesResponse
    {
        /// <summary>
        /// Gets or sets the interfaces
        /// </summary>
        [JsonProperty("interfaces")]
        public List<TailscaleInterface> Interfaces { get; set; }
    }

    /// <summary>
    /// Tailscale interface
    /// </summary>
    public class TailscaleInterface
    {
        /// <summary>
        /// Gets or sets the name of the interface
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the IP addresses of the interface
        /// </summary>
        [JsonProperty("ips")]
        public List<string> IpAddresses { get; set; }
    }
}
