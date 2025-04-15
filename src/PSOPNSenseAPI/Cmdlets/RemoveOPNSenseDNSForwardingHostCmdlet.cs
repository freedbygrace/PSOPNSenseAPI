using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a DNS forwarding host from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseDNSForwardingHost cmdlet removes a DNS forwarding host from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a DNS forwarding host</para>
    ///     <code>Remove-OPNSenseDNSForwardingHost -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Apply</code>
    ///     <para>This example removes a DNS forwarding host by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a DNS forwarding host with confirmation</para>
    ///     <code>Remove-OPNSenseDNSForwardingHost -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm -Apply</code>
    ///     <para>This example removes a DNS forwarding host by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseDNSForwardingHost", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseDNSForwardingHostCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the DNS forwarding host to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">Whether to apply the changes immediately.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Apply { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var dnsService = new DNSService(ApiClient, Logger);

                // Get the host details for the confirmation message
                var getTask = Task.Run(async () => await dnsService.GetDNSForwardingHostAsync(Uuid));
                var host = getTask.GetAwaiter().GetResult().Host;

                string confirmMessage = $"DNS forwarding host: {host.Domain} -> {host.Server}";
                if (!string.IsNullOrEmpty(host.Description))
                {
                    confirmMessage += $" ({host.Description})";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await dnsService.DeleteDNSForwardingHostAsync(Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"DNS forwarding host {Uuid} removed: {deleteResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await dnsService.ApplyDNSChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"DNS changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
