using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates VLANs for subnets on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseSubnetVLANs cmdlet divides a CIDR network into subnets and creates VLANs for each subnet on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create VLANs for subnets with sequential VLAN IDs</para>
    ///     <code>New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "192.168.0.0/24" -SubnetMaskBits 27 -StartingVlanId 10 -VlanIdIncrement 1</code>
    ///     <para>This example divides the 192.168.0.0/24 network into /27 subnets and creates VLANs with IDs 10, 11, 12, etc.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Create VLANs for subnets with VLAN IDs in multiples of 10</para>
    ///     <code>New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "10.0.0.0/16" -SubnetMaskBits 24 -StartingVlanId 10 -VlanIdIncrement 10</code>
    ///     <para>This example divides the 10.0.0.0/16 network into /24 subnets and creates VLANs with IDs 10, 20, 30, etc.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Create VLANs for subnets with DHCP enabled</para>
    ///     <code>New-OPNSenseSubnetVLANs -ParentInterface "em0" -Network "172.16.0.0/20" -SubnetMaskBits 24 -StartingVlanId 100 -VlanIdIncrement 1 -EnableDHCP</code>
    ///     <para>This example divides the 172.16.0.0/20 network into /24 subnets, creates VLANs with IDs 100, 101, 102, etc., and enables DHCP on each VLAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseSubnetVLANs", SupportsShouldProcess = true)]
    [OutputType(typeof(PSObject))]
    public class NewOPNSenseSubnetVLANsCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The parent interface for the VLANs.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string ParentInterface { get; set; }

        /// <summary>
        /// <para type="description">The network in CIDR notation to divide into subnets.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Network { get; set; }

        /// <summary>
        /// <para type="description">The subnet mask bits for the subnets.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateRange(1, 32)]
        public int SubnetMaskBits { get; set; }

        /// <summary>
        /// <para type="description">The starting VLAN ID.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        [ValidateRange(1, 4094)]
        public int StartingVlanId { get; set; }

        /// <summary>
        /// <para type="description">The increment for VLAN IDs.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 4)]
        [ValidateRange(1, 4094)]
        public int VlanIdIncrement { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable DHCP on the VLAN interfaces.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter EnableDHCP { get; set; }

        /// <summary>
        /// <para type="description">The domain name for DHCP clients.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Domain { get; set; } = "local";

        /// <summary>
        /// <para type="description">The DNS servers for DHCP clients. If not specified, the gateway IP will be used.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] DnsServers { get; set; }

        /// <summary>
        /// <para type="description">The description prefix for the VLANs.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string DescriptionPrefix { get; set; } = "VLAN";

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            // Parse the network CIDR
            var ipNetwork = IPNetwork2.Parse(Network);

            // Validate subnet mask bits
            if (SubnetMaskBits <= ipNetwork.Cidr)
            {
                ProcessingException = new ArgumentException($"Subnet mask bits ({SubnetMaskBits}) must be greater than the network CIDR ({ipNetwork.Cidr}).");
                WriteWarning($"Subnet mask bits ({SubnetMaskBits}) must be greater than the network CIDR ({ipNetwork.Cidr}).");
                return;
            }

            // Calculate the number of subnets
            int numSubnets = (int)Math.Pow(2, SubnetMaskBits - ipNetwork.Cidr);

            // Calculate the maximum VLAN ID
            int maxVlanId = StartingVlanId + (numSubnets - 1) * VlanIdIncrement;

            // Validate the maximum VLAN ID
            if (maxVlanId > 4094)
            {
                ProcessingException = new ArgumentException($"The maximum VLAN ID ({maxVlanId}) exceeds the maximum allowed value (4094).");
                WriteWarning($"The maximum VLAN ID ({maxVlanId}) exceeds the maximum allowed value (4094).");
                return;
            }

            // Get confirmation
            if (!Force.IsPresent && !ShouldProcess($"Create {numSubnets} VLANs for subnets of {Network} with VLAN IDs {StartingVlanId}-{maxVlanId}", "New-OPNSenseSubnetVLANs"))
            {
                return;
            }

            // Get the interface service
            var interfaceService = new InterfaceService(ApiClient, Logger);

            // Verify the parent interface exists
            var interfaceDetail = ExecuteAsyncTask(() => interfaceService.GetInterfaceDetailAsync(ParentInterface));

            // Only continue if no exception occurred
            if (ProcessingException != null)
            {
                return;
            }

            // Get existing VLANs
            var vlansResult = ExecuteAsyncTask(() => interfaceService.GetVLANsAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || vlansResult == null)
            {
                return;
            }

            var existingVlans = vlansResult.Rows;

            // Create a list to store the created VLANs
            var createdVlans = new List<PSObject>();

            // Get DHCP service if needed
            DHCPService dhcpService = null;
            if (EnableDHCP.IsPresent)
            {
                dhcpService = new DHCPService(ApiClient, Logger);
            }

            // Divide the network into subnets
            var subnets = ipNetwork.Subnet((byte)SubnetMaskBits);
            int vlanId = StartingVlanId;
            int subnetIndex = 0;

            foreach (var subnet in subnets)
            {
                // Calculate the gateway IP (first host address)
                var gatewayIp = GetFirstHostAddress(subnet);

                // Calculate the DHCP range (second host address to last host address)
                var dhcpStart = GetSecondHostAddress(subnet);
                var dhcpEnd = GetLastHostAddress(subnet);

                // Create a description for the VLAN
                string description = $"{DescriptionPrefix} {vlanId} - {subnet}";

                // Check if the VLAN already exists
                var existingVlan = existingVlans.FirstOrDefault(v =>
                    v.Interface == ParentInterface &&
                    int.Parse(v.Tag) == vlanId);

                if (existingVlan != null)
                {
                    WriteVerbose($"VLAN {vlanId} already exists on interface {ParentInterface}");

                    // Add the existing VLAN to the list
                    var vlanInfo = new PSObject();
                    vlanInfo.Properties.Add(new PSNoteProperty("VlanId", vlanId));
                    vlanInfo.Properties.Add(new PSNoteProperty("Subnet", subnet.ToString()));
                    vlanInfo.Properties.Add(new PSNoteProperty("Gateway", gatewayIp.ToString()));
                    vlanInfo.Properties.Add(new PSNoteProperty("Uuid", existingVlan.Uuid));
                    vlanInfo.Properties.Add(new PSNoteProperty("Status", "Existing"));

                    createdVlans.Add(vlanInfo);
                }
                else
                {
                    // Create the VLAN
                    var vlanConfig = new VLANConfig
                    {
                        Interface = ParentInterface,
                        Tag = vlanId.ToString(),
                        Priority = "0",
                        Description = description
                    };

                    var createVlanResult = ExecuteAsyncTask(() => interfaceService.CreateVLANAsync(vlanConfig));

                    // Only continue if no exception occurred
                    if (ProcessingException != null || createVlanResult == null)
                    {
                        return;
                    }

                    WriteVerbose($"Created VLAN {vlanId} on interface {ParentInterface} with UUID {createVlanResult.Uuid}");

                    // Configure the VLAN interface
                    string vlanInterfaceName = $"{ParentInterface}.{vlanId}";

                    var interfaceConfig = new InterfaceConfig
                    {
                        Description = description,
                        IpAddress = gatewayIp.ToString(),
                        SubnetMask = SubnetMaskBits.ToString(),
                        Enabled = "1"
                    };

                    var updateInterfaceResult = ExecuteAsyncTask(() => interfaceService.UpdateInterfaceAsync(vlanInterfaceName, interfaceConfig));

                    // Only continue if no exception occurred
                    if (ProcessingException != null)
                    {
                        return;
                    }

                    WriteVerbose($"Configured interface {vlanInterfaceName} with IP {gatewayIp}/{SubnetMaskBits}");

                    // If EnableDHCP is specified, configure DHCP for the interface
                    if (EnableDHCP.IsPresent && dhcpService != null)
                    {
                        // Configure DHCP server for the interface
                        var dhcpConfig = new DHCPServerConfig
                        {
                            Enabled = "1",
                            RangeFrom = dhcpStart.ToString(),
                            RangeTo = dhcpEnd.ToString(),
                            DefaultLeaseTime = "7200",
                            MaxLeaseTime = "86400",
                            Domain = Domain,
                            Gateway = gatewayIp.ToString(),
                            DnsServers = DnsServers != null ? new List<string>(DnsServers) : new List<string> { gatewayIp.ToString() }
                        };

                        var updateDhcpResult = ExecuteAsyncTask(() => dhcpService.UpdateServerAsync(vlanInterfaceName, dhcpConfig));

                        // Only continue if no exception occurred
                        if (ProcessingException != null)
                        {
                            return;
                        }

                        WriteVerbose($"Configured DHCP server for interface {vlanInterfaceName} with range {dhcpStart} - {dhcpEnd}");
                    }

                    // Add the created VLAN to the list
                    var vlanInfo = new PSObject();
                    vlanInfo.Properties.Add(new PSNoteProperty("VlanId", vlanId));
                    vlanInfo.Properties.Add(new PSNoteProperty("Subnet", subnet.ToString()));
                    vlanInfo.Properties.Add(new PSNoteProperty("Gateway", gatewayIp.ToString()));
                    vlanInfo.Properties.Add(new PSNoteProperty("Uuid", createVlanResult.Uuid));
                    vlanInfo.Properties.Add(new PSNoteProperty("Status", "Created"));
                    if (EnableDHCP.IsPresent)
                    {
                        vlanInfo.Properties.Add(new PSNoteProperty("DHCPRange", $"{dhcpStart} - {dhcpEnd}"));
                    }

                    createdVlans.Add(vlanInfo);
                }

                // Increment the VLAN ID
                vlanId += VlanIdIncrement;
                subnetIndex++;
            }

            // Apply DHCP changes if needed
            if (EnableDHCP.IsPresent && dhcpService != null)
            {
                var applyDhcpResult = ExecuteAsyncTask(() => dhcpService.ApplyChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyDhcpResult == null)
                {
                    return;
                }

                WriteVerbose($"Applied DHCP changes: {applyDhcpResult.Status}");
            }

            // Create a result object
            var result = new PSObject();
            result.Properties.Add(new PSNoteProperty("ParentInterface", ParentInterface));
            result.Properties.Add(new PSNoteProperty("Network", Network));
            result.Properties.Add(new PSNoteProperty("SubnetMaskBits", SubnetMaskBits));
            result.Properties.Add(new PSNoteProperty("VLANs", createdVlans));

            WriteObject(result);
        }

        /// <summary>
        /// Gets the first host address in a subnet
        /// </summary>
        /// <param name="subnet">The subnet</param>
        /// <returns>The first host address</returns>
        private static IPAddress GetFirstHostAddress(IPNetwork2 subnet)
        {
            byte[] addressBytes = subnet.Network.GetAddressBytes();

            // If the subnet is too small (e.g., /31 or /32), return the network address
            if (subnet.Cidr >= 31)
                return subnet.Network;

            // Increment the last byte of the network address by 1
            addressBytes[addressBytes.Length - 1]++;

            return new IPAddress(addressBytes);
        }

        /// <summary>
        /// Gets the second host address in a subnet
        /// </summary>
        /// <param name="subnet">The subnet</param>
        /// <returns>The second host address</returns>
        private static IPAddress GetSecondHostAddress(IPNetwork2 subnet)
        {
            byte[] addressBytes = subnet.Network.GetAddressBytes();

            // If the subnet is too small (e.g., /31 or /32), return the network address
            if (subnet.Cidr >= 31)
                return subnet.Network;

            // If the subnet is /30, return the broadcast address - 1
            if (subnet.Cidr == 30)
                return subnet.Broadcast;

            // Increment the last byte of the network address by 2
            addressBytes[addressBytes.Length - 1] += 2;

            return new IPAddress(addressBytes);
        }

        /// <summary>
        /// Gets the last host address in a subnet
        /// </summary>
        /// <param name="subnet">The subnet</param>
        /// <returns>The last host address</returns>
        private static IPAddress GetLastHostAddress(IPNetwork2 subnet)
        {
            byte[] broadcastBytes = subnet.Broadcast.GetAddressBytes();

            // If the subnet is too small (e.g., /31 or /32), return the broadcast address
            if (subnet.Cidr >= 31)
                return subnet.Broadcast;

            // Decrement the last byte of the broadcast address by 1
            broadcastBytes[broadcastBytes.Length - 1]--;

            return new IPAddress(broadcastBytes);
        }
    }
}
