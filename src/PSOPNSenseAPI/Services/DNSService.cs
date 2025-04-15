using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSOPNSenseAPI.Logging;

namespace PSOPNSenseAPI.Services
{
    /// <summary>
    /// Service for interacting with the OPNSense DNS API
    /// </summary>
    public class DNSService
    {
        private readonly OPNSenseApiClient _apiClient;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DNSService"/> class
        /// </summary>
        /// <param name="apiClient">The API client</param>
        /// <param name="logger">The logger</param>
        public DNSService(OPNSenseApiClient apiClient, ILogger logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Gets the DNS configuration
        /// </summary>
        /// <returns>The DNS configuration</returns>
        public async Task<DNSConfigResponse> GetDNSConfigAsync()
        {
            _logger.Information("Getting DNS configuration");

            var endpoint = "unbound/settings/get";
            return await _apiClient.GetAsync<DNSConfigResponse>(endpoint);
        }

        /// <summary>
        /// Updates the DNS configuration
        /// </summary>
        /// <param name="config">The updated DNS configuration</param>
        /// <returns>The response indicating success</returns>
        public async Task<DNSUpdateResponse> UpdateDNSConfigAsync(DNSConfig config)
        {
            _logger.Information("Updating DNS configuration");

            var endpoint = "unbound/settings/set";
            var data = new { unbound = config };
            return await _apiClient.PostAsync<DNSUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Gets all DNS overrides
        /// </summary>
        /// <returns>A list of DNS overrides</returns>
        public async Task<DNSOverrideListResponse> GetDNSOverridesAsync()
        {
            _logger.Information("Getting DNS overrides");

            var endpoint = "unbound/overrides/searchHostOverride";
            return await _apiClient.GetAsync<DNSOverrideListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a DNS override by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the override</param>
        /// <returns>The DNS override</returns>
        public async Task<DNSOverrideResponse> GetDNSOverrideAsync(string uuid)
        {
            _logger.Information($"Getting DNS override with UUID {uuid}");

            var endpoint = $"unbound/overrides/getHostOverride/{uuid}";
            return await _apiClient.GetAsync<DNSOverrideResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new DNS override
        /// </summary>
        /// <param name="override">The override to create</param>
        /// <returns>The response containing the UUID of the new override</returns>
        public async Task<DNSOverrideCreateResponse> CreateDNSOverrideAsync(DNSOverrideConfig @override)
        {
            _logger.Information("Creating DNS override");

            var endpoint = "unbound/overrides/addHostOverride";
            var data = new { host = @override };
            return await _apiClient.PostAsync<DNSOverrideCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a DNS override
        /// </summary>
        /// <param name="uuid">The UUID of the override to update</param>
        /// <param name="override">The updated override</param>
        /// <returns>The response indicating success</returns>
        public async Task<DNSOverrideUpdateResponse> UpdateDNSOverrideAsync(string uuid, DNSOverrideConfig @override)
        {
            _logger.Information($"Updating DNS override with UUID {uuid}");

            var endpoint = $"unbound/overrides/setHostOverride/{uuid}";
            var data = new { host = @override };
            return await _apiClient.PostAsync<DNSOverrideUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a DNS override
        /// </summary>
        /// <param name="uuid">The UUID of the override to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<DNSOverrideDeleteResponse> DeleteDNSOverrideAsync(string uuid)
        {
            _logger.Information($"Deleting DNS override with UUID {uuid}");

            var endpoint = $"unbound/overrides/delHostOverride/{uuid}";
            return await _apiClient.PostAsync<DNSOverrideDeleteResponse>(endpoint);
        }

        /// <summary>
        /// Applies DNS configuration changes
        /// </summary>
        /// <returns>The response indicating success</returns>
        public async Task<DNSApplyResponse> ApplyDNSChangesAsync()
        {
            _logger.Information("Applying DNS changes");

            var endpoint = "unbound/service/reconfigure";
            return await _apiClient.PostAsync<DNSApplyResponse>(endpoint);
        }

        /// <summary>
        /// Gets the DNS forwarding configuration
        /// </summary>
        /// <returns>The DNS forwarding configuration</returns>
        public async Task<DNSForwardingResponse> GetDNSForwardingAsync()
        {
            _logger.Information("Getting DNS forwarding configuration");

            var endpoint = "unbound/forward/get";
            return await _apiClient.GetAsync<DNSForwardingResponse>(endpoint);
        }

        /// <summary>
        /// Updates the DNS forwarding configuration
        /// </summary>
        /// <param name="config">The updated forwarding configuration</param>
        /// <returns>The response indicating success</returns>
        public async Task<DNSForwardingUpdateResponse> UpdateDNSForwardingAsync(DNSForwardingConfig config)
        {
            _logger.Information("Updating DNS forwarding configuration");

            var endpoint = "unbound/forward/set";
            var data = new { forward = config };
            return await _apiClient.PostAsync<DNSForwardingUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Gets the DNS forwarding hosts
        /// </summary>
        /// <returns>The DNS forwarding hosts</returns>
        public async Task<DNSForwardingHostListResponse> GetDNSForwardingHostsAsync()
        {
            _logger.Information("Getting DNS forwarding hosts");

            var endpoint = "unbound/forward/searchHost";
            return await _apiClient.GetAsync<DNSForwardingHostListResponse>(endpoint);
        }

        /// <summary>
        /// Gets a DNS forwarding host by UUID
        /// </summary>
        /// <param name="uuid">The UUID of the forwarding host</param>
        /// <returns>The DNS forwarding host</returns>
        public async Task<DNSForwardingHostResponse> GetDNSForwardingHostAsync(string uuid)
        {
            _logger.Information($"Getting DNS forwarding host with UUID {uuid}");

            var endpoint = $"unbound/forward/getHost/{uuid}";
            return await _apiClient.GetAsync<DNSForwardingHostResponse>(endpoint);
        }

        /// <summary>
        /// Creates a new DNS forwarding host
        /// </summary>
        /// <param name="host">The forwarding host to create</param>
        /// <returns>The response containing the UUID of the new forwarding host</returns>
        public async Task<DNSForwardingHostCreateResponse> CreateDNSForwardingHostAsync(DNSForwardingHostConfig host)
        {
            _logger.Information("Creating DNS forwarding host");

            var endpoint = "unbound/forward/addHost";
            var data = new { host = host };
            return await _apiClient.PostAsync<DNSForwardingHostCreateResponse>(endpoint, data);
        }

        /// <summary>
        /// Updates a DNS forwarding host
        /// </summary>
        /// <param name="uuid">The UUID of the forwarding host to update</param>
        /// <param name="host">The updated forwarding host</param>
        /// <returns>The response indicating success</returns>
        public async Task<DNSForwardingHostUpdateResponse> UpdateDNSForwardingHostAsync(string uuid, DNSForwardingHostConfig host)
        {
            _logger.Information($"Updating DNS forwarding host with UUID {uuid}");

            var endpoint = $"unbound/forward/setHost/{uuid}";
            var data = new { host = host };
            return await _apiClient.PostAsync<DNSForwardingHostUpdateResponse>(endpoint, data);
        }

        /// <summary>
        /// Deletes a DNS forwarding host
        /// </summary>
        /// <param name="uuid">The UUID of the forwarding host to delete</param>
        /// <returns>The response indicating success</returns>
        public async Task<DNSForwardingHostDeleteResponse> DeleteDNSForwardingHostAsync(string uuid)
        {
            _logger.Information($"Deleting DNS forwarding host with UUID {uuid}");

            var endpoint = $"unbound/forward/delHost/{uuid}";
            return await _apiClient.PostAsync<DNSForwardingHostDeleteResponse>(endpoint);
        }
    }

    /// <summary>
    /// Response for getting DNS configuration
    /// </summary>
    public class DNSConfigResponse
    {
        /// <summary>
        /// Gets or sets the DNS configuration
        /// </summary>
        [JsonProperty("unbound")]
        public DNSConfig Unbound { get; set; }
    }

    /// <summary>
    /// DNS configuration
    /// </summary>
    public class DNSConfig
    {
        /// <summary>
        /// Gets or sets whether DNS is enabled
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the DNS port
        /// </summary>
        [JsonProperty("port")]
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the DNS interfaces
        /// </summary>
        [JsonProperty("interfaces")]
        public List<string> Interfaces { get; set; }

        /// <summary>
        /// Gets or sets the DNS forwarders
        /// </summary>
        [JsonProperty("forwarding")]
        public string Forwarding { get; set; }

        /// <summary>
        /// Gets or sets the DNS forwarder addresses
        /// </summary>
        [JsonProperty("forwarders")]
        public List<string> Forwarders { get; set; }

        /// <summary>
        /// Gets or sets the DNS domain
        /// </summary>
        [JsonProperty("regdhcp")]
        public string RegisterDhcp { get; set; }

        /// <summary>
        /// Gets or sets the DNS domain
        /// </summary>
        [JsonProperty("regdhcpdomain")]
        public string RegisterDhcpDomain { get; set; }

        /// <summary>
        /// Gets or sets the DNS domain
        /// </summary>
        [JsonProperty("regdhcpstatic")]
        public string RegisterDhcpStatic { get; set; }

        /// <summary>
        /// Gets or sets the DNS domain
        /// </summary>
        [JsonProperty("active_interface")]
        public List<string> ActiveInterfaces { get; set; }
    }

    /// <summary>
    /// Response for updating DNS configuration
    /// </summary>
    public class DNSUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for getting DNS overrides
    /// </summary>
    public class DNSOverrideListResponse
    {
        /// <summary>
        /// Gets or sets the total number of overrides
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
        /// Gets or sets the overrides
        /// </summary>
        [JsonProperty("rows")]
        public List<DNSOverride> Rows { get; set; }
    }

    /// <summary>
    /// DNS override
    /// </summary>
    public class DNSOverride
    {
        /// <summary>
        /// Gets or sets the UUID of the override
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the override
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the domain of the override
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the override
        /// </summary>
        [JsonProperty("server")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the description of the override
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the override is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response for getting a DNS override
    /// </summary>
    public class DNSOverrideResponse
    {
        /// <summary>
        /// Gets or sets the override
        /// </summary>
        [JsonProperty("host")]
        public DNSOverrideDetail Host { get; set; }
    }

    /// <summary>
    /// DNS override details
    /// </summary>
    public class DNSOverrideDetail
    {
        /// <summary>
        /// Gets or sets whether the override is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the hostname of the override
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the domain of the override
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the override
        /// </summary>
        [JsonProperty("server")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the description of the override
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// DNS override configuration
    /// </summary>
    public class DNSOverrideConfig
    {
        /// <summary>
        /// Gets or sets whether the override is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "1";

        /// <summary>
        /// Gets or sets the hostname of the override
        /// </summary>
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// Gets or sets the domain of the override
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the override
        /// </summary>
        [JsonProperty("server")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the description of the override
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response for creating a DNS override
    /// </summary>
    public class DNSOverrideCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new override
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a DNS override
    /// </summary>
    public class DNSOverrideUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a DNS override
    /// </summary>
    public class DNSOverrideDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for applying DNS changes
    /// </summary>
    public class DNSApplyResponse
    {
        /// <summary>
        /// Gets or sets the status of the apply
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    /// <summary>
    /// Response for getting DNS forwarding configuration
    /// </summary>
    public class DNSForwardingResponse
    {
        /// <summary>
        /// Gets or sets the DNS forwarding configuration
        /// </summary>
        [JsonProperty("forward")]
        public DNSForwardingConfig Forward { get; set; }
    }

    /// <summary>
    /// DNS forwarding configuration
    /// </summary>
    public class DNSForwardingConfig
    {
        /// <summary>
        /// Gets or sets whether DNS forwarding is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "0";

        /// <summary>
        /// Gets or sets the type of DNS forwarding
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = "forward";

        /// <summary>
        /// Gets or sets the DNS servers to forward to
        /// </summary>
        [JsonProperty("dns")]
        public List<string> DnsServers { get; set; } = new List<string>();
    }

    /// <summary>
    /// Response for updating DNS forwarding configuration
    /// </summary>
    public class DNSForwardingUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for getting DNS forwarding hosts
    /// </summary>
    public class DNSForwardingHostListResponse
    {
        /// <summary>
        /// Gets or sets the total number of hosts
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
        /// Gets or sets the hosts
        /// </summary>
        [JsonProperty("rows")]
        public List<DNSForwardingHost> Rows { get; set; }
    }

    /// <summary>
    /// DNS forwarding host
    /// </summary>
    public class DNSForwardingHost
    {
        /// <summary>
        /// Gets or sets the UUID of the host
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the domain of the host
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers for the host
        /// </summary>
        [JsonProperty("server")]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the description of the host
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the host is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }
    }

    /// <summary>
    /// Response for getting a DNS forwarding host
    /// </summary>
    public class DNSForwardingHostResponse
    {
        /// <summary>
        /// Gets or sets the host
        /// </summary>
        [JsonProperty("host")]
        public DNSForwardingHostDetail Host { get; set; }
    }

    /// <summary>
    /// DNS forwarding host details
    /// </summary>
    public class DNSForwardingHostDetail
    {
        /// <summary>
        /// Gets or sets whether the host is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; }

        /// <summary>
        /// Gets or sets the domain of the host
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers for the host
        /// </summary>
        [JsonProperty("server")]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the description of the host
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// DNS forwarding host configuration
    /// </summary>
    public class DNSForwardingHostConfig
    {
        /// <summary>
        /// Gets or sets whether the host is enabled
        /// </summary>
        [JsonProperty("enabled")]
        public string Enabled { get; set; } = "1";

        /// <summary>
        /// Gets or sets the domain of the host
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the DNS servers for the host
        /// </summary>
        [JsonProperty("server")]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the description of the host
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response for creating a DNS forwarding host
    /// </summary>
    public class DNSForwardingHostCreateResponse
    {
        /// <summary>
        /// Gets or sets the UUID of the new host
        /// </summary>
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    /// <summary>
    /// Response for updating a DNS forwarding host
    /// </summary>
    public class DNSForwardingHostUpdateResponse
    {
        /// <summary>
        /// Gets or sets the result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }

    /// <summary>
    /// Response for deleting a DNS forwarding host
    /// </summary>
    public class DNSForwardingHostDeleteResponse
    {
        /// <summary>
        /// Gets or sets the result of the deletion
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
