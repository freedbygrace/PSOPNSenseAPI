using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a DHCP static mapping from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseDHCPStaticMapping cmdlet removes a DHCP static mapping from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a static mapping</para>
    ///     <code>Remove-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example removes a static mapping by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a static mapping with confirmation</para>
    ///     <code>Remove-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm</code>
    ///     <para>This example removes a static mapping by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseDHCPStaticMapping", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseDHCPStaticMappingCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface of the static mapping to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The UUID of the static mapping to remove.</para>
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

                // Get the mapping details for the confirmation message
                var getTask = Task.Run(async () => await dhcpService.GetStaticMappingAsync(Interface, Uuid));
                var mapping = getTask.GetAwaiter().GetResult().Mapping;

                string confirmMessage = $"Static mapping: {mapping.MacAddress} -> {mapping.IpAddress}";
                if (!string.IsNullOrEmpty(mapping.Hostname))
                {
                    confirmMessage += $" ({mapping.Hostname})";
                }

                if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
                {
                    return;
                }

                var deleteTask = Task.Run(async () => await dhcpService.DeleteStaticMappingAsync(Interface, Uuid));
                var deleteResult = deleteTask.GetAwaiter().GetResult();

                WriteVerbose($"Static mapping {Uuid} removed: {deleteResult.Result}");

                // Apply the changes if requested
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
