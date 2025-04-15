using System.Collections.Generic;
using Newtonsoft.Json;

namespace PSOPNSenseAPI.Models
{
    /// <summary>
    /// Response from the OPNSense API for interface statistics
    /// </summary>
    public class InterfaceStatisticsResponse
    {
        /// <summary>
        /// Dictionary of interface statistics
        /// </summary>
        [JsonProperty("statistics")]
        public Dictionary<string, InterfaceStatistics> Statistics { get; set; }
    }

    /// <summary>
    /// Interface statistics
    /// </summary>
    public class InterfaceStatistics
    {
        /// <summary>
        /// Number of incoming packets
        /// </summary>
        [JsonProperty("inpkts")]
        public long InPackets { get; set; }

        /// <summary>
        /// Number of incoming bytes
        /// </summary>
        [JsonProperty("inbytes")]
        public long InBytes { get; set; }

        /// <summary>
        /// Number of incoming errors
        /// </summary>
        [JsonProperty("inerrs")]
        public long InErrors { get; set; }

        /// <summary>
        /// Number of outgoing packets
        /// </summary>
        [JsonProperty("outpkts")]
        public long OutPackets { get; set; }

        /// <summary>
        /// Number of outgoing bytes
        /// </summary>
        [JsonProperty("outbytes")]
        public long OutBytes { get; set; }

        /// <summary>
        /// Number of outgoing errors
        /// </summary>
        [JsonProperty("outerrs")]
        public long OutErrors { get; set; }

        /// <summary>
        /// Number of collisions
        /// </summary>
        [JsonProperty("collisions")]
        public long Collisions { get; set; }
    }
}
