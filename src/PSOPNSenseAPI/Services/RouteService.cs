using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense route API
    /// </summary>
    public class RouteService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public RouteService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all routes
        /// </summary>
        /// <returns>A list of routes</returns>
        public async Task<RouteListResponse> GetRoutesAsync()
        {
            _logger.Information("Getting routes");
            
            var endpoint = "routes/routes/search";
            return await _apiClient.GetAsync<RouteListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a route by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the route</param>
        /// <returns>The route</returns>
        public async Task<RouteResponse> GetRouteAsync(string uuid)
        {
            _logger.Information($"Getting route with UUID {uuid}");
            
            var endpoint = $"routes/routes/get/{uuid}";
            return await _apiClient.GetAsync<RouteResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new route
        /// </summary>
        /// <param name="route">The route to create</param>
        /// <returns>The response containing the UUID of the new route</returns>
        public async Task<RouteCreateResponse> CreateRouteAsync(RouteConfig route)
        {
            _logger.Information("Creating route");
            
            var endpoint = "routes/routes/add";
            var data = new { route = route };
            return await _apiClient.PostAsync<RouteCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a route
        /// </summary>
        /// <param name="uuid">The UUID of the route to update</param>
        /// <param name="route">The updated route</param>
        /// <returns>The response indicating success</returns>
        public async Task<RouteUpdateResponse> UpdateRouteAsync(string uuid, RouteConfig route)
        {
            _logger.Information($"Updating route with UUID {uuid}");
            
            var endpoint = $"routes/routes/set/{uuid}";
            var data = new { route = route };
            return await _apiClient.PostAsync<RouteUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a route
        /// </summary>
        /// <param name="uuid">The UUID of the route to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<RouteDeleteResponse> DeleteRouteAsync(string uuid)
        {
            _logger.Information($"Deleting route with UUID {uuid}");
            
            var endpoint = $"routes/routes/del/{uuid}";
            return await _apiClient.PostAsync<RouteDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Applies route changes
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<RouteApplyResponse> ApplyRouteChangesAsync()
        {
            _logger.Information("Applying route changes");
            
            var endpoint = "routes/routes/reconfigure";
            return await _apiClient.PostAsync<RouteApplyResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting routes
    /// </summary>
    public class RouteListResponse
    {
        /// <summary>
        /// Gets or sets the total number of routes
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
        /// Gets or sets the routes
        /// </summary>
        [JsonProperty("rows")]
        public List<Route> Rows { get; set; }
    }

    /// <summary>
    /// Route
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the UUID of the route
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the network of the route
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the gateway of the route
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets the description of the route
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the route is disabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; }
    }

    /// <summary>
    /// Response for getting a route
    /// </summary>
    public class RouteResponse
    {
        /// <summary>
        /// Gets or sets the route
        /// </summary>
        [JsonProperty("route")]
        public RouteDetail Route { get; set; }
    }

    /// <summary>
    /// Route details
    /// </summary>
    public class RouteDetail
    {
        /// <summary>
        /// Gets or sets the network of the route
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the gateway of the route
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets the description of the route
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the route is disabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; }
    }

    /// <summary>
    /// Route configuration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Gets or sets the network of the route
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the gateway of the route
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets the description of the route
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the route is disabled
        /// </summary>
        [JsonProperty("disabled")]
        public string Disabled { get; set; } = "0";
    }

    /// <summary>
    /// Response for creating a route
    /// </summary>
    public class RouteCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new route
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a route
    /// </summary>
    public class RouteUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a route
    /// </summary>
    public class RouteDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for applying route changes
    /// </summary>
    public class RouteApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
