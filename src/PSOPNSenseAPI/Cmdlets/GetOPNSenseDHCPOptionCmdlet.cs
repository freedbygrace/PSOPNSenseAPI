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
        protected override void ProcessRecordInternal()
        {
            var dhcpService = new DHCPService(ApiClient, Logger);

            if (ParameterSetName == "ByUuid")
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => dhcpService.GetOptionAsync(Interface, Uuid));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Option);
            }
            else
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => dhcpService.GetOptionsAsync(Interface));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Rows, true);
            }
        }
    }
}
