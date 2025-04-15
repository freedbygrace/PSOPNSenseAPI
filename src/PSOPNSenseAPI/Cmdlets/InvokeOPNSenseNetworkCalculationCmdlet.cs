using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Utilities;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Performs network calculations using IPNetwork2.</para>
    /// <para type="description">The Invoke-OPNSenseNetworkCalculation cmdlet performs various network calculations using the IPNetwork2 library.</para>
    /// <example>
    ///     <para>Example 1: Get network information</para>
    ///     <code>Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Info</code>
    ///     <para>This example returns detailed information about the 192.168.1.0/24 network.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Divide a network into subnets</para>
    ///     <code>Invoke-OPNSenseNetworkCalculation -Network "10.0.0.0/16" -Operation Subnet -PrefixLength 24</code>
    ///     <para>This example divides the 10.0.0.0/16 network into /24 subnets.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Check if an IP address is within a network</para>
    ///     <code>Invoke-OPNSenseNetworkCalculation -Network "192.168.1.0/24" -Operation Contains -IPAddress "192.168.1.100"</code>
    ///     <para>This example checks if the IP address 192.168.1.100 is within the 192.168.1.0/24 network.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Invoke, "OPNSenseNetworkCalculation")]
    [OutputType(typeof(PSObject), typeof(bool), typeof(IPNetwork2[]))]
    public class InvokeOPNSenseNetworkCalculationCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The network in CIDR notation to perform calculations on.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Network { get; set; }

        /// <summary>
        /// <para type="description">The operation to perform.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateSet("Info", "Subnet", "SubnetByCount", "Contains", "Overlaps", "Supernet", "SupernetSummarize")]
        public string Operation { get; set; }

        /// <summary>
        /// <para type="description">The prefix length for subnet operations.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 128)]
        public int? PrefixLength { get; set; }

        /// <summary>
        /// <para type="description">The number of subnets for SubnetByCount operations.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(2, int.MaxValue)]
        public int? SubnetCount { get; set; }

        /// <summary>
        /// <para type="description">The IP address for Contains operations.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string IPAddress { get; set; }

        /// <summary>
        /// <para type="description">The second network for Overlaps and Supernet operations.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] AdditionalNetworks { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void BeginProcessing()
        {
            // Override the base implementation to avoid checking for connection
            // since this cmdlet doesn't require a connection
            base.BeginProcessing();
            Logger = new PowerShellLogger(this);
        }

        protected override void ProcessRecordInternal()
        {
            // Parse the network
            IPNetwork2 network;
            try
            {
                network = NetworkUtility.ParseCIDR(Network);
            }
            catch (Exception ex)
            {
                ProcessingException = new ArgumentException($"Invalid network format: {Network}", ex);
                return;
            }

            switch (Operation)
            {
                case "Info":
                    WriteNetworkInfo(network);
                    break;

                case "Subnet":
                    if (!PrefixLength.HasValue)
                    {
                        ProcessingException = new ArgumentException("PrefixLength is required for Subnet operations.");
                        return;
                    }

                    WriteSubnets(network, PrefixLength.Value);
                    break;

                case "SubnetByCount":
                    if (!SubnetCount.HasValue)
                    {
                        ProcessingException = new ArgumentException("SubnetCount is required for SubnetByCount operations.");
                        return;
                    }

                    WriteSubnetsByCount(network, SubnetCount.Value);
                    break;

                case "Contains":
                    if (string.IsNullOrEmpty(IPAddress))
                    {
                        ProcessingException = new ArgumentException("IPAddress is required for Contains operations.");
                        return;
                    }

                    WriteContains(network, IPAddress);
                    break;

                case "Overlaps":
                    if (AdditionalNetworks == null || AdditionalNetworks.Length == 0)
                    {
                        ProcessingException = new ArgumentException("AdditionalNetworks is required for Overlaps operations.");
                        return;
                    }

                    WriteOverlaps(network, AdditionalNetworks);
                    break;

                case "Supernet":
                    if (AdditionalNetworks == null || AdditionalNetworks.Length == 0)
                    {
                        ProcessingException = new ArgumentException("AdditionalNetworks is required for Supernet operations.");
                        return;
                    }

                    WriteSupernet(network, AdditionalNetworks);
                    break;

                case "SupernetSummarize":
                    if (AdditionalNetworks == null || AdditionalNetworks.Length == 0)
                    {
                        ProcessingException = new ArgumentException("AdditionalNetworks is required for SupernetSummarize operations.");
                        return;
                    }

                    WriteSupernetSummarize(network, AdditionalNetworks);
                    break;
            }
        }

        private void WriteNetworkInfo(IPNetwork2 network)
        {
            var info = new PSObject();
            info.Properties.Add(new PSNoteProperty("CIDR", network.ToString()));
            info.Properties.Add(new PSNoteProperty("Network", network.Network.ToString()));
            info.Properties.Add(new PSNoteProperty("Netmask", network.Netmask.ToString()));
            info.Properties.Add(new PSNoteProperty("Broadcast", network.Broadcast.ToString()));
            info.Properties.Add(new PSNoteProperty("FirstUsableHost", NetworkUtility.GetFirstUsableHost(network).ToString()));
            info.Properties.Add(new PSNoteProperty("LastUsableHost", NetworkUtility.GetLastUsableHost(network).ToString()));
            info.Properties.Add(new PSNoteProperty("UsableHostCount", NetworkUtility.GetUsableHostCount(network).ToString()));
            info.Properties.Add(new PSNoteProperty("TotalAddressCount", network.Total.ToString()));
            info.Properties.Add(new PSNoteProperty("AddressFamily", network.AddressFamily.ToString()));
            info.Properties.Add(new PSNoteProperty("PrefixLength", network.Cidr));
            info.Properties.Add(new PSNoteProperty("IsPrivate", NetworkUtility.IsPrivate(network.Network)));

            WriteObject(info);
        }

        private void WriteSubnets(IPNetwork2 network, int prefixLength)
        {
            if (prefixLength <= network.Cidr)
            {
                ProcessingException = new ArgumentException($"Prefix length ({prefixLength}) must be greater than the network CIDR ({network.Cidr}).");
                return;
            }

            var subnets = NetworkUtility.Subnet(network, prefixLength);
            WriteObject(subnets, true);
        }

        private void WriteSubnetsByCount(IPNetwork2 network, int subnetCount)
        {
            try
            {
                var subnets = NetworkUtility.SubnetByCount(network, subnetCount);
                WriteObject(subnets, true);
            }
            catch (ArgumentException ex)
            {
                ProcessingException = ex;
            }
        }

        private void WriteContains(IPNetwork2 network, string ipAddress)
        {
            try
            {
                // Check if the input is an IP address or a network
                if (ipAddress.Contains("/"))
                {
                    // It's a network
                    IPNetwork2 otherNetwork = NetworkUtility.ParseCIDR(ipAddress);
                    bool contains = NetworkUtility.Contains(network, otherNetwork);
                    WriteObject(contains);
                }
                else
                {
                    // It's an IP address
                    System.Net.IPAddress ip = System.Net.IPAddress.Parse(ipAddress);
                    bool contains = NetworkUtility.Contains(network, ip);
                    WriteObject(contains);
                }
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
            }
        }

        private void WriteOverlaps(IPNetwork2 network, string[] additionalNetworks)
        {
            try
            {
                var results = new List<PSObject>();

                foreach (var additionalNetwork in additionalNetworks)
                {
                    IPNetwork2 otherNetwork = NetworkUtility.ParseCIDR(additionalNetwork);
                    bool overlaps = NetworkUtility.Overlaps(network, otherNetwork);

                    var result = new PSObject();
                    result.Properties.Add(new PSNoteProperty("Network1", network.ToString()));
                    result.Properties.Add(new PSNoteProperty("Network2", otherNetwork.ToString()));
                    result.Properties.Add(new PSNoteProperty("Overlaps", overlaps));

                    results.Add(result);
                }

                WriteObject(results, true);
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
            }
        }

        private void WriteSupernet(IPNetwork2 network, string[] additionalNetworks)
        {
            try
            {
                var networks = new List<IPNetwork2> { network };

                foreach (var additionalNetwork in additionalNetworks)
                {
                    IPNetwork2 otherNetwork = NetworkUtility.ParseCIDR(additionalNetwork);
                    networks.Add(otherNetwork);
                }

                IPNetwork2 supernet = NetworkUtility.Supernet(networks.ToArray());
                WriteObject(supernet);
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
            }
        }

        private void WriteSupernetSummarize(IPNetwork2 network, string[] additionalNetworks)
        {
            try
            {
                var networks = new List<IPNetwork2> { network };

                foreach (var additionalNetwork in additionalNetworks)
                {
                    IPNetwork2 otherNetwork = NetworkUtility.ParseCIDR(additionalNetwork);
                    networks.Add(otherNetwork);
                }

                List<IPNetwork2> summarizedNetworks = NetworkUtility.SupernetSummarize(networks);
                WriteObject(summarizedNetworks, true);
            }
            catch (Exception ex)
            {
                ProcessingException = ex;
            }
        }
    }
}


