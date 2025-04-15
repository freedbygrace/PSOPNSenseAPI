using Newtonsoft.Json;
using System.Collections.Generic;

namespace PSOPNSenseAPI.Models
{
    /// <summary>
    /// Response from the OPNSense API for interface list
    /// </summary>
    public class InterfaceListResponse
    {
        /// <summary>
        /// List of interfaces
        /// </summary>
        [JsonProperty("rows")]
        public List<InterfaceItem> Interfaces { get; set; }
    }

    /// <summary>
    /// Interface item
    /// </summary>
    public class InterfaceItem
    {
        /// <summary>
        /// Interface name
        /// </summary>
        [JsonProperty("if")]
        public string Name { get; set; }

        /// <summary>
        /// Interface status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Interface IPv4 address
        /// </summary>
        [JsonProperty("ipv4")]
        public string IPv4 { get; set; }

        /// <summary>
        /// Interface IPv6 address
        /// </summary>
        [JsonProperty("ipv6")]
        public string IPv6 { get; set; }

        /// <summary>
        /// Interface MAC address
        /// </summary>
        [JsonProperty("mac")]
        public string MAC { get; set; }

        /// <summary>
        /// Interface description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Response from the OPNSense API for interface detail
    /// </summary>
    public class InterfaceDetailResponse
    {
        /// <summary>
        /// Interface detail
        /// </summary>
        [JsonProperty("interface")]
        public InterfaceDetail Interface { get; set; }
    }

    /// <summary>
    /// Interface detail
    /// </summary>
    public class InterfaceDetail
    {
        /// <summary>
        /// Interface name
        /// </summary>
        [JsonProperty("if")]
        public string Name { get; set; }

        /// <summary>
        /// Interface description
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Interface status
        /// </summary>
        [JsonProperty("enable")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Interface IPv4 configuration type
        /// </summary>
        [JsonProperty("ipv4_type")]
        public string IPv4Type { get; set; }

        /// <summary>
        /// Interface IPv4 address
        /// </summary>
        [JsonProperty("ipv4_address")]
        public string IPv4Address { get; set; }

        /// <summary>
        /// Interface IPv4 subnet
        /// </summary>
        [JsonProperty("ipv4_subnet")]
        public string IPv4Subnet { get; set; }

        /// <summary>
        /// Interface IPv6 configuration type
        /// </summary>
        [JsonProperty("ipv6_type")]
        public string IPv6Type { get; set; }

        /// <summary>
        /// Interface IPv6 address
        /// </summary>
        [JsonProperty("ipv6_address")]
        public string IPv6Address { get; set; }

        /// <summary>
        /// Interface IPv6 subnet
        /// </summary>
        [JsonProperty("ipv6_subnet")]
        public string IPv6Subnet { get; set; }

        /// <summary>
        /// Interface block bogon networks
        /// </summary>
        [JsonProperty("blockbogons")]
        public bool BlockBogons { get; set; }

        /// <summary>
        /// Interface block private networks
        /// </summary>
        [JsonProperty("blockpriv")]
        public bool BlockPrivate { get; set; }

        /// <summary>
        /// Interface DHCP configuration
        /// </summary>
        [JsonProperty("dhcp")]
        public string Dhcp { get; set; }

        /// <summary>
        /// Interface MTU
        /// </summary>
        [JsonProperty("mtu")]
        public string Mtu { get; set; }

        /// <summary>
        /// Interface media configuration
        /// </summary>
        [JsonProperty("media")]
        public string Media { get; set; }

        /// <summary>
        /// Interface gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Interface IP address
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Interface subnet mask
        /// </summary>
        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }


    }

    /// <summary>
    /// Interface configuration
    /// </summary>
    public class InterfaceConfig
    {
        /// <summary>
        /// Interface description
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Interface status
        /// </summary>
        [JsonProperty("enable")]
        public string Enabled { get; set; }

        /// <summary>
        /// Interface IPv4 configuration type
        /// </summary>
        [JsonProperty("ipv4_type")]
        public string IPv4Type { get; set; }

        /// <summary>
        /// Interface IPv4 address
        /// </summary>
        [JsonProperty("ipv4_address")]
        public string IPv4Address { get; set; }

        /// <summary>
        /// Interface IPv4 subnet
        /// </summary>
        [JsonProperty("ipv4_subnet")]
        public string IPv4Subnet { get; set; }

        /// <summary>
        /// Interface IPv6 configuration type
        /// </summary>
        [JsonProperty("ipv6_type")]
        public string IPv6Type { get; set; }

        /// <summary>
        /// Interface IPv6 address
        /// </summary>
        [JsonProperty("ipv6_address")]
        public string IPv6Address { get; set; }

        /// <summary>
        /// Interface IPv6 subnet
        /// </summary>
        [JsonProperty("ipv6_subnet")]
        public string IPv6Subnet { get; set; }

        /// <summary>
        /// Interface block bogon networks
        /// </summary>
        [JsonProperty("blockbogons")]
        public string BlockBogons { get; set; }

        /// <summary>
        /// Interface block private networks
        /// </summary>
        [JsonProperty("blockpriv")]
        public string BlockPrivate { get; set; }

        /// <summary>
        /// Interface DHCP configuration
        /// </summary>
        [JsonProperty("dhcp")]
        public string Dhcp { get; set; }

        /// <summary>
        /// Interface MTU
        /// </summary>
        [JsonProperty("mtu")]
        public string Mtu { get; set; }

        /// <summary>
        /// Interface media configuration
        /// </summary>
        [JsonProperty("media")]
        public string Media { get; set; }

        /// <summary>
        /// Interface gateway
        /// </summary>
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        /// <summary>
        /// Interface IP address
        /// </summary>
        [JsonProperty("ipaddr")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Interface subnet mask
        /// </summary>
        [JsonProperty("subnet")]
        public string SubnetMask { get; set; }


    }

    /// <summary>
    /// Response from the OPNSense API for interface update
    /// </summary>
    public class InterfaceUpdateResponse
    {
        /// <summary>
        /// Result of the update
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}
