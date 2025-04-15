using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense gateway API
    /// </summary>
    public class GatewayService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public GatewayService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all gateways
        /// </summary>
        /// <returns>A list of gateways</returns>
        public async Task<GatewayListResponse> GetGatewaysAsync()
        {
            _logger.Information("Getting gateways");
            
            var endpoint = "routes/gateway/search";
            return await _apiClient.GetAsync<GatewayListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a gateway by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the gateway</param>
        /// <returns>The gateway</returns>
        public async Task<GatewayResponse> GetGatewayAsync(string uuid)
        {
            _logger.Information($"Getting gateway with UUID {uuid}");
            
            var endpoint = $"routes/gateway/get/{uuid}";
            return await _apiClient.GetAsync<GatewayResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new gateway
        /// </summary>
        /// <param name="gateway">The gateway to create</param>
        /// <returns>The response containing the UUID of the new gateway</returns>
        public async Task<GatewayCreateResponse> CreateGatewayAsync(GatewayConfig gateway)
        {
            _logger.Information("Creating gateway");
            
            var endpoint = "routes/gateway/add";
            var data = new { gateway = gateway };
            return await _apiClient.PostAsync<GatewayCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a gateway
        /// </summary>
        /// <param name="uuid">The UUID of the gateway to update</param>
        /// <param name="gateway">The updated gateway</param>
        /// <returns>The response indicating success</returns>
        public async Task<GatewayUpdateResponse> UpdateGatewayAsync(string uuid, GatewayConfig gateway)
        {
            _logger.Information($"Updating gateway with UUID {uuid}");
            
            var endpoint = $"routes/gateway/set/{uuid}";
            var data = new { gateway = gateway };
            return await _apiClient.PostAsync<GatewayUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a gateway
        /// </summary>
        /// <param name="uuid">The UUID of the gateway to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<GatewayDeleteResponse> DeleteGatewayAsync(string uuid)
        {
            _logger.Information($"Deleting gateway with UUID {uuid}");
            
            var endpoint = $"routes/gateway/del/{uuid}";
            return await _apiClient.PostAsync<GatewayDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Applies gateway changes
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<GatewayApplyResponse> ApplyGatewayChangesAsync()
        {
            _logger.Information("Applying gateway changes");
            
            var endpoint = "routes/gateway/reconfigure";
            return await _apiClient.PostAsync<GatewayApplyResponse>(endpoint);
        }

        /// <summary>
        /// Gets the gateway status
        /// </summary>
        /// <returns>The gateway status</returns>
        public async Task<GatewayStatusResponse> GetGatewayStatusAsync()
        {
            _logger.Information("Getting gateway status");
            
            var endpoint = "routes/gateway/status";
            return await _apiClient.GetAsync<GatewayStatusResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting gateways
    /// </summary>
    public class GatewayListResponse
    {
        /// <summary>
        /// Gets or sets the total number of gateways
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
        /// Gets or sets the gateways
        /// </summary>
        [JsonProperty("rows")]
        public List<Gateway> Rows { get; set; }
    }

    /// <summary>
    /// Gateway
    /// </summary>
    public class Gateway
    {
        /// <summary>
        /// Gets or sets the UUID of the gateway
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the name of the gateway
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the interface of the gateway
        /// </summary>
        [JsonProperty("interface")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the monitor IP of the gateway
        /// </summary>
        [JsonProperty("monitor")]
        public string MonitorIp { get; set; }

        /// <summary>
        /// Gets or sets the description of the gateway
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the gateway is the default gateway
        /// </summary>
        [JsonProperty("defaultgw")]
        public string IsDefault { get; set; }

        /// <summary>
        /// Gets or sets whether the gateway is disabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; }
    }

    /// <summary>
    /// Response for getting a gateway
    /// </summary>
    public class GatewayResponse
    {
        /// <summary>
        /// Gets or sets the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public GatewayDetail Gateway { get; set; }
    }

    /// <summary>
    /// Gateway details
    /// </summary>
    public class GatewayDetail
    {
        /// <summary>
        /// Gets or sets the name of the gateway
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the interface of the gateway
        /// </summary>
        [JsonProperty("interface")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the monitor IP of the gateway
        /// </summary>
        [JsonProperty("monitor")]
        public string MonitorIp { get; set; }

        /// <summary>
        /// Gets or sets the description of the gateway
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the gateway is the default gateway
        /// </summary>
        [JsonProperty("defaultgw")]
        public string IsDefault { get; set; }

        /// <summary>
        /// Gets or sets whether the gateway is disabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; }

        /// <summary>
        /// Gets or sets the weight of the gateway
        /// </summary>
        [JsonProperty("weight")]
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the IP protocol of the gateway
        /// </summary>
        [JsonProperty("ipprotocol")]
        public string IpProtocol { get; set; }

        /// <summary>
        /// Gets or sets the monitor disable option
        /// </summary>
        [JsonProperty("monitor_disable")]
        public string MonitorDisable { get; set; }

        /// <summary>
        /// Gets or sets the force down option
        /// </summary>
        [JsonProperty("force_down")]
        public string ForceDown { get; set; }
    }

    /// <summary>
    /// Gateway configuration
    /// </summary>
    public class GatewayConfig
    {
        /// <summary>
        /// Gets or sets the name of the gateway
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the interface of the gateway
        /// </summary>
        [JsonProperty("interface")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the monitor IP of the gateway
        /// </summary>
        [JsonProperty("monitor")]
        public string MonitorIp { get; set; }

        /// <summary>
        /// Gets or sets the description of the gateway
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the gateway is the default gateway
        /// </summary>
        [JsonProperty("defaultgw")]
        public string IsDefault { get; set; } = "0";

        /// <summary>
        /// Gets or sets whether the gateway is disabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; } = "0";

        /// <summary>
        /// Gets or sets the weight of the gateway
        /// </summary>
        [JsonProperty("weight")]
        public string Weight { get; set; } = "1";

        /// <summary>
        /// Gets or sets the IP protocol of the gateway
        /// </summary>
        [JsonProperty("ipprotocol")]
        public string IpProtocol { get; set; } = "inet";

        /// <summary>
        /// Gets or sets the monitor disable option
        /// </summary>
        [JsonProperty("monitor_disable")]
        public string MonitorDisable { get; set; } = "0";

        /// <summary>
        /// Gets or sets the force down option
        /// </summary>
        [JsonProperty("force_down")]
        public string ForceDown { get; set; } = "0";
    }

    /// <summary>
    /// Response for creating a gateway
    /// </summary>
    public class GatewayCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new gateway
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a gateway
    /// </summary>
    public class GatewayUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a gateway
    /// </summary>
    public class GatewayDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for applying gateway changes
    /// </summary>
    public class GatewayApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting gateway status
    /// </summary>
    public class GatewayStatusResponse
    {
        /// <summary>
        /// Gets or sets the gateway status items
        /// </summary>
        [JsonProperty("items")]
        public Dictionary<string, GatewayStatusItem> Items { get; set; }
    }

    /// <summary>
    /// Gateway status item
    /// </summary>
    public class GatewayStatusItem
    {
        /// <summary>
        /// Gets or sets the name of the gateway
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the status of the gateway
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the RTT of the gateway
        /// </summary>
        [JsonProperty("rtt")]
        public string RTT { get; set; }

        /// <summary>
        /// Gets or sets the RTT standard deviation of the gateway
        /// </summary>
        [JsonProperty("stddev")]
        public string StdDev { get; set; }

        /// <summary>
        /// Gets or sets the loss percentage of the gateway
        /// </summary>
        [JsonProperty("loss")]
        public string Loss { get; set; }
    }
}
