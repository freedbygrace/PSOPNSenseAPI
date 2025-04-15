using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense DHCP API
    /// </summary>
    public class DHCPService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DHCPService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public DHCPService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets all DHCP leases
        /// </summary>
        /// <returns>A list of DHCP leases</returns>
        public async Task<DHCPLeaseListResponse> GetLeasesAsync()
        {
            _logger.Information("Getting DHCP leases");

            var endpoint = "dhcp/leases/searchLease";
            return await _apiClient.GetAsync<DHCPLeaseListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a specific DHCP lease by MAC address
        /// </summary>
        /// <param name="macAddress">The MAC address of the lease</param>
        /// <returns>The DHCP lease</returns>
        public async Task<DHCPLeaseResponse> GetLeaseByMacAsync(string macAddress)
        {
            _logger.Information($"Getting DHCP lease for MAC address {macAddress}");

            var endpoint = $"dhcp/leases/getLeaseByMac/{macAddress}";
            return await _apiClient.GetAsync<DHCPLeaseResponse>(endpoint);
        }

        /// <summary>
        /// Deletes a DHCP lease
        /// </summary>
        /// <param name="macAddress">The MAC address of the lease to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPLeaseDeleteResponse> DeleteLeaseAsync(string macAddress)
        {
            _logger.Information($"Deleting DHCP lease for MAC address {macAddress}");

            var endpoint = $"dhcp/leases/delLease/{macAddress}";
            return await _apiClient.PostAsync<DHCPLeaseDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Adds a static DHCP lease
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="lease">The lease to add</param>
        /// <returns>The response containing the UUID of the new lease</returns>
        public async Task<DHCPStaticMappingCreateResponse> AddStaticLeaseAsync(string @interface, DHCPStaticMappingConfig lease)
        {
            _logger.Information($"Adding static DHCP lease for interface {@interface}");

            return await CreateStaticMappingAsync(@interface, lease);
        }

        /// <summary>
        /// Gets all DHCP servers
        /// </summary>
        /// <returns>A list of DHCP servers</returns>
        public async Task<DHCPServerListResponse> GetServersAsync()
        {
            _logger.Information("Getting DHCP servers");

            var endpoint = "dhcp/service/get";
            return await _apiClient.GetAsync<DHCPServerListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a DHCP server by interface
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <returns>The DHCP server</returns>
        public async Task<DHCPServerResponse> GetServerAsync(string @interface)
        {
            _logger.Information($"Getting DHCP server for interface {@interface}");

            var endpoint = $"dhcp/service/getServer/{@interface}";
            return await _apiClient.GetAsync<DHCPServerResponse>(endpoint);
        }

        /// <summary>
        /// Updates a DHCP server
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="server">The updated server</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPServerUpdateResponse> UpdateServerAsync(string @interface, DHCPServerConfig server)
        {
            _logger.Information($"Updating DHCP server for interface {@interface}");

            var endpoint = $"dhcp/service/setServer/{@interface}";
            var data = new { server = server };
            return await _apiClient.PostAsync<DHCPServerUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Enables a DHCP server
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPServerEnableResponse> EnableServerAsync(string @interface)
        {
            _logger.Information($"Enabling DHCP server for interface {@interface}");

            var endpoint = $"dhcp/service/enableServer/{@interface}";
            return await _apiClient.PostAsync<DHCPServerEnableResponse>(endpoint);
        }

        /// <summary>
        /// Disables a DHCP server
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPServerDisableResponse> DisableServerAsync(string @interface)
        {
            _logger.Information($"Disabling DHCP server for interface {@interface}");

            var endpoint = $"dhcp/service/disableServer/{@interface}";
            return await _apiClient.PostAsync<DHCPServerDisableResponse>(endpoint);
        }

        /// <summary>
        /// Gets all static mappings for a DHCP server
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <returns>A list of static mappings</returns>
        public async Task<DHCPStaticMappingListResponse> GetStaticMappingsAsync(string @interface)
        {
            _logger.Information($"Getting static mappings for interface {@interface}");

            var endpoint = $"dhcp/service/searchStaticMapping/{@interface}";
            return await _apiClient.GetAsync<DHCPStaticMappingListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a static mapping by UUID
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="uuid">The UUID of the static mapping</param>
        /// <returns>The static mapping</returns>
        public async Task<DHCPStaticMappingResponse> GetStaticMappingAsync(string @interface, string uuid)
        {
            _logger.Information($"Getting static mapping with UUID {uuid} for interface {@interface}");

            var endpoint = $"dhcp/service/getStaticMapping/{@interface}/{uuid}";
            return await _apiClient.GetAsync<DHCPStaticMappingResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new static mapping
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="mapping">The static mapping to create</param>
        /// <returns>The response containing the UUID of the new static mapping</returns>
        public async Task<DHCPStaticMappingCreateResponse> CreateStaticMappingAsync(string @interface, DHCPStaticMappingConfig mapping)
        {
            _logger.Information($"Creating static mapping for interface {@interface}");

            var endpoint = $"dhcp/service/addStaticMapping/{@interface}";
            var data = new { mapping = mapping };
            return await _apiClient.PostAsync<DHCPStaticMappingCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a static mapping
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="uuid">The UUID of the static mapping to update</param>
        /// <param name="mapping">The updated static mapping</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPStaticMappingUpdateResponse> UpdateStaticMappingAsync(string @interface, string uuid, DHCPStaticMappingConfig mapping)
        {
            _logger.Information($"Updating static mapping with UUID {uuid} for interface {@interface}");

            var endpoint = $"dhcp/service/setStaticMapping/{@interface}/{uuid}";
            var data = new { mapping = mapping };
            return await _apiClient.PostAsync<DHCPStaticMappingUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a static mapping
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="uuid">The UUID of the static mapping to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPStaticMappingDeleteResponse> DeleteStaticMappingAsync(string @interface, string uuid)
        {
            _logger.Information($"Deleting static mapping with UUID {uuid} for interface {@interface}");

            var endpoint = $"dhcp/service/delStaticMapping/{@interface}/{uuid}";
            return await _apiClient.PostAsync<DHCPStaticMappingDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Gets DHCP options for an interface
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <returns>The DHCP options</returns>
        public async Task<DHCPOptionsListResponse> GetOptionsAsync(string @interface)
        {
            _logger.Information($"Getting DHCP options for interface {@interface}");

            var endpoint = $"dhcp/service/searchOptions/{@interface}";
            return await _apiClient.GetAsync<DHCPOptionsListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a DHCP option by UUID
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="uuid">The UUID of the option</param>
        /// <returns>The DHCP option</returns>
        public async Task<DHCPOptionResponse> GetOptionAsync(string @interface, string uuid)
        {
            _logger.Information($"Getting DHCP option with UUID {uuid} for interface {@interface}");

            var endpoint = $"dhcp/service/getOption/{@interface}/{uuid}";
            return await _apiClient.GetAsync<DHCPOptionResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new DHCP option
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="option">The option to create</param>
        /// <returns>The response containing the UUID of the new option</returns>
        public async Task<DHCPOptionCreateResponse> CreateOptionAsync(string @interface, DHCPOptionConfig option)
        {
            _logger.Information($"Creating DHCP option for interface {@interface}");

            var endpoint = $"dhcp/service/addOption/{@interface}";
            var data = new { option = option };
            return await _apiClient.PostAsync<DHCPOptionCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a DHCP option
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="uuid">The UUID of the option to update</param>
        /// <param name="option">The updated option</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPOptionUpdateResponse> UpdateOptionAsync(string @interface, string uuid, DHCPOptionConfig option)
        {
            _logger.Information($"Updating DHCP option with UUID {uuid} for interface {@interface}");

            var endpoint = $"dhcp/service/setOption/{@interface}/{uuid}";
            var data = new { option = option };
            return await _apiClient.PostAsync<DHCPOptionUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a DHCP option
        /// </summary>
        /// <param name="interface">The interface name</param>
        /// <param name="uuid">The UUID of the option to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPOptionDeleteResponse> DeleteOptionAsync(string @interface, string uuid)
        {
            _logger.Information($"Deleting DHCP option with UUID {uuid} for interface {@interface}");

            var endpoint = $"dhcp/service/delOption/{@interface}/{uuid}";
            return await _apiClient.PostAsync<DHCPOptionDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Applies DHCP changes
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<DHCPApplyResponse> ApplyChangesAsync()
        {
            _logger.Information("Applying DHCP changes");

            var endpoint = "dhcp/service/reconfigure";
            return await _apiClient.PostAsync<DHCPApplyResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting DHCP leases
    /// </summary>
    public class DHCPLeaseListResponse
    {
        /// <summary>
        /// Gets or sets the total number of leases
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
        /// Gets or sets the leases
        /// </summary>
        [JsonProperty("rows")]
        public List<DHCPLease> Rows { get; set; }
    }

    /// <summary>
    /// DHCP lease
    /// </summary>
    public class DHCPLease
    {
        /// <summary>
        /// Gets or sets the MAC address of the lease
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the lease
        /// </summary>
        [JsonProperty("ip")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the lease
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the start time of the lease
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the lease
        /// </summary>
        [JsonProperty("end")]
        public string End { get; set; }

        /// <summary>
        /// Gets or sets the state of the lease
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the type of the lease
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Response for getting DHCP servers
    /// </summary>
    public class DHCPServerListResponse
    {
        /// <summary>
        /// Gets or sets the DHCP servers
        /// </summary>
        [JsonProperty("dhcpd")]
        public DHCPServers Servers { get; set; }
    }

    /// <summary>
    /// DHCP servers
    /// </summary>
    public class DHCPServers
    {
        /// <summary>
        /// Gets or sets whether DHCP is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the interfaces
        /// </summary>
        [JsonProperty("interfaces")]
        public Dictionary<string, DHCPServerInfo> Interfaces { get; set; }
    }

    /// <summary>
    /// DHCP server information
    /// </summary>
    public class DHCPServerInfo
    {
        /// <summary>
        /// Gets or sets whether the server is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the range start
        /// </summary>
        [JsonProperty("range_from")]
        public string RangeFrom { get; set; }

        /// <summary>
        /// Gets or sets the range end
        /// </summary>
        [JsonProperty("range_to")]
        public string RangeTo { get; set; }

        /// <summary>
        /// Gets or sets the default lease time
        /// </summary>
        [JsonProperty("defaultleasetime")]
        public string DefaultLeaseTime { get; set; }

        /// <summary>
        /// Gets or sets the maximum lease time
        /// </summary>
        [JsonProperty("maxleasetime")]
        public string MaxLeaseTime { get; set; }

        /// <summary>
        /// Gets or sets the domain name
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers
        /// </summary>
        [JsonProperty("dnsserver")]
        public List<string> DnsServers { get; set; }

        /// <summary>
        /// Gets or sets the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }
    }

    /// <summary>
    /// Response for getting a DHCP server
    /// </summary>
    public class DHCPServerResponse
    {
        /// <summary>
        /// Gets or sets the server
        /// </summary>
        [JsonProperty("server")]
        public DHCPServerDetail Server { get; set; }
    }

    /// <summary>
    /// DHCP server details
    /// </summary>
    public class DHCPServerDetail
    {
        /// <summary>
        /// Gets or sets whether the server is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the range start
        /// </summary>
        [JsonProperty("range_from")]
        public string RangeFrom { get; set; }

        /// <summary>
        /// Gets or sets the range end
        /// </summary>
        [JsonProperty("range_to")]
        public string RangeTo { get; set; }

        /// <summary>
        /// Gets or sets the default lease time
        /// </summary>
        [JsonProperty("defaultleasetime")]
        public string DefaultLeaseTime { get; set; }

        /// <summary>
        /// Gets or sets the maximum lease time
        /// </summary>
        [JsonProperty("maxleasetime")]
        public string MaxLeaseTime { get; set; }

        /// <summary>
        /// Gets or sets the domain name
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers
        /// </summary>
        [JsonProperty("dnsserver")]
        public List<string> DnsServers { get; set; }

        /// <summary>
        /// Gets or sets the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }
    }

    /// <summary>
    /// DHCP server configuration
    /// </summary>
    public class DHCPServerConfig
    {
        /// <summary>
        /// Gets or sets whether the server is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; } = "1";

        /// <summary>
        /// Gets or sets the range start
        /// </summary>
        [JsonProperty("range_from")]
        public string RangeFrom { get; set; }

        /// <summary>
        /// Gets or sets the range end
        /// </summary>
        [JsonProperty("range_to")]
        public string RangeTo { get; set; }

        /// <summary>
        /// Gets or sets the default lease time
        /// </summary>
        [JsonProperty("defaultleasetime")]
        public string DefaultLeaseTime { get; set; } = "7200";

        /// <summary>
        /// Gets or sets the maximum lease time
        /// </summary>
        [JsonProperty("maxleasetime")]
        public string MaxLeaseTime { get; set; } = "86400";

        /// <summary>
        /// Gets or sets the domain name
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers
        /// </summary>
        [JsonProperty("dnsserver")]
        public List<string> DnsServers { get; set; }

        /// <summary>
        /// Gets or sets the gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }
    }

    /// <summary>
    /// Response for updating a DHCP server
    /// </summary>
    public class DHCPServerUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for enabling a DHCP server
    /// </summary>
    public class DHCPServerEnableResponse
    {
        /// <summary>
        /// Gets or sets the result of the enable
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for disabling a DHCP server
    /// </summary>
    public class DHCPServerDisableResponse
    {
        /// <summary>
        /// Gets or sets the result of the disable
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for getting static mappings
    /// </summary>
    public class DHCPStaticMappingListResponse
    {
        /// <summary>
        /// Gets or sets the total number of mappings
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
        /// Gets or sets the mappings
        /// </summary>
        [JsonProperty("rows")]
        public List<DHCPStaticMapping> Rows { get; set; }
    }

    /// <summary>
    /// DHCP static mapping
    /// </summary>
    public class DHCPStaticMapping
    {
        /// <summary>
        /// Gets or sets the UUID of the mapping
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the MAC address of the mapping
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the mapping
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the mapping
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the description of the mapping
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the mapping is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response for getting a static mapping
    /// </summary>
    public class DHCPStaticMappingResponse
    {
        /// <summary>
        /// Gets or sets the mapping
        /// </summary>
        [JsonProperty("mapping")]
        public DHCPStaticMappingDetail Mapping { get; set; }
    }

    /// <summary>
    /// DHCP static mapping details
    /// </summary>
    public class DHCPStaticMappingDetail
    {
        /// <summary>
        /// Gets or sets the MAC address of the mapping
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the mapping
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the mapping
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the description of the mapping
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the mapping is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// DHCP static mapping configuration
    /// </summary>
    public class DHCPStaticMappingConfig
    {
        /// <summary>
        /// Gets or sets the MAC address of the mapping
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the mapping
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the mapping
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the description of the mapping
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the mapping is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; } = "1";
    }

    /// <summary>
    /// Response for creating a static mapping
    /// </summary>
    public class DHCPStaticMappingCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new mapping
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a static mapping
    /// </summary>
    public class DHCPStaticMappingUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a static mapping
    /// </summary>
    public class DHCPStaticMappingDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for applying DHCP changes
    /// </summary>
    public class DHCPApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting a DHCP lease by MAC address
    /// </summary>
    public class DHCPLeaseResponse
    {
        /// <summary>
        /// Gets or sets the lease
        /// </summary>
        [JsonProperty("lease")]
        public DHCPLeaseDetail Lease { get; set; }
    }

    /// <summary>
    /// DHCP lease details
    /// </summary>
    public class DHCPLeaseDetail
    {
        /// <summary>
        /// Gets or sets the MAC address of the lease
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the lease
        /// </summary>
        [JsonProperty("address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the lease
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the start time of the lease
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the lease
        /// </summary>
        [JsonProperty("end")]
        public string End { get; set; }

        /// <summary>
        /// Gets or sets the state of the lease
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the type of the lease
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Response for deleting a DHCP lease
    /// </summary>
    public class DHCPLeaseDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for getting DHCP options
    /// </summary>
    public class DHCPOptionsListResponse
    {
        /// <summary>
        /// Gets or sets the total number of options
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
        /// Gets or sets the options
        /// </summary>
        [JsonProperty("rows")]
        public List<DHCPOption> Rows { get; set; }
    }

    /// <summary>
    /// DHCP option
    /// </summary>
    public class DHCPOption
    {
        /// <summary>
        /// Gets or sets the UUID of the option
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the number of the option
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the value of the option
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the option
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the option
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response for getting a DHCP option
    /// </summary>
    public class DHCPOptionResponse
    {
        /// <summary>
        /// Gets or sets the option
        /// </summary>
        [JsonProperty("option")]
        public DHCPOptionDetail Option { get; set; }
    }

    /// <summary>
    /// DHCP option details
    /// </summary>
    public class DHCPOptionDetail
    {
        /// <summary>
        /// Gets or sets the number of the option
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the value of the option
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the option
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the option
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// DHCP option configuration
    /// </summary>
    public class DHCPOptionConfig
    {
        /// <summary>
        /// Gets or sets the number of the option
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the value of the option
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the option
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "string";

        /// <summary>
        /// Gets or sets the description of the option
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response for creating a DHCP option
    /// </summary>
    public class DHCPOptionCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new option
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a DHCP option
    /// </summary>
    public class DHCPOptionUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a DHCP option
    /// </summary>
    public class DHCPOptionDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
