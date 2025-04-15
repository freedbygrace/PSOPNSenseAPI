using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense plugin API
    /// </summary>
    public class PluginService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public PluginService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all plugins
        /// </summary>
        /// <returns>A list of plugins</returns>
        public async Task<PluginListResponse> GetPluginsAsync()
        {
            _logger.Information("Getting plugins");

            var endpoint = "core/firmware/getPlugins";
            return await _apiClient.GetAsync<PluginListResponse>(endpoint);
        }

        /// <summary>
        /// Gets all installed plugins
        /// </summary>
        /// <returns>A list of installed plugins</returns>
        public async Task<PluginListResponse> GetInstalledPluginsAsync()
        {
            _logger.Information("Getting installed plugins");

            var endpoint = "core/firmware/getInstalledPlugins";
            return await _apiClient.GetAsync<PluginListResponse>(endpoint);
        }

        /// <summary>
        /// Installs a plugin
        /// </summary>
        /// <param name="name">The name of the plugin to install</param>
        /// <returns>The response indicating success</returns>
        public async Task<PluginInstallResponse> InstallPluginAsync(string name)
        {
            _logger.Information($"Installing plugin {name}");

            var endpoint = "core/firmware/install";
            var data = new { pkg = name };
            return await _apiClient.PostAsync<PluginInstallResponse>(endpoint, data);
        }

        /// <summary>
        /// Uninstalls a plugin
        /// </summary>
        /// <param name="name">The name of the plugin to uninstall</param>
        /// <returns>The response indicating success</returns>
        public async Task<PluginUninstallResponse> UninstallPluginAsync(string name)
        {
            _logger.Information($"Uninstalling plugin {name}");

            var endpoint = "core/firmware/remove";
            var data = new { pkg = name };
            return await _apiClient.PostAsync<PluginUninstallResponse>(endpoint, data);
        }

        /// <summary>
        /// Enables a plugin
        /// </summary>
        /// <param name="name">The name of the plugin to enable</param>
        /// <returns>The response indicating success</returns>
        public async Task<PluginEnableResponse> EnablePluginAsync(string name)
        {
            _logger.Information($"Enabling plugin {name}");

            var endpoint = "core/firmware/enable";
            var data = new { pkg = name };
            return await _apiClient.PostAsync<PluginEnableResponse>(endpoint, data);
        }

        /// <summary>
        /// Disables a plugin
        /// </summary>
        /// <param name="name">The name of the plugin to disable</param>
        /// <returns>The response indicating success</returns>
        public async Task<PluginDisableResponse> DisablePluginAsync(string name)
        {
            _logger.Information($"Disabling plugin {name}");

            var endpoint = "core/firmware/disable";
            var data = new { pkg = name };
            return await _apiClient.PostAsync<PluginDisableResponse>(endpoint, data);
        }

        /// <summary>
        /// Gets the status of a plugin operation
        /// </summary>
        /// <returns>The status of the plugin operation</returns>
        public async Task<PluginStatusResponse> GetPluginStatusAsync()
        {
            _logger.Information("Getting plugin operation status");

            var endpoint = "core/firmware/status";
            return await _apiClient.GetAsync<PluginStatusResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting plugins
    /// </summary>
    public class PluginListResponse
    {
        /// <summary>
        /// Gets or sets the installed plugins
        /// </summary>
        [JsonProperty("installed")]
        public Dictionary<string, Plugin> Installed { get; set; }

        /// <summary>
        /// Gets or sets the available plugins
        /// </summary>
        [JsonProperty("available")]
        public Dictionary<string, Plugin> Available { get; set; }
    }

    /// <summary>
    /// Plugin information
    /// </summary>
    public class Plugin
    {
        /// <summary>
        /// Gets or sets the name of the plugin
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version of the plugin
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the comment of the plugin
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the repository of the plugin
        /// </summary>
        [JsonProperty("repository")]
        public string Repository { get; set; }

        /// <summary>
        /// Gets or sets the origin of the plugin
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the license of the plugin
        /// </summary>
        [JsonProperty("license")]
        public string License { get; set; }

        /// <summary>
        /// Gets or sets the flatsize of the plugin
        /// </summary>
        [JsonProperty("flatsize")]
        public string FlatSize { get; set; }

        /// <summary>
        /// Gets or sets the locked status of the plugin
        /// </summary>
        [JsonProperty("locked")]
        public string Locked { get; set; }

        /// <summary>
        /// Gets or sets the enabled status of the plugin
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response for installing a plugin
    /// </summary>
    public class PluginInstallResponse
    {
        /// <summary>
        /// Gets or sets the status of the installation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for uninstalling a plugin
    /// </summary>
    public class PluginUninstallResponse
    {
        /// <summary>
        /// Gets or sets the status of the uninstallation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for enabling a plugin
    /// </summary>
    public class PluginEnableResponse
    {
        /// <summary>
        /// Gets or sets the status of the enable
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for disabling a plugin
    /// </summary>
    public class PluginDisableResponse
    {
        /// <summary>
        /// Gets or sets the status of the disable
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting the status of a plugin operation
    /// </summary>
    public class PluginStatusResponse
    {
        /// <summary>
        /// Gets or sets the status of the plugin operation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the log of the plugin operation
        /// </summary>
        [JsonProperty("log")]
        public string Log { get; set; }
    }
}
