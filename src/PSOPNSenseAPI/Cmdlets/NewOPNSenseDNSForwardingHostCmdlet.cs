using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new DNS forwarding host on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseDNSForwardingHost cmdlet creates a new DNS forwarding host on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new DNS forwarding host</para>
    ///     <code>New-OPNSenseDNSForwardingHost -Domain "example.com" -Server "192.168.1.10" -Description "Internal DNS Server" -Apply</code>
    ///     <para>This example creates a new DNS forwarding host for example.com that forwards to 192.168.1.10.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseDNSForwardingHost")]
    [OutputType(typeof(string))]
    public class NewOPNSenseDNSForwardingHostCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The domain to forward.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">The DNS server to forward to.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Server { get; set; }

        /// <summary>
        /// <para type="description">The description of the forwarding host.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Whether the forwarding host is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; } = true;

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
            var dnsService = new DNSService(ApiClient, Logger);

            var host = new DNSForwardingHostConfig
            {
                Domain = Domain,
                Server = Server,
                Description = Description,
                Enabled = Enabled.IsPresent ? "1" : "0"
            };

            // Use our safe execution method
            var createResult = ExecuteAsyncTask(() => dnsService.CreateDNSForwardingHostAsync(host));

            // Only continue if no exception occurred
            if (ProcessingException != null || createResult == null)
            {
                return;
            }

            WriteVerbose($"Created DNS forwarding host with UUID {createResult.Uuid}");

            // Apply changes if requested
            if (Apply.IsPresent)
            {
                // Use our safe execution method
                var applyResult = ExecuteAsyncTask(() => dnsService.ApplyDNSChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"DNS changes applied: {applyResult.Status}");
            }

            WriteObject(createResult.Uuid);
        }
    }
}
