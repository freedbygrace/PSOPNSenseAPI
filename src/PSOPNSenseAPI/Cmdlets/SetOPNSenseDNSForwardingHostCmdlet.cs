using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a DNS forwarding host on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseDNSForwardingHost cmdlet updates a DNS forwarding host on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a DNS forwarding host's server</para>
    ///     <code>Set-OPNSenseDNSForwardingHost -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Server "192.168.1.20" -Apply</code>
    ///     <para>This example updates the server of a DNS forwarding host.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Disable a DNS forwarding host</para>
    ///     <code>Set-OPNSenseDNSForwardingHost -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Disabled -Apply</code>
    ///     <para>This example disables a DNS forwarding host.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseDNSForwardingHost")]
    [OutputType(typeof(void))]
    public class SetOPNSenseDNSForwardingHostCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the DNS forwarding host to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The domain to forward.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Domain { get; set; }

        /// <summary>
        /// <para type="description">The DNS server to forward to.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
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
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the forwarding host is disabled.</para>
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
            var dnsService = new DNSService(ApiClient, Logger);

            // Get current host
            var getResult = ExecuteAsyncTask(() => dnsService.GetDNSForwardingHostAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var currentHost = getResult.Host;

            // Create updated host
            var host = new DNSForwardingHostConfig
            {
                Domain = Domain ?? currentHost.Domain,
                Server = Server ?? currentHost.Server,
                Description = Description ?? currentHost.Description
            };

            // Handle enabled/disabled state
            if (Enabled.IsPresent && Disabled.IsPresent)
            {
                WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                host.Enabled = "1";
            }
            else if (Enabled.IsPresent)
            {
                host.Enabled = "1";
            }
            else if (Disabled.IsPresent)
            {
                host.Enabled = "0";
            }
            else
            {
                host.Enabled = currentHost.Enabled;
            }

            // Update host
            var updateResult = ExecuteAsyncTask(() => dnsService.UpdateDNSForwardingHostAsync(Uuid, host));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"DNS forwarding host {Uuid} updated: {updateResult.Result}");

            // Apply changes if requested
            if (Apply.IsPresent)
            {
                var applyResult = ExecuteAsyncTask(() => dnsService.ApplyDNSChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"DNS changes applied: {applyResult.Status}");
            }
        }
    }
}
