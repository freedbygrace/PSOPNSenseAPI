using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets DHCP options from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDHCPOption cmdlet retrieves DHCP options from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all DHCP options for an interface</para>
    ///     <code>Get-OPNSenseDHCPOption -Interface "lan"</code>
    ///     <para>This example retrieves all DHCP options for the LAN interface.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific DHCP option by UUID</para>
    ///     <code>Get-OPNSenseDHCPOption -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific DHCP option by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDHCPOption")]
    [OutputType(typeof(DHCPOption), typeof(DHCPOptionDetail))]
    public class GetOPNSenseDHCPOptionCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface name.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The UUID of the DHCP option to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var dhcpService = new DHCPService(ApiClient, Logger);

                if (ParameterSetName == "ByUuid")
                {
                    var task = Task.Run(async () => await dhcpService.GetOptionAsync(Interface, Uuid));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Option);
                }
                else
                {
                    var task = Task.Run(async () => await dhcpService.GetOptionsAsync(Interface));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Rows, true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
