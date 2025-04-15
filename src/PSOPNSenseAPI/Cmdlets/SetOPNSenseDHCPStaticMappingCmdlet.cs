using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a DHCP static mapping on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseDHCPStaticMapping cmdlet updates a DHCP static mapping on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a static mapping's hostname</para>
    ///     <code>Set-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Hostname "new-printer"</code>
    ///     <para>This example updates the hostname of a static mapping.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update a static mapping's IP address</para>
    ///     <code>Set-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -IpAddress "192.168.1.101"</code>
    ///     <para>This example updates the IP address of a static mapping.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseDHCPStaticMapping")]
    [OutputType(typeof(void))]
    public class SetOPNSenseDHCPStaticMappingCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface of the static mapping to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The UUID of the static mapping to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The MAC address of the static mapping.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string MacAddress { get; set; }

        /// <summary>
        /// <para type="description">The IP address of the static mapping.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string IpAddress { get; set; }

        /// <summary>
        /// <para type="description">The hostname of the static mapping.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Hostname { get; set; }

        /// <summary>
        /// <para type="description">The description of the static mapping.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">Whether the static mapping is enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Enabled { get; set; }

        /// <summary>
        /// <para type="description">Whether the static mapping is disabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled { get; set; }

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

                // First, get the current mapping
                var getTask = Task.Run(async () => await dhcpService.GetStaticMappingAsync(Interface, Uuid));
                var currentMapping = getTask.GetAwaiter().GetResult().Mapping;

                // Create the updated mapping
                var mapping = new DHCPStaticMappingConfig
                {
                    MacAddress = MacAddress ?? currentMapping.MacAddress,
                    IpAddress = IpAddress ?? currentMapping.IpAddress,
                    Hostname = Hostname ?? currentMapping.Hostname,
                    Description = Description ?? currentMapping.Description
                };

                // Handle enabled/disabled state
                if (Enabled.IsPresent && Disabled.IsPresent)
                {
                    WriteWarning("Both -Enabled and -Disabled parameters were specified. Using -Enabled.");
                    mapping.Enabled = "1";
                }
                else if (Enabled.IsPresent)
                {
                    mapping.Enabled = "1";
                }
                else if (Disabled.IsPresent)
                {
                    mapping.Enabled = "0";
                }
                else
                {
                    mapping.Enabled = currentMapping.Enabled;
                }

                // Update the mapping
                var updateTask = Task.Run(async () => await dhcpService.UpdateStaticMappingAsync(Interface, Uuid, mapping));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"Static mapping {Uuid} updated: {updateResult.Result}");

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
