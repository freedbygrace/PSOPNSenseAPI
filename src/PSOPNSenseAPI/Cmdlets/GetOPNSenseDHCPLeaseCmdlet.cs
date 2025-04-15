using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets DHCP leases from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDHCPLease cmdlet retrieves DHCP leases from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all DHCP leases</para>
    ///     <code>Get-OPNSenseDHCPLease</code>
    ///     <para>This example retrieves all DHCP leases from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get active DHCP leases</para>
    ///     <code>Get-OPNSenseDHCPLease | Where-Object { $_.State -eq "active" }</code>
    ///     <para>This example retrieves all active DHCP leases from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDHCPLease")]
    [OutputType(typeof(DHCPLease))]
    public class GetOPNSenseDHCPLeaseCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var dhcpService = new DHCPService(ApiClient, Logger);

                var task = Task.Run(async () => await dhcpService.GetLeasesAsync());
                var result = task.GetAwaiter().GetResult();

                WriteObject(result.Rows, true);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
