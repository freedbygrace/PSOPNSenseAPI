using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a DHCP option from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseDHCPOption cmdlet removes a DHCP option from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a DHCP option</para>
    ///     <code>Remove-OPNSenseDHCPOption -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Apply</code>
    ///     <para>This example removes a DHCP option by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a DHCP option with confirmation</para>
    ///     <code>Remove-OPNSenseDHCPOption -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm -Apply</code>
    ///     <para>This example removes a DHCP option by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseDHCPOption", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseDHCPOptionCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface name.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The UUID of the DHCP option to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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
                var dhcpService = new DHCPService(ApiClient, Logger);

                // Get the option details for the confirmation message
                var getTask = Task.Run(async () => await dhcpService.GetOptionAsync(Interface, Uuid));
                var option = getTask.GetAwaiter().GetResult().Option;

                string confirmMessage = $"DHCP option: {option.Number} = {option.Value}";
                if (!string.IsNullOrEmpty(option.Description))
                {
                    confirmMessage += $" ({option.Description})";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await dhcpService.DeleteOptionAsync(Interface, Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"DHCP option {Uuid} removed: {deleteResult.Result}");

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
