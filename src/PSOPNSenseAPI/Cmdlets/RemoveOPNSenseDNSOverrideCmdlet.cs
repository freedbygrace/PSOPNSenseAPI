using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a DNS override from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseDNSOverride cmdlet removes a DNS override from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a DNS override</para>
    ///     <code>Remove-OPNSenseDNSOverride -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example removes a DNS override by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a DNS override with confirmation</para>
    ///     <code>Remove-OPNSenseDNSOverride -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm</code>
    ///     <para>This example removes a DNS override by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseDNSOverride", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseDNSOverrideCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the DNS override to remove.</para>
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

                // Get the DNS override details for the confirmation message
                var getTask = Task.Run(async () => await dnsService.GetDNSOverrideAsync(Uuid));
                var dnsOverride = getTask.GetAwaiter().GetResult().Host;

                string confirmMessage = $"DNS override {dnsOverride.Hostname}.{dnsOverride.Domain} -> {dnsOverride.IpAddress}";
                if (!string.IsNullOrEmpty(dnsOverride.Description))
                {
                    confirmMessage += $" ({dnsOverride.Description})";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await dnsService.DeleteDNSOverrideAsync(Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"DNS override {Uuid} removed: {deleteResult.Result}");

                // Apply the changes if requested
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
