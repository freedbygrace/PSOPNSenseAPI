using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense firewall API
    /// </summary>
    public class FirewallService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirewallService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public FirewallService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all firewall rules
        /// </summary>
        /// <returns>A list of firewall rules</returns>
        public async Task<FirewallRuleSearchResponse> GetRulesAsync(string searchPhrase = "", int page = 1, int rowCount = 100)
        {
            _logger.Information("Getting firewall rules");
            
            var endpoint = $"firewall/filter/searchRule?current={page}&rowCount={rowCount}&searchPhrase={searchPhrase}";
            return await _apiClient.GetAsync<FirewallRuleSearchResponse>(endpoint);
        }

        /// <summary>
        /// Gets a firewall rule by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the rule</param>
        /// <returns>The firewall rule</returns>
        public async Task<FirewallRuleResponse> GetRuleAsync(string uuid)
        {
            _logger.Information($"Getting firewall rule with UUID {uuid}");
            
            var endpoint = $"firewall/filter/getRule/{uuid}";
            return await _apiClient.GetAsync<FirewallRuleResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new firewall rule
        /// </summary>
        /// <param name="rule">The rule to create</param>
        /// <returns>The response containing the UUID of the new rule</returns>
        public async Task<FirewallRuleCreateResponse> CreateRuleAsync(FirewallRuleCreate rule)
        {
            _logger.Information("Creating firewall rule");
            
            var endpoint = "firewall/filter/addRule";
            var data = new { rule = rule };
            return await _apiClient.PostAsync<FirewallRuleCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a firewall rule
        /// </summary>
        /// <param name="uuid">The UUID of the rule to update</param>
        /// <param name="rule">The updated rule</param>
        /// <returns>The response indicating success</returns>
        public async Task<FirewallRuleUpdateResponse> UpdateRuleAsync(string uuid, FirewallRuleCreate rule)
        {
            _logger.Information($"Updating firewall rule with UUID {uuid}");
            
            var endpoint = $"firewall/filter/setRule/{uuid}";
            var data = new { rule = rule };
            return await _apiClient.PostAsync<FirewallRuleUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a firewall rule
        /// </summary>
        /// <param name="uuid">The UUID of the rule to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<FirewallRuleDeleteResponse> DeleteRuleAsync(string uuid)
        {
            _logger.Information($"Deleting firewall rule with UUID {uuid}");
            
            var endpoint = $"firewall/filter/delRule/{uuid}";
            return await _apiClient.PostAsync<FirewallRuleDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Toggles a firewall rule
        /// </summary>
        /// <param name="uuid">The UUID of the rule to toggle</param>
        /// <param name="enabled">Whether to enable or disable the rule</param>
        /// <returns>The response indicating success</returns>
        public async Task<FirewallRuleToggleResponse> ToggleRuleAsync(string uuid, bool enabled)
        {
            _logger.Information($"Toggling firewall rule with UUID {uuid} to {(enabled ? "enabled" : "disabled")}");
            
            var endpoint = $"firewall/filter/toggleRule/{uuid}/{(enabled ? "1" : "0")}";
            return await _apiClient.PostAsync<FirewallRuleToggleResponse>(endpoint);
        }

        /// <summary>
        /// Creates a savepoint for the firewall configuration
        /// </summary>
        /// <returns>The response containing the savepoint revision</returns>
        public async Task<FirewallSavepointResponse> CreateSavepointAsync()
        {
            _logger.Information("Creating firewall savepoint");
            
            var endpoint = "firewall/filter/savepoint";
            return await _apiClient.PostAsync<FirewallSavepointResponse>(endpoint);
        }

        /// <summary>
        /// Applies firewall changes
        /// </summary>
        /// <param name="revision">The savepoint revision to use for rollback if needed</param>
        /// <returns>The response indicating success</returns>
        public async Task<FirewallApplyResponse> ApplyChangesAsync(string revision = null)
        {
            _logger.Information("Applying firewall changes");
            
            var endpoint = revision != null ? $"firewall/filter/apply/{revision}" : "firewall/filter/apply";
            return await _apiClient.PostAsync<FirewallApplyResponse>(endpoint);
        }

        /// <summary>
        /// Cancels a rollback
        /// </summary>
        /// <param name="revision">The savepoint revision</param>
        /// <returns>The response indicating success</returns>
        public async Task<FirewallCancelRollbackResponse> CancelRollbackAsync(string revision)
        {
            _logger.Information($"Cancelling rollback for revision {revision}");
            
            var endpoint = $"firewall/filter/cancelRollback/{revision}";
            return await _apiClient.PostAsync<FirewallCancelRollbackResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for firewall rule search
    /// </summary>
    public class FirewallRuleSearchResponse
    {
        /// <summary>
        /// Gets or sets the total number of rules
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
        /// Gets or sets the rules
        /// </summary>
        [JsonProperty("rows")]
        public List<FirewallRule> Rows { get; set; }
    }

    /// <summary>
    /// Firewall rule
    /// </summary>
    public class FirewallRule
    {
        /// <summary>
        /// Gets or sets the UUID of the rule
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets whether the rule is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the sequence of the rule
        /// </summary>
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the protocol of the rule
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the source network of the rule
        /// </summary>
        [JsonProperty("source_net")]
        public string SourceNet { get; set; }

        /// <summary>
        /// Gets or sets the source port of the rule
        /// </summary>
        [JsonProperty("source_port")]
        public string SourcePort { get; set; }

        /// <summary>
        /// Gets or sets the destination network of the rule
        /// </summary>
        [JsonProperty("destination_net")]
        public string DestinationNet { get; set; }

        /// <summary>
        /// Gets or sets the destination port of the rule
        /// </summary>
        [JsonProperty("destination_port")]
        public string DestinationPort { get; set; }

        /// <summary>
        /// Gets or sets the action of the rule
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
    }

    /// <summary>
    /// Response for getting a firewall rule
    /// </summary>
    public class FirewallRuleResponse
    {
        /// <summary>
        /// Gets or sets the rule
        /// </summary>
        [JsonProperty("rule")]
        public FirewallRuleDetails Rule { get; set; }
    }

    /// <summary>
    /// Detailed firewall rule
    /// </summary>
    public class FirewallRuleDetails
    {
        /// <summary>
        /// Gets or sets whether the rule is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the sequence of the rule
        /// </summary>
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the protocol of the rule
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the source network of the rule
        /// </summary>
        [JsonProperty("source_net")]
        public string SourceNet { get; set; }

        /// <summary>
        /// Gets or sets the source port of the rule
        /// </summary>
        [JsonProperty("source_port")]
        public string SourcePort { get; set; }

        /// <summary>
        /// Gets or sets the destination network of the rule
        /// </summary>
        [JsonProperty("destination_net")]
        public string DestinationNet { get; set; }

        /// <summary>
        /// Gets or sets the destination port of the rule
        /// </summary>
        [JsonProperty("destination_port")]
        public string DestinationPort { get; set; }

        /// <summary>
        /// Gets or sets the action of the rule
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }
    }

    /// <summary>
    /// Request for creating a firewall rule
    /// </summary>
    public class FirewallRuleCreate
    {
        /// <summary>
        /// Gets or sets whether the rule is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "1";

        /// <summary>
        /// Gets or sets the sequence of the rule
        /// </summary>
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the protocol of the rule
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// Gets or sets the source network of the rule
        /// </summary>
        [JsonProperty("source_net")]
        public string SourceNet { get; set; }

        /// <summary>
        /// Gets or sets the source port of the rule
        /// </summary>
        [JsonProperty("source_port")]
        public string SourcePort { get; set; }

        /// <summary>
        /// Gets or sets the destination network of the rule
        /// </summary>
        [JsonProperty("destination_net")]
        public string DestinationNet { get; set; }

        /// <summary>
        /// Gets or sets the destination port of the rule
        /// </summary>
        [JsonProperty("destination_port")]
        public string DestinationPort { get; set; }

        /// <summary>
        /// Gets or sets the action of the rule
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; } = "pass";
    }

    /// <summary>
    /// Response for creating a firewall rule
    /// </summary>
    public class FirewallRuleCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new rule
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a firewall rule
    /// </summary>
    public class FirewallRuleUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a firewall rule
    /// </summary>
    public class FirewallRuleDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for toggling a firewall rule
    /// </summary>
    public class FirewallRuleToggleResponse
    {
        /// <summary>
        /// Gets or sets the result of the toggle
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for creating a savepoint
    /// </summary>
    public class FirewallSavepointResponse
    {
        /// <summary>
        /// Gets or sets the revision of the savepoint
        /// </summary>
        [JsonProperty("revision")]
        public string Revision { get; set; }
    }

    /// <summary>
    /// Response for applying firewall changes
    /// </summary>
    public class FirewallApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for cancelling a rollback
    /// </summary>
    public class FirewallCancelRollbackResponse
    {
        /// <summary>
        /// Gets or sets the status of the cancel
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
