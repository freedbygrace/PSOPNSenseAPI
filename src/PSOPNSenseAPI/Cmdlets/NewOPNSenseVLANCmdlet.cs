using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new VLAN on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseVLAN cmdlet creates a new VLAN on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new VLAN</para>
    ///     <code>New-OPNSenseVLAN -Interface "em0" -Tag 10 -Description "Management VLAN"</code>
    ///     <para>This example creates a new VLAN with tag 10 on interface em0.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseVLAN")]
    [OutputType(typeof(string))]
    public class NewOPNSenseVLANCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The parent interface for the VLAN.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The VLAN tag (1-4094).</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateRange(1, 4094)]
        public int Tag { get; set; }

        /// <summary>
        /// <para type="description">The priority of the VLAN (0-7).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(0, 7)]
        public int Priority { get; set; } = 0;

        /// <summary>
        /// <para type="description">The description of the VLAN.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var interfaceService = new InterfaceService(ApiClient, Logger);

            var vlan = new VLANConfig
            {
                Interface = Interface,
                Tag = Tag.ToString(),
                Priority = Priority.ToString(),
                Description = Description
            };

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => interfaceService.CreateVLANAsync(vlan));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Created VLAN with UUID {result.Uuid}");
            WriteObject(result.Uuid);
        }
    }
}
