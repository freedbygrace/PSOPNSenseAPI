using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets VLANs from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseVLAN cmdlet retrieves VLANs from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all VLANs</para>
    ///     <code>Get-OPNSenseVLAN</code>
    ///     <para>This example retrieves all VLANs from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific VLAN by UUID</para>
    ///     <code>Get-OPNSenseVLAN -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific VLAN by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseVLAN")]
    [OutputType(typeof(VLAN), typeof(VLANDetail))]
    public class GetOPNSenseVLANCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the VLAN to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var interfaceService = new InterfaceService(ApiClient, Logger);

            if (ParameterSetName == "ByUuid")
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => interfaceService.GetVLANAsync(Uuid));

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Vlan);
            }
            else
            {
                // Use our safe execution method
                var result = ExecuteAsyncTask(() => interfaceService.GetVLANsAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || result == null)
                {
                    return;
                }

                WriteObject(result.Rows, true);
            }
        }
    }
}
