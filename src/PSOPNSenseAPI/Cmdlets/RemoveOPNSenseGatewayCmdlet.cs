using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a gateway from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseGateway cmdlet removes a gateway from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a gateway</para>
    ///     <code>Remove-OPNSenseGateway -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Apply</code>
    ///     <para>This example removes a gateway by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a gateway with confirmation</para>
    ///     <code>Remove-OPNSenseGateway -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm -Apply</code>
    ///     <para>This example removes a gateway by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseGateway", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseGatewayCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the gateway to remove.</para>
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
                var gatewayService = new GatewayService(ApiClient, Logger);

                // Get the gateway details for the confirmation message
                var getTask = Task.Run(async () => await gatewayService.GetGatewayAsync(Uuid));
                var gateway = getTask.GetAwaiter().GetResult().Gateway;

                string confirmMessage = $"Gateway: {gateway.Name} ({gateway.IpAddress})";
                if (!string.IsNullOrEmpty(gateway.Description))
                {
                    confirmMessage += $" - {gateway.Description}";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await gatewayService.DeleteGatewayAsync(Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"Gateway {Uuid} removed: {deleteResult.Result}");

                // Apply changes if requested
                if (Apply.IsPresent)
                {
                    var applyTask = Task.Run(async () => await gatewayService.ApplyGatewayChangesAsync());
                    var applyResult = applyTask.GetAwaiter().GetResult();

                    WriteVerbose($"Gateway changes applied: {applyResult.Status}");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
