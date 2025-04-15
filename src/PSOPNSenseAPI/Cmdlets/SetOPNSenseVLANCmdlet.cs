using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a VLAN on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseVLAN cmdlet updates a VLAN on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a VLAN description</para>
    ///     <code>Set-OPNSenseVLAN -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Description "Updated VLAN Description"</code>
    ///     <para>This example updates the description of a VLAN.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update a VLAN tag and priority</para>
    ///     <code>Set-OPNSenseVLAN -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Tag 20 -Priority 3</code>
    ///     <para>This example updates the tag and priority of a VLAN.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseVLAN")]
    [OutputType(typeof(void))]
    public class SetOPNSenseVLANCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The UUID of the VLAN to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The parent interface for the VLAN.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The VLAN tag (1-4094).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 4094)]
        public int? Tag { get; set; }

        /// <summary>
        /// <para type="description">The priority of the VLAN (0-7).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(0, 7)]
        public int? Priority { get; set; }

        /// <summary>
        /// <para type="description">The description of the VLAN.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var interfaceService = new InterfaceService(ApiClient, Logger);

                // First, get the current VLAN configuration
                var getTask = Task.Run(async () => await interfaceService.GetVLANAsync(Uuid));
                var currentVlan = getTask.GetAwaiter().GetResult().Vlan;

                // Create the updated configuration
                var vlan = new VLANConfig
                {
                    Interface = Interface ?? currentVlan.Interface,
                    Tag = Tag?.ToString() ?? currentVlan.Tag,
                    Priority = Priority?.ToString() ?? currentVlan.Priority,
                    Description = Description ?? currentVlan.Description
                };

                // Update the VLAN
                var updateTask = Task.Run(async () => await interfaceService.UpdateVLANAsync(Uuid, vlan));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"VLAN {Uuid} updated: {updateResult.Result}");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
