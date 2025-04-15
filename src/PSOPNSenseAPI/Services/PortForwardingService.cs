using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for managing port forwarding rules in OPNSense
    /// </summary>
    public class PortForwardingService : ServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortForwardingService"/> class
        /// </summary>
        /// <param name="apiClient">The API client to use for requests</param>
        public PortForwardingService(OPNSenseApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortForwardingService"/> class
        /// </summary>
        /// <param name="apiClient">The API client to use for requests</param>
        /// <param name="logger">The logger to use</param>
        public PortForwardingService(OPNSenseApiClient apiClient, ILogger logger) : base(apiClient, logger)
        {
        }

        /// <summary>
        /// Gets all port forwarding rules
        /// </summary>
        /// <returns>A list of port forwarding rules</returns>
        public async Task<List<PortForwardingRule>> GetPortForwardingRulesAsync()
        {
            Logger.Information("Getting all port forwarding rules");

            try
            {
                var endpoint = "firewall/nat/getRule";
                var response = await ApiClient.GetAsync<JObject>(endpoint);

                var rules = new List<PortForwardingRule>();
                // Process response when API is implemented
                return rules;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error getting port forwarding rules: {ex.Message}");
                return new List<PortForwardingRule>();
            }
        }

        /// <summary>
        /// Gets a port forwarding rule by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the rule to get</param>
        /// <returns>The port forwarding rule</returns>
        public async Task<PortForwardingRule> GetPortForwardingRuleAsync(string uuid)
        {
            Logger.Information($"Getting port forwarding rule with UUID {uuid}");

            try
            {
                var endpoint = $"firewall/nat/getRule/{uuid}";
                var response = await ApiClient.GetAsync<JObject>(endpoint);

                // Process response when API is implemented
                return new PortForwardingRule();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error getting port forwarding rule: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Creates a new port forwarding rule
        /// </summary>
        /// <param name="rule">The rule to create</param>
        /// <returns>The UUID of the created rule</returns>
        public async Task<string> CreatePortForwardingRuleAsync(PortForwardingRule rule)
        {
            Logger.Information("Creating new port forwarding rule");

            try
            {
                var endpoint = "firewall/nat/addRule";
                var response = await ApiClient.PostAsync<JObject>(endpoint, rule);

                // Process response when API is implemented
                return "new-uuid";
            }
            catch (Exception ex)
            {
                Logger.Error($"Error creating port forwarding rule: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Updates an existing port forwarding rule
        /// </summary>
        /// <param name="rule">The rule to update</param>
        /// <returns>True if successful</returns>
        public async Task<bool> UpdatePortForwardingRuleAsync(PortForwardingRule rule)
        {
            Logger.Information($"Updating port forwarding rule with UUID {rule.Uuid}");

            try
            {
                var endpoint = $"firewall/nat/setRule/{rule.Uuid}";
                var response = await ApiClient.PostAsync<JObject>(endpoint, rule);

                // Process response when API is implemented
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error updating port forwarding rule: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes a port forwarding rule
        /// </summary>
        /// <param name="uuid">The UUID of the rule to delete</param>
        /// <returns>True if successful</returns>
        public async Task<bool> DeletePortForwardingRuleAsync(string uuid)
        {
            Logger.Information($"Deleting port forwarding rule with UUID {uuid}");

            try
            {
                var endpoint = $"firewall/nat/delRule/{uuid}";
                var response = await ApiClient.PostAsync<JObject>(endpoint, null);

                // Process response when API is implemented
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error deleting port forwarding rule: {ex.Message}");
                return false;
            }
        }
    }
}
