using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense configuration API
    /// </summary>
    public class ConfigService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;
        private readonly OPNSenseApiEndpoints _apiEndpoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public ConfigService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
            _apiEndpoints = OPNSenseSessionState.Instance.ApiEndpoints;
        }

        /// <summary>
        /// Gets the list of configuration backups
        /// </summary>
        /// <returns>A list of configuration backups</returns>
        public ConfigBackupListResponse GetConfigBackups()
        {
            _logger.Information("Getting configuration backups");

            var endpoint = _apiEndpoints.GetEndpoint("config.backup.list");
            return _apiClient.Get<ConfigBackupListResponse>(endpoint);
        }

        /// <summary>
        /// Creates a configuration backup
        /// </summary>
        /// <param name="filename">The filename for the backup</param>
        /// <returns>The response indicating success</returns>
        public ConfigBackupCreateResponse CreateConfigBackup(string filename = null)
        {
            _logger.Information("Creating configuration backup");

            var endpoint = _apiEndpoints.GetEndpoint("config.backup.create");
            var data = string.IsNullOrEmpty(filename) ? null : new { filename = filename };
            return _apiClient.Post<ConfigBackupCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Downloads a configuration backup
        /// </summary>
        /// <param name="filename">The filename of the backup to download</param>
        /// <returns>The backup content</returns>
        public byte[] DownloadConfigBackup(string filename)
        {
            _logger.Information($"Downloading configuration backup {filename}");

            var endpoint = _apiEndpoints.GetEndpoint("config.backup.download", filename);
            var response = _apiClient.Get<ConfigBackupDownloadResponse>(endpoint);

            if (string.IsNullOrEmpty(response.Content))
            {
                throw new OPNSenseApiException("Backup content is empty", System.Net.HttpStatusCode.NoContent, "");
            }

            return Convert.FromBase64String(response.Content);
        }

        /// <summary>
        /// Restores a configuration backup
        /// </summary>
        /// <param name="filename">The filename of the backup to restore</param>
        /// <returns>The response indicating success</returns>
        public ConfigBackupRestoreResponse RestoreConfigBackup(string filename)
        {
            _logger.Information($"Restoring configuration backup {filename}");

            var endpoint = _apiEndpoints.GetEndpoint("config.backup.restore");
            var data = new { filename = filename };
            return _apiClient.Post<ConfigBackupRestoreResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a configuration backup
        /// </summary>
        /// <param name="filename">The filename of the backup to delete</param>
        /// <returns>The response indicating success</returns>
        public ConfigBackupDeleteResponse DeleteConfigBackup(string filename)
        {
            _logger.Information($"Deleting configuration backup {filename}");

            var endpoint = _apiEndpoints.GetEndpoint("config.backup.delete");
            var data = new { filename = filename };
            return _apiClient.Post<ConfigBackupDeleteResponse>(endpoint, data);
        }

        /// <summary>
        /// Exports the configuration
        /// </summary>
        /// <returns>The exported configuration</returns>
        public byte[] ExportConfig()
        {
            _logger.Information("Exporting configuration");

            var endpoint = _apiEndpoints.GetEndpoint("config.export");
            var response = _apiClient.Get<ConfigExportResponse>(endpoint);

            if (string.IsNullOrEmpty(response.Content))
            {
                throw new OPNSenseApiException("Export content is empty", System.Net.HttpStatusCode.NoContent, "");
            }

            return Convert.FromBase64String(response.Content);
        }

        /// <summary>
        /// Imports a configuration
        /// </summary>
        /// <param name="configContent">The configuration content</param>
        /// <returns>The response indicating success</returns>
        public ConfigImportResponse ImportConfig(byte[] configContent)
        {
            _logger.Information("Importing configuration");

            var endpoint = _apiEndpoints.GetEndpoint("config.import");
            var base64Content = Convert.ToBase64String(configContent);
            var data = new { content = base64Content };
            return _apiClient.Post<ConfigImportResponse>(endpoint, data);
        }
    }

    /// <summary>
    /// Response for getting configuration backups
    /// </summary>
    public class ConfigBackupListResponse
    {
        /// <summary>
        /// Gets or sets the backups
        /// </summary>
        [JsonProperty("backups")]
        public List<ConfigBackup> Backups { get; set; }
    }

    /// <summary>
    /// Configuration backup
    /// </summary>
    public class ConfigBackup
    {
        /// <summary>
        /// Gets or sets the filename of the backup
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the description of the backup
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the size of the backup
        /// </summary>
        [JsonProperty("filesize")]
        public string FileSize { get; set; }

        /// <summary>
        /// Gets or sets the date of the backup
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the version of the backup
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }
    }

    /// <summary>
    /// Response for creating a configuration backup
    /// </summary>
    public class ConfigBackupCreateResponse
    {
        /// <summary>
        /// Gets or sets the filename of the backup
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the status of the backup
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for downloading a configuration backup
    /// </summary>
    public class ConfigBackupDownloadResponse
    {
        /// <summary>
        /// Gets or sets the content of the backup
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
    }

    /// <summary>
    /// Response for restoring a configuration backup
    /// </summary>
    public class ConfigBackupRestoreResponse
    {
        /// <summary>
        /// Gets or sets the status of the restore
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for deleting a configuration backup
    /// </summary>
    public class ConfigBackupDeleteResponse
    {
        /// <summary>
        /// Gets or sets the status of the deletion
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for exporting the configuration
    /// </summary>
    public class ConfigExportResponse
    {
        /// <summary>
        /// Gets or sets the content of the export
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
    }

    /// <summary>
    /// Response for importing a configuration
    /// </summary>
    public class ConfigImportResponse
    {
        /// <summary>
        /// Gets or sets the status of the import
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}

