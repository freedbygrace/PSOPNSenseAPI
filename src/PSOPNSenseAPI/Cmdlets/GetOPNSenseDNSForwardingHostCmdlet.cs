using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets DNS forwarding hosts from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDNSForwardingHost cmdlet retrieves DNS forwarding hosts from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all DNS forwarding hosts</para>
    ///     <code>Get-OPNSenseDNSForwardingHost</code>
    ///     <para>This example retrieves all DNS forwarding hosts from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific DNS forwarding host by UUID</para>
    ///     <code>Get-OPNSenseDNSForwardingHost -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific DNS forwarding host by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDNSForwardingHost")]
    [OutputType(typeof(DNSForwardingHost), typeof(DNSForwardingHostDetail))]
    public class GetOPNSenseDNSForwardingHostCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the DNS forwarding host to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var dnsService = new DNSService(ApiClient, Logger);

            if (ParameterSetName == "ByUuid")
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => dnsService.GetDNSForwardingHostAsync(Uuid));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Host);
            }
            else
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => dnsService.GetDNSForwardingHostsAsync());

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
