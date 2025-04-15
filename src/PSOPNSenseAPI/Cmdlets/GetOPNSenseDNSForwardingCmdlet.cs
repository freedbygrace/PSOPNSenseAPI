using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets the DNS forwarding configuration from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDNSForwarding cmdlet retrieves the DNS forwarding configuration from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get DNS forwarding configuration</para>
    ///     <code>Get-OPNSenseDNSForwarding</code>
    ///     <para>This example retrieves the DNS forwarding configuration from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDNSForwarding")]
    [OutputType(typeof(DNSForwardingConfig))]
    public class GetOPNSenseDNSForwardingCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var dnsService = new DNSService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => dnsService.GetDNSForwardingAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteObject(result.Forward);
        }
    }
}
