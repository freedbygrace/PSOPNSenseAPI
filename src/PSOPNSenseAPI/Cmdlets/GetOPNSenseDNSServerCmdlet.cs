using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets DNS server configuration from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDNSServer cmdlet retrieves DNS server configuration from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get DNS server configuration</para>
    ///     <code>Get-OPNSenseDNSServer</code>
    ///     <para>This example retrieves the DNS server configuration from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDNSServer")]
    [OutputType(typeof(PSObject))]
    public class GetOPNSenseDNSServerCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var dnsService = new DNSService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => dnsService.GetDNSConfigAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            var dnsConfig = new PSObject();
            dnsConfig.Properties.Add(new PSNoteProperty("Enabled", result.Unbound.Enabled == "1"));
            dnsConfig.Properties.Add(new PSNoteProperty("Port", result.Unbound.Port));
            dnsConfig.Properties.Add(new PSNoteProperty("Interfaces", result.Unbound.Interfaces));
            dnsConfig.Properties.Add(new PSNoteProperty("Forwarding", result.Unbound.Forwarding == "1"));
            dnsConfig.Properties.Add(new PSNoteProperty("Forwarders", result.Unbound.Forwarders));
            dnsConfig.Properties.Add(new PSNoteProperty("RegisterDhcp", result.Unbound.RegisterDhcp == "1"));
            dnsConfig.Properties.Add(new PSNoteProperty("RegisterDhcpDomain", result.Unbound.RegisterDhcpDomain));
            dnsConfig.Properties.Add(new PSNoteProperty("RegisterDhcpStatic", result.Unbound.RegisterDhcpStatic == "1"));
            dnsConfig.Properties.Add(new PSNoteProperty("ActiveInterfaces", result.Unbound.ActiveInterfaces));

            WriteObject(dnsConfig);
        }
    }
}
