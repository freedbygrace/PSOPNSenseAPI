using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AliasService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public AliasService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all aliases
        /// </summary>
        /// <returns>A list of aliases</returns>
        public async Task<AliasListResponse> GetAliasesAsync()
        {
            _logger.Information("Getting aliases");
            
            var endpoint = "firewall/alias/searchItem";
            return await _apiClient.GetAsync<AliasListResponse>(endpoint);
        }

        /// <summary>
        /// Gets an alias by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the alias</param>
        /// <returns>The alias</returns>
        public async Task<AliasResponse> GetAliasAsync(string uuid)
        {
            _logger.Information($"Getting alias with UUID {uuid}");
            
            var endpoint = $"firewall/alias/getItem/{uuid}";
            return await _apiClient.GetAsync<AliasResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new alias
        /// </summary>
        /// <param name="alias">The alias to create</param>
        /// <returns>The response containing the UUID of the new alias</returns>
        public async Task<AliasCreateResponse> CreateAliasAsync(AliasConfig alias)
        {
            _logger.Information("Creating alias");
            
            var endpoint = "firewall/alias/addItem";
            var data = new { alias = alias };
            return await _apiClient.PostAsync<AliasCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates an alias
        /// </summary>
        /// <param name="uuid">The UUID of the alias to update</param>
        /// <param name="alias">The updated alias</param>
        /// <returns>The response indicating success</returns>
        public async Task<AliasUpdateResponse> UpdateAliasAsync(string uuid, AliasConfig alias)
        {
            _logger.Information($"Updating alias with UUID {uuid}");
            
            var endpoint = $"firewall/alias/setItem/{uuid}";
            var data = new { alias = alias };
            return await _apiClient.PostAsync<AliasUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes an alias
        /// </summary>
        /// <param name="uuid">The UUID of the alias to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<AliasDeleteResponse> DeleteAliasAsync(string uuid)
        {
            _logger.Information($"Deleting alias with UUID {uuid}");
            
            var endpoint = $"firewall/alias/delItem/{uuid}";
            return await _apiClient.PostAsync<AliasDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Reconfigures aliases
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<AliasReconfigureResponse> ReconfigureAliasesAsync()
        {
            _logger.Information("Reconfiguring aliases");
            
            var endpoint = "firewall/alias/reconfigure";
            return await _apiClient.PostAsync<AliasReconfigureResponse>(endpoint);
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
