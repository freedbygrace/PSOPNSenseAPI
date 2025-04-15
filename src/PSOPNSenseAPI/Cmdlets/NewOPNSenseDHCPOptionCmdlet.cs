using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new DHCP option on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseDHCPOption cmdlet creates a new DHCP option on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new DHCP option</para>
    ///     <code>New-OPNSenseDHCPOption -Interface "lan" -Number 66 -Value "192.168.1.10" -Description "TFTP Server" -Apply</code>
    ///     <para>This example creates a new DHCP option for the TFTP server.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseDHCPOption")]
    [OutputType(typeof(string))]
    public class NewOPNSenseDHCPOptionCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface name.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The option number.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Number { get; set; }

        /// <summary>
        /// <para type="description">The option value.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string Value { get; set; }

        /// <summary>
        /// <para type="description">The option type.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateSet("string", "text", "boolean", "array")]
        public string Type { get; set; } = "string";

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

            var option = new DHCPOptionConfig
            {
                Number = Number,
                Value = Value,
                Type = Type,
                Description = Description
            };

            // Use our safe execution method
            var createResult = ExecuteAsyncTask(() => dhcpService.CreateOptionAsync(Interface, option));

            // Only continue if no exception occurred
            if (ProcessingException != null || createResult == null)
            {
                return;
            }

            WriteVerbose($"Created DHCP option with UUID {createResult.Uuid}");

            // Apply changes if requested
            if (Apply.IsPresent)
            {
                // Use our safe execution method
                var applyResult = ExecuteAsyncTask(() => dhcpService.ApplyChangesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || applyResult == null)
                {
                    return;
                }

                WriteVerbose($"DHCP changes applied: {applyResult.Status}");
            }

            WriteObject(createResult.Uuid);
        }
    }
}
