using System.Collections.Generic;
using Newtonsoft.Json;

namespace PSOPNSenseAPI.Models
{
    /// <summary>
    /// Represents a port forwarding rule in OPNSense
    /// </summary>
    public class PortForwardingRule
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
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the description of the rule
        /// </summary>
        [JsonProperty("descr")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the interface for the rule
        /// </summary>
        [JsonProperty("interface")]
        public string Interface { get; set; }

        /// <summary>
        /// Gets or sets the TCP/IP protocol (tcp, udp, tcp/udp)
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; } = "tcp";

        /// <summary>
        /// Gets or sets the source address for the rule
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; } = "any";

        /// <summary>
        /// Gets or sets the source port range for the rule
        /// </summary>
        [JsonProperty("sourceport")]
        public string SourcePort { get; set; } = "any";

        /// <summary>
        /// Gets or sets the destination address for the rule
        /// </summary>
        [JsonProperty("destination")]
        public string Destination { get; set; } = "wanip";

        /// <summary>
        /// Gets or sets the destination port range for the rule
        /// </summary>
        [JsonProperty("destinationport")]
        public string DestinationPort { get; set; }

        /// <summary>
        /// Gets or sets the target IP address for the rule
        /// </summary>
        [JsonProperty("target")]
        public string TargetIP { get; set; }

        /// <summary>
        /// Gets or sets the target port for the rule
        /// </summary>
        [JsonProperty("targetport")]
        public string TargetPort { get; set; }

        /// <summary>
        /// Gets or sets the NAT reflection mode (enable, disable, purenat)
        /// </summary>
        [JsonProperty("natreflection")]
        public string NatReflection { get; set; } = "enable";

        /// <summary>
        /// Gets or sets whether to filter rule association is enabled
        /// </summary>
        [JsonProperty("associated-rule-id")]
        public bool FilterRuleAssociation { get; set; } = true;

        /// <summary>
        /// Gets or sets whether logging is enabled for this rule
        /// </summary>
        [JsonProperty("log")]
        public bool Log { get; set; } = false;
    }

    /// <summary>
    /// Represents a collection of port forwarding rules
    /// </summary>
    public class PortForwardingRules
    {
        /// <summary>
        /// Gets or sets the list of port forwarding rules
        /// </summary>
        [JsonProperty("rules")]
        public List<PortForwardingRule> Rules { get; set; } = new List<PortForwardingRule>();
    }
}
