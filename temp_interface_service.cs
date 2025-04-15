using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Services
{
    public class InterfaceService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        public InterfaceService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<InterfaceListResponse> GetInterfacesAsync()
        {
            _logger.Information("Getting interfaces");
            
            var endpoint = "interfaces/overview/searchInterfaces";
            return await _apiClient.GetAsync<InterfaceListResponse>(endpoint);
        }

        public async Task<InterfaceDetailResponse> GetInterfaceDetailAsync(string interfaceName)
        {
            _logger.Information($"Getting interface {interfaceName}");
            
            var endpoint = $"interfaces/overview/getInterface/{interfaceName}";
            return await _apiClient.GetAsync<InterfaceDetailResponse>(endpoint);
        }

        public async Task<InterfaceStatisticsResponse> GetInterfaceStatisticsAsync()
        {
            _logger.Information("Getting interface statistics");
            
            var endpoint = "diagnostics/interface/getInterfaceStatistics";
            return await _apiClient.GetAsync<InterfaceStatisticsResponse>(endpoint);
        }

        public async Task<InterfaceUpdateResponse> UpdateInterfaceAsync(string interfaceName, InterfaceConfig interfaceConfig)
        {
            _logger.Information($"Updating interface {interfaceName}");
            
            var endpoint = $"interfaces/overview/setInterface/{interfaceName}";
            var data = new { interface = interfaceConfig };
            return await _apiClient.PostAsync<InterfaceUpdateResponse>(endpoint, data);
        }

        public async Task<InterfaceRestartResponse> RestartInterfaceAsync(string interfaceName)
        {
            _logger.Information($"Restarting interface {interfaceName}");
            
            var endpoint = $"interfaces/overview/reconfigure/{interfaceName}";
            return await _apiClient.PostAsync<InterfaceRestartResponse>(endpoint);
        }

        public async Task<VLANListResponse> GetVLANsAsync()
        {
            _logger.Information("Getting VLANs");
            
            var endpoint = "interfaces/vlan/searchItem";
            return await _apiClient.GetAsync<VLANListResponse>(endpoint);
        }

        public async Task<VLANResponse> GetVLANAsync(string uuid)
        {
            _logger.Information($"Getting VLAN with UUID {uuid}");
            
            var endpoint = $"interfaces/vlan/getItem/{uuid}";
            return await _apiClient.GetAsync<VLANResponse>(endpoint);
        }

        public async Task<VLANCreateResponse> CreateVLANAsync(VLANConfig vlan)
        {
            _logger.Information("Creating VLAN");
            
            var endpoint = "interfaces/vlan/addItem";
            var data = new { vlan = vlan };
            return await _apiClient.PostAsync<VLANCreateResponse>(endpoint, data);
        }

        public async Task<VLANUpdateResponse> UpdateVLANAsync(string uuid, VLANConfig vlan)
        {
            _logger.Information($"Updating VLAN with UUID {uuid}");
            
            var endpoint = $"interfaces/vlan/setItem/{uuid}";
            var data = new { vlan = vlan };
            return await _apiClient.PostAsync<VLANUpdateResponse>(endpoint, data);
        }

        public async Task<VLANDeleteResponse> DeleteVLANAsync(string uuid)
        {
            _logger.Information($"Deleting VLAN with UUID {uuid}");
            
            var endpoint = $"interfaces/vlan/delItem/{uuid}";
            return await _apiClient.PostAsync<VLANDeleteResponse>(endpoint);
        }
    }

    public class InterfaceListResponse
    {
        [JsonProperty("rows")]
        public List<InterfaceInfo> Interfaces { get; set; }
    }

    public class InterfaceInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InterfaceDetailResponse
    {
        [JsonProperty("interface")]
        public InterfaceDetail Interface { get; set; }
    }

    public class InterfaceDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("enable")]
        public string Enabled { get; set; }
    }

    public class InterfaceConfig
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("enable")]
        public string Enabled { get; set; }
    }

    public class InterfaceUpdateResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    public class InterfaceRestartResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InterfaceStatisticsResponse
    {
        [JsonProperty("statistics")]
        public Dictionary<string, InterfaceStatistics> Statistics { get; set; }
    }

    public class InterfaceStatistics
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("inpkts")]
        public string InPackets { get; set; }

        [JsonProperty("outpkts")]
        public string OutPackets { get; set; }

        [JsonProperty("inbytes")]
        public string InBytes { get; set; }

        [JsonProperty("outbytes")]
        public string OutBytes { get; set; }
    }

    public class VLANListResponse
    {
        [JsonProperty("rows")]
        public List<VLAN> Rows { get; set; }
    }

    public class VLAN
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("if")]
        public string Interface { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("pcp")]
        public string Priority { get; set; }

        [JsonProperty("descr")]
        public string Description { get; set; }
    }

    public class VLANResponse
    {
        [JsonProperty("vlan")]
        public VLANDetail Vlan { get; set; }
    }

    public class VLANDetail
    {
        [JsonProperty("if")]
        public string Interface { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("pcp")]
        public string Priority { get; set; }

        [JsonProperty("descr")]
        public string Description { get; set; }
    }

    public class VLANConfig
    {
        [JsonProperty("if")]
        public string Interface { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("pcp")]
        public string Priority { get; set; }

        [JsonProperty("descr")]
        public string Description { get; set; }
    }

    public class VLANCreateResponse
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    public class VLANUpdateResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    public class VLANDeleteResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
