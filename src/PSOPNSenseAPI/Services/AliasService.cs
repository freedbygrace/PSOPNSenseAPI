using System.Collections.Generic;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense alias API
    /// </summary>
    public class AliasService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;
        private readonly OPNSenseApiEndpoints _apiEndpoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="AliasService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public AliasService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
            _apiEndpoints = OPNSenseSessionState.Instance.ApiEndpoints;
        }

        /// <summary>
        /// Gets all aliases
        /// </summary>
        /// <returns>A list of aliases</returns>
        public AliasListResponse GetAliases()
        {
            _logger.Information("Getting aliases");

            var endpoint = _apiEndpoints.GetEndpoint("firewall.alias.list");
            return _apiClient.Get<AliasListResponse>(endpoint);
        }

        /// <summary>
        /// Gets an alias by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the alias</param>
        /// <returns>The alias</returns>
        public AliasResponse GetAlias(string uuid)
        {
            _logger.Information($"Getting alias with UUID {uuid}");

            var endpoint = _apiEndpoints.GetEndpoint("firewall.alias.get", uuid);
            return _apiClient.Get<AliasResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new alias
        /// </summary>
        /// <param name="alias">The alias to create</param>
        /// <returns>The response containing the UUID of the new alias</returns>
        public AliasCreateResponse CreateAlias(AliasConfig alias)
        {
            _logger.Information("Creating alias");

            var endpoint = _apiEndpoints.GetEndpoint("firewall.alias.add");
            var data = new { alias = alias };
            return _apiClient.Post<AliasCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates an alias
        /// </summary>
        /// <param name="uuid">The UUID of the alias to update</param>
        /// <param name="alias">The updated alias</param>
        /// <returns>The response indicating success</returns>
        public AliasUpdateResponse UpdateAlias(string uuid, AliasConfig alias)
        {
            _logger.Information($"Updating alias with UUID {uuid}");

            var endpoint = _apiEndpoints.GetEndpoint("firewall.alias.update", uuid);
            var data = new { alias = alias };
            return _apiClient.Post<AliasUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes an alias
        /// </summary>
        /// <param name="uuid">The UUID of the alias to delete</param>
        /// <returns>The response indicating success</returns>
        public AliasDeleteResponse DeleteAlias(string uuid)
        {
            _logger.Information($"Deleting alias with UUID {uuid}");

            var endpoint = _apiEndpoints.GetEndpoint("firewall.alias.delete", uuid);
            return _apiClient.Post<AliasDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Reconfigures aliases
        /// </summary>
        /// <returns>The response indicating success</returns>
        public AliasReconfigureResponse ReconfigureAliases()
        {
            _logger.Information("Reconfiguring aliases");

            var endpoint = _apiEndpoints.GetEndpoint("firewall.alias.reconfigure");
            return _apiClient.Post<AliasReconfigureResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting aliases
    /// </summary>
    public class AliasListResponse
    {
        /// <summary>
        /// Gets or sets the total number of aliases
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of rows per page
        /// </summary>
        [JsonProperty("rowCount")]
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the current page
        /// </summary>
        [JsonProperty("current")]
        public int Current { get; set; }

        /// <summary>
        /// Gets or sets the aliases
        /// </summary>
        [JsonProperty("rows")]
        public List<Alias> Rows { get; set; }
    }

    /// <summary>
    /// Alias
    /// </summary>
    public class Alias
    {
        /// <summary>
        /// Gets or sets the UUID of the alias
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the name of the alias
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the alias
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the content of the alias
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the description of the alias
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the alias is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response for getting an alias
    /// </summary>
    public class AliasResponse
    {
        /// <summary>
        /// Gets or sets the alias
        /// </summary>
        [JsonProperty("alias")]
        public AliasDetail Alias { get; set; }
    }

    /// <summary>
    /// Alias details
    /// </summary>
    public class AliasDetail
    {
        /// <summary>
        /// Gets or sets the name of the alias
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the alias
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the content of the alias
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the description of the alias
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the protocol of the alias (for port aliases)
        /// </summary>
        [JsonProperty("proto")]
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the update frequency of the alias (for URL aliases)
        /// </summary>
        [JsonProperty("updatefreq")]
        public string UpdateFrequency { get; set; }

        /// <summary>
        /// Gets or sets the counters of the alias
        /// </summary>
        [JsonProperty("counters")]
        public string Counters { get; set; }

        /// <summary>
        /// Gets or sets whether the alias is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Alias configuration
    /// </summary>
    public class AliasConfig
    {
        /// <summary>
        /// Gets or sets the name of the alias
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the alias
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the content of the alias
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the description of the alias
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the protocol of the alias (for port aliases)
        /// </summary>
        [JsonProperty("proto")]
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the update frequency of the alias (for URL aliases)
        /// </summary>
        [JsonProperty("updatefreq")]
        public string UpdateFrequency { get; set; }

        /// <summary>
        /// Gets or sets the counters of the alias
        /// </summary>
        [JsonProperty("counters")]
        public string Counters { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether the alias is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "1";
    }

    /// <summary>
    /// Response for creating an alias
    /// </summary>
    public class AliasCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new alias
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating an alias
    /// </summary>
    public class AliasUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting an alias
    /// </summary>
    public class AliasDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for reconfiguring aliases
    /// </summary>
    public class AliasReconfigureResponse
    {
        /// <summary>
        /// Gets or sets the status of the reconfiguration
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}

