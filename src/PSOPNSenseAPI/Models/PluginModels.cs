using System.Collections.Generic;
using Newtonsoft.Json;

namespace PSOPNSenseAPI.Models
{
    /// <summary>
    /// Response from the OPNSense API for plugin list
    /// </summary>
    public class PluginListResponse
    {
        /// <summary>
        /// List of plugins
        /// </summary>
        [JsonProperty("rows")]
        public List<PluginItem> Rows { get; set; }

        /// <summary>
        /// Total number of plugins
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        [JsonProperty("current")]
        public int Current { get; set; }

        /// <summary>
        /// Number of rows per page
        /// </summary>
        [JsonProperty("rowCount")]
        public int RowCount { get; set; }

        /// <summary>
        /// Status of the operation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        [JsonProperty("status_msg")]
        public string StatusMessage { get; set; }
    }

    /// <summary>
    /// Plugin item
    /// </summary>
    public class PluginItem
    {
        /// <summary>
        /// Plugin name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Plugin version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Plugin comment
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Plugin repository
        /// </summary>
        [JsonProperty("repository")]
        public string Repository { get; set; }

        /// <summary>
        /// Plugin origin
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }

        /// <summary>
        /// Plugin category
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Plugin license
        /// </summary>
        [JsonProperty("license")]
        public string License { get; set; }

        /// <summary>
        /// Plugin maintainer
        /// </summary>
        [JsonProperty("maintainer")]
        public string Maintainer { get; set; }

        /// <summary>
        /// Plugin website
        /// </summary>
        [JsonProperty("www")]
        public string Website { get; set; }

        /// <summary>
        /// Plugin installed
        /// </summary>
        [JsonProperty("installed")]
        public bool Installed { get; set; }

        /// <summary>
        /// Plugin locked
        /// </summary>
        [JsonProperty("locked")]
        public bool Locked { get; set; }

        /// <summary>
        /// Plugin size
        /// </summary>
        [JsonProperty("flatsize")]
        public long Size { get; set; }

        /// <summary>
        /// Plugin description
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for plugin installation
    /// </summary>
    public class PluginInstallResponse
    {
        /// <summary>
        /// Status of the installation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Message from the installation
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// Log from the installation
        /// </summary>
        [JsonProperty("log")]
        public string Log { get; set; }

        /// <summary>
        /// UUID of the installation
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Status of the installation
        /// </summary>
        [JsonProperty("status_msg")]
        public string StatusMessage { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for plugin uninstallation
    /// </summary>
    public class PluginUninstallResponse
    {
        /// <summary>
        /// Status of the uninstallation
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Message from the uninstallation
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        /// <summary>
        /// Log from the uninstallation
        /// </summary>
        [JsonProperty("log")]
        public string Log { get; set; }

        /// <summary>
        /// UUID of the uninstallation
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Status message
        /// </summary>
        [JsonProperty("status_msg")]
        public string StatusMessage { get; set; }
    }
}
