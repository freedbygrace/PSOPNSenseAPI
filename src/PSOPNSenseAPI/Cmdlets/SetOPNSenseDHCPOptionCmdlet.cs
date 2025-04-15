using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates a DHCP option on an OPNSense firewall.</para>
    /// <para type="description">The Set-OPNSenseDHCPOption cmdlet updates a DHCP option on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update a DHCP option's value</para>
    ///     <code>Set-OPNSenseDHCPOption -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f" -Value "192.168.1.20" -Apply</code>
    ///     <para>This example updates the value of a DHCP option.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "OPNSenseDHCPOption")]
    [OutputType(typeof(void))]
    public class SetOPNSenseDHCPOptionCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface name.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The UUID of the DHCP option to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// <para type="description">The option number.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Number { get; set; }

        /// <summary>
        /// <para type="description">The option value.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Value { get; set; }

        /// <summary>
        /// <para type="description">The option type.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("string", "text", "boolean", "array")]
        public string Type { get; set; }

        /// <summary>
        /// <para type="description">The option description.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

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
            var dhcpService = new DHCPService(ApiClient, Logger);

            // Get current option
            var getResult = ExecuteAsyncTask(() => dhcpService.GetOptionAsync(Interface, Uuid));

            // Only continue if no exception occurred
            if (ProcessingException != null || getResult == null)
            {
                return;
            }

            var currentOption = getResult.Option;

            // Create updated option
            var option = new DHCPOptionConfig
            {
                Number = Number ?? currentOption.Number,
                Value = Value ?? currentOption.Value,
                Type = Type ?? currentOption.Type,
                Description = Description ?? currentOption.Description
            };

            // Update option
            var updateResult = ExecuteAsyncTask(() => dhcpService.UpdateOptionAsync(Interface, Uuid, option));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"DHCP option {Uuid} updated: {updateResult.Result}");

            // Apply changes if requested
            if (Apply.IsPresent)
            {
                var applyResult = ExecuteAsyncTask(() => dhcpService.ApplyChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"DHCP changes applied: {applyResult.Status}");
            }
        }
    }
}
