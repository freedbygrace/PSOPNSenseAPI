using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a DHCP server on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseDHCPServer cmdlet updates a DHCP server on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a DHCP server's range</para>
    ///     <code>Set-OPNSenseDHCPServer -Interface "lan" -RangeFrom "192.168.1.100" -RangeTo "192.168.1.200"</code>
    ///     <para>This example updates the DHCP range for the LAN interface.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update a DHCP server's DNS servers</para>
    ///     <code>Set-OPNSenseDHCPServer -Interface "lan" -DnsServers "8.8.8.8","8.8.4.4" -Domain "example.com"</code>
    ///     <para>This example updates the DNS servers and domain for the LAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseDHCPServer")]
    [OutputType(typeof(void))]
    public class SetOPNSenseDHCPServerCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface to update the DHCP server for.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The start of the DHCP range.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string RangeFrom { get; set; }

        /// <summary>
        /// <para type="description">The end of the DHCP range.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string RangeTo { get; set; }

        /// <summary>
        /// <para type="description">The default lease time in seconds.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public int? DefaultLeaseTime { get; set; }

        /// <summary>
        /// <para type="description">The maximum lease time in seconds.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public int? MaxLeaseTime { get; set; }

        /// <summary>
        /// <para type="description">The domain name for DHCP clients.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">The DNS servers for DHCP clients.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] DnsServers { get; set; }

        /// <summary>
        /// <para type="description">The gateway for DHCP clients.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Gateway { get; set; }

        /// <summary>
        /// <para type="description">Whether the DHCP server is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the DHCP server is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

        /// <summary>
        /// <para type="description">Whether to apply the changes immediately.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Apply { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var dhcpService = new DHCPService(ApiClient, Logger);

            // First, get the current server configuration
            var getResult = ExecuteAsyncTask(() => dhcpService.GetServerAsync(Interface));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var currentServer = getResult.Server;

            // Create the updated configuration
            var server = new DHCPServerConfig
            {
                RangeFrom = RangeFrom ?? currentServer.RangeFrom,
                RangeTo = RangeTo ?? currentServer.RangeTo,
                DefaultLeaseTime = DefaultLeaseTime?.ToString() ?? currentServer.DefaultLeaseTime,
                MaxLeaseTime = MaxLeaseTime?.ToString() ?? currentServer.MaxLeaseTime,
                Domain = Domain ?? currentServer.Domain,
                DnsServers = DnsServers != null ? new List<string>(DnsServers) : currentServer.DnsServers,
                Gateway = Gateway ?? currentServer.Gateway
            };

            // Handle enabled/disabled state
            if (Enabled.IsPresent && Disabled.IsPresent)
            {
                WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                server.Enabled = "1";
            }
            else if (Enabled.IsPresent)
            {
                server.Enabled = "1";
            }
            else if (Disabled.IsPresent)
            {
                server.Enabled = "0";
            }
            else
            {
                server.Enabled = currentServer.Enabled;
            }

            // Update the server
            var updateResult = ExecuteAsyncTask(() => dhcpService.UpdateServerAsync(Interface, server));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"DHCP server for interface {Interface} updated: {updateResult.Result}");

            // Apply the changes if requested
            if (Apply.IsPresent)
            {
                var applyResult = ExecuteAsyncTask(() => dhcpService.ApplyChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"DHCP changes applied: {applyResult.Status}");
            }
        }
    }
}
