using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates an interface on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseInterface cmdlet updates an interface on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update an interface description</para>
    ///     <code>Set-OPNSenseInterface -Name "lan" -Description "Local Area Network"</code>
    ///     <para>This example updates the description of the LAN interface.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update an interface IP address</para>
    ///     <code>Set-OPNSenseInterface -Name "lan" -IpAddress "192.168.1.1" -SubnetMask "24"</code>
    ///     <para>This example updates the IP address and subnet mask of the LAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseInterface")]
    [OutputType(typeof(void))]
    public class SetOPNSenseInterfaceCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the interface to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The description of the interface.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The IP address of the interface.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string IpAddress { get; set; }

        /// <summary>
        /// <para type="description">The subnet mask of the interface.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string SubnetMask { get; set; }

        /// <summary>
        /// <para type="description">Whether the interface is a DHCP client.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Dhcp { get; set; }

        /// <summary>
        /// <para type="description">The gateway of the interface.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Gateway { get; set; }

        /// <summary>
        /// <para type="description">The MTU of the interface.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Mtu { get; set; }

        /// <summary>
        /// <para type="description">The speed/duplex of the interface.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Media { get; set; }

        /// <summary>
        /// <para type="description">Whether the interface is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; } = true;

        /// <summary>
        /// <para type="description">Whether to apply the changes immediately.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Apply { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var interfaceService = new InterfaceService(ApiClient, Logger);

            // First, get the current interface configuration
            var getResult = ExecuteAsyncTask(() => interfaceService.GetInterfaceDetailAsync(Name));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var currentInterface = getResult.Interface;

            // Create the updated configuration
            var interfaceConfig = new InterfaceConfig
            {
                Description = Description ?? currentInterface.Description,
                IpAddress = IpAddress ?? currentInterface.IpAddress,
                SubnetMask = SubnetMask ?? currentInterface.SubnetMask,
                Gateway = Gateway ?? currentInterface.Gateway,
                Enabled = Enabled.IsPresent ? "1" : "0"
            };

            // Update the interface
            var updateResult = ExecuteAsyncTask(() => interfaceService.UpdateInterfaceAsync(Name, interfaceConfig));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"Interface {Name} updated: {updateResult.Result}");

            // Apply the changes if requested
            if (Apply.IsPresent)
            {
                var restartResult = ExecuteAsyncTask(() => interfaceService.RestartInterfaceAsync(Name));

                // Only continue if no exception occurred
                if (ProcessingException != null || restartResult == null)
                {
                    return;
                }

                WriteVerbose($"Interface {Name} restarted: {restartResult.Status}");
            }
        }
    }
}
