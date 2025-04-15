using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Removes a VLAN from an OPNSense firewall.</para>
    /// <para type="description">The Remove-OPNSenseVLAN cmdlet removes a VLAN from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Remove a VLAN</para>
    ///     <code>Remove-OPNSenseVLAN -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example removes a VLAN by its UUID.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Remove a VLAN with confirmation</para>
    ///     <code>Remove-OPNSenseVLAN -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Confirm</code>
    ///     <para>This example removes a VLAN by its UUID after confirmation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "OPNSenseVLAN", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RemoveOPNSenseVLANCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the VLAN to remove.</para>
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
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var interfaceService = new InterfaceService(ApiClient, Logger);

            // Get the VLAN details for the confirmation message
            var getResult = ExecuteAsyncTask(() => interfaceService.GetVLANAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var vlan = getResult.Vlan;

            string confirmMessage = $"VLAN {vlan.Tag} on interface {vlan.Interface}";
            if (!string.IsNullOrEmpty(vlan.Description))
            {
                confirmMessage += $" ({vlan.Description})";
            }

            if (!Force.IsPresent && !ShouldProcess(confirmMessage, "Remove"))
            {
                return;
            }

            var deleteResult = ExecuteAsyncTask(() => interfaceService.DeleteVLANAsync(Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || deleteResult == null)
            {
                return;
            }

            WriteVerbose($"VLAN {Uuid} removed: {deleteResult.Result}");
        }
    }
}
