using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets DHCP servers from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDHCPServer cmdlet retrieves DHCP servers from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all DHCP servers</para>
    ///     <code>Get-OPNSenseDHCPServer</code>
    ///     <para>This example retrieves all DHCP servers from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific DHCP server by interface</para>
    ///     <code>Get-OPNSenseDHCPServer -Interface "lan"</code>
    ///     <para>This example retrieves the DHCP server for the LAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDHCPServer")]
    [OutputType(typeof(PSObject), typeof(DHCPServerDetail))]
    public class GetOPNSenseDHCPServerCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface to get the DHCP server for.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByInterface")]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var dhcpService = new DHCPService(ApiClient, Logger);

                if (ParameterSetName == "ByInterface")
                {
                    var task = Task.Run(async () => await dhcpService.GetServerAsync(Interface));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Server);
                }
                else
                {
                    var task = Task.Run(async () => await dhcpService.GetServersAsync());
                    var result = task.GetAwaiter().GetResult();

                    foreach (var kvp in result.Servers.Interfaces)
                    {
                        var server = new PSObject();
                        server.Properties.Add(new PSNoteProperty("Interface", kvp.Key));
                        server.Properties.Add(new PSNoteProperty("Enabled", kvp.Value.Enabled == "1"));
                        server.Properties.Add(new PSNoteProperty("RangeFrom", kvp.Value.RangeFrom));
                        server.Properties.Add(new PSNoteProperty("RangeTo", kvp.Value.RangeTo));
                        server.Properties.Add(new PSNoteProperty("DefaultLeaseTime", kvp.Value.DefaultLeaseTime));
                        server.Properties.Add(new PSNoteProperty("MaxLeaseTime", kvp.Value.MaxLeaseTime));
                        server.Properties.Add(new PSNoteProperty("Domain", kvp.Value.Domain));
                        server.Properties.Add(new PSNoteProperty("DnsServers", kvp.Value.DnsServers));
                        server.Properties.Add(new PSNoteProperty("Gateway", kvp.Value.Gateway));
                        
                        WriteObject(server);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
