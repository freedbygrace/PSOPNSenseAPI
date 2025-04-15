using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for managing OPNSense interfaces
    /// </summary>
    public class InterfaceService
    {
        private readonly IOPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceService"/> class
        /// </summary>
        /// <param name="apiClient">The OPNSense API client</param>
        /// <param name="logger">The logger</param>
        public InterfaceService(IOPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all interfaces
        /// </summary>
        /// <returns>A list of interfaces</returns>
        public async Task<InterfaceListResponse> GetInterfacesAsync()
        {
            _logger.Information("Getting interfaces");

            var endpoint = "interfaces/overview/searchInterfaces";
            return await _apiClient.GetAsync<InterfaceListResponse>(endpoint);
        }

        /// <summary>
        /// Gets details for a specific interface
        /// </summary>
        /// <param name="interfaceName">The name of the interface</param>
        /// <returns>The interface details</returns>
        public async Task<InterfaceDetailResponse> GetInterfaceDetailAsync(string interfaceName)
        {
            _logger.Information($"Getting interface {interfaceName}");

            var endpoint = $"interfaces/overview/getInterface/{interfaceName}";
            return await _apiClient.GetAsync<InterfaceDetailResponse>(endpoint);
        }

        /// <summary>
        /// Gets statistics for all interfaces
        /// </summary>
        /// <returns>The interface statistics</returns>
        public async Task<InterfaceStatisticsResponse> GetInterfaceStatisticsAsync()
        {
            _logger.Information("Getting interface statistics");

            var endpoint = "diagnostics/interface/getInterfaceStatistics";
            return await _apiClient.GetAsync<InterfaceStatisticsResponse>(endpoint);
        }

        /// <summary>
        /// Updates an interface
        /// </summary>
        /// <param name="interfaceName">The name of the interface to update</param>
        /// <param name="interfaceConfig">The interface configuration</param>
        /// <returns>The update response</returns>
        public async Task<InterfaceUpdateResponse> UpdateInterfaceAsync(string interfaceName, InterfaceConfig interfaceConfig)
        {
            _logger.Information($"Updating interface {interfaceName}");

            var endpoint = $"interfaces/overview/setInterface/{interfaceName}";
            var data = new { iface = interfaceConfig };
            return await _apiClient.PostAsync<InterfaceUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Restarts an interface
        /// </summary>
        /// <param name="interfaceName">The name of the interface to restart</param>
        /// <returns>The restart response</returns>
        public async Task<InterfaceRestartResponse> RestartInterfaceAsync(string interfaceName)
        {
            _logger.Information($"Restarting interface {interfaceName}");

            var endpoint = $"interfaces/overview/reconfigure/{interfaceName}";
            return await _apiClient.PostAsync<InterfaceRestartResponse>(endpoint);
        }

        /// <summary>
        /// Gets all VLANs
        /// </summary>
        /// <returns>A list of VLANs</returns>
        public async Task<VLANListResponse> GetVLANsAsync()
        {
            _logger.Information("Getting VLANs");

            var endpoint = "interfaces/vlan/searchItem";
            return await _apiClient.GetAsync<VLANListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a specific VLAN
        /// </summary>
        /// <param name="uuid">The UUID of the VLAN</param>
        /// <returns>The VLAN details</returns>
        public async Task<VLANResponse> GetVLANAsync(string uuid)
        {
            _logger.Information($"Getting VLAN with UUID {uuid}");

            var endpoint = $"interfaces/vlan/getItem/{uuid}";
            return await _apiClient.GetAsync<VLANResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new VLAN
        /// </summary>
        /// <param name="vlan">The VLAN configuration</param>
        /// <returns>The create response with the UUID of the created VLAN</returns>
        public async Task<VLANCreateResponse> CreateVLANAsync(VLANConfig vlan)
        {
            _logger.Information("Creating VLAN");

            var endpoint = "interfaces/vlan/addItem";
            var data = new { vlan = vlan };
            return await _apiClient.PostAsync<VLANCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates an existing VLAN
        /// </summary>
        /// <param name="uuid">The UUID of the VLAN to update</param>
        /// <param name="vlan">The updated VLAN configuration</param>
        /// <returns>The update response</returns>
        public async Task<VLANUpdateResponse> UpdateVLANAsync(string uuid, VLANConfig vlan)
        {
            _logger.Information($"Updating VLAN with UUID {uuid}");

            var endpoint = $"interfaces/vlan/setItem/{uuid}";
            var data = new { vlan = vlan };
            return await _apiClient.PostAsync<VLANUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a VLAN
        /// </summary>
        /// <param name="uuid">The UUID of the VLAN to delete</param>
        /// <returns>The delete response</returns>
        public async Task<VLANDeleteResponse> DeleteVLANAsync(string uuid)
        {
            _logger.Information($"Deleting VLAN with UUID {uuid}");

            var endpoint = $"interfaces/vlan/delItem/{uuid}";
            return await _apiClient.PostAsync<VLANDeleteResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response from the OPNSense API for interface list
    /// </summary>
    public class InterfaceListResponse
    {
        /// <summary>
        /// Gets or sets the list of interfaces
        /// </summary>
        [JsonProperty("rows")]
        public List<InterfaceInfo> Interfaces { get; set; }
    }

    /// <summary>
    /// Information about an interface
    /// </summary>
    public class InterfaceInfo
    {
        /// <summary>
        /// Gets or sets the name of the interface
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the interface
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the interface
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the subnet mask of the interface
        /// </summary>
        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }

        /// <summary>
        /// Gets or sets the gateway of the interface
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets the status of the interface
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for interface details
    /// </summary>
    public class InterfaceDetailResponse
    {
        /// <summary>
        /// Gets or sets the interface details
        /// </summary>
        [JsonProperty("interface")]
        public InterfaceDetail Interface { get; set; }
    }

    /// <summary>
    /// Detailed information about an interface
    /// </summary>
    public class InterfaceDetail
    {
        /// <summary>
        /// Gets or sets the name of the interface
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the interface
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the interface
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the subnet mask of the interface
        /// </summary>
        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }

        /// <summary>
        /// Gets or sets the gateway of the interface
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets whether the interface is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Configuration for an interface
    /// </summary>
    public class InterfaceConfig
    {
        /// <summary>
        /// Gets or sets the description of the interface
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the interface
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the subnet mask of the interface
        /// </summary>
        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }

        /// <summary>
        /// Gets or sets the gateway of the interface
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Gets or sets whether the interface is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for interface update
    /// </summary>
    public class InterfaceUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for interface restart
    /// </summary>
    public class InterfaceRestartResponse
    {
        /// <summary>
        /// Gets or sets the status of the restart
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for interface statistics
    /// </summary>
    public class InterfaceStatisticsResponse
    {
        /// <summary>
        /// Gets or sets the statistics for each interface
        /// </summary>
        [JsonProperty("statistics")]
        public Dictionary<string, InterfaceStatistics> Statistics { get; set; }
    }

    /// <summary>
    /// Statistics for an interface
    /// </summary>
    public class InterfaceStatistics
    {
        /// <summary>
        /// Gets or sets the name of the interface
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of incoming packets
        /// </summary>
        [JsonProperty("inpkts")]
        public string InPackets { get; set; }

        /// <summary>
        /// Gets or sets the number of outgoing packets
        /// </summary>
        [JsonProperty("outpkts")]
        public string OutPackets { get; set; }

        /// <summary>
        /// Gets or sets the number of incoming bytes
        /// </summary>
        [JsonProperty("inbytes")]
        public string InBytes { get; set; }

        /// <summary>
        /// Gets or sets the number of outgoing bytes
        /// </summary>
        [JsonProperty("outbytes")]
        public string OutBytes { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for VLAN list
    /// </summary>
    public class VLANListResponse
    {
        /// <summary>
        /// Gets or sets the list of VLANs
        /// </summary>
        [JsonProperty("rows")]
        public List<VLAN> Rows { get; set; }
    }

    /// <summary>
    /// Information about a VLAN
    /// </summary>
    public class VLAN
    {
        /// <summary>
        /// Gets or sets the UUID of the VLAN
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the interface of the VLAN
        /// </summary>
        [JsonProperty("if")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the tag of the VLAN
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the priority of the VLAN
        /// </summary>
        [JsonProperty("pcp")]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the description of the VLAN
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for VLAN details
    /// </summary>
    public class VLANResponse
    {
        /// <summary>
        /// Gets or sets the VLAN details
        /// </summary>
        [JsonProperty("vlan")]
        public VLANDetail Vlan { get; set; }
    }

    /// <summary>
    /// Detailed information about a VLAN
    /// </summary>
    public class VLANDetail
    {
        /// <summary>
        /// Gets or sets the interface of the VLAN
        /// </summary>
        [JsonProperty("if")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the tag of the VLAN
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the priority of the VLAN
        /// </summary>
        [JsonProperty("pcp")]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the description of the VLAN
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Configuration for a VLAN
    /// </summary>
    public class VLANConfig
    {
        /// <summary>
        /// Gets or sets the interface of the VLAN
        /// </summary>
        [JsonProperty("if")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the tag of the VLAN
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the priority of the VLAN
        /// </summary>
        [JsonProperty("pcp")]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the description of the VLAN
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for VLAN creation
    /// </summary>
    public class VLANCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the created VLAN
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for VLAN update
    /// </summary>
    public class VLANUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for VLAN deletion
    /// </summary>
    public class VLANDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
