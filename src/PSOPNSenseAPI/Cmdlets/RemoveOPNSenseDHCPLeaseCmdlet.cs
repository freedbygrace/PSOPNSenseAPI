using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a DHCP lease from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseDHCPLease cmdlet removes a DHCP lease from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a DHCP lease</para>
    ///     <code>Remove-OPNSenseDHCPLease -MacAddress "00:11:22:33:44:55" -Apply</code>
    ///     <para>This example removes a DHCP lease by its MAC address.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a DHCP lease with confirmation</para>
    ///     <code>Remove-OPNSenseDHCPLease -MacAddress "00:11:22:33:44:55" -Confirm -Apply</code>
    ///     <para>This example removes a DHCP lease by its MAC address after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseDHCPLease", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseDHCPLeaseCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The MAC address of the DHCP lease to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string MacAddress { get; set; }

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
                var dhcpService = new DHCPService(ApiClient, Logger);

                // Get the lease details for the confirmation message
                var getTask = Task.Run(async () => await dhcpService.GetLeaseByMacAsync(MacAddress));
                var lease = getTask.GetAwaiter().GetResult().Lease;

                string confirmMessage = $"DHCP lease: {lease.MacAddress} -> {lease.IpAddress}";
                if (!string.IsNullOrEmpty(lease.Hostname))
                {
                    confirmMessage += $" ({lease.Hostname})";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await dhcpService.DeleteLeaseAsync(MacAddress));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"DHCP lease {MacAddress} removed: {deleteResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await dhcpService.ApplyChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"DHCP changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
