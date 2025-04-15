using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a new DHCP static mapping on an OPNSense firewall.</para>
    /// <para type="description">The New-OPNSenseDHCPStaticMapping cmdlet creates a new DHCP static mapping on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Create a new static mapping</para>
    ///     <code>New-OPNSenseDHCPStaticMapping -Interface "lan" -MacAddress "00:11:22:33:44:55" -IpAddress "192.168.1.100" -Hostname "printer" -Description "Office Printer"</code>
    ///     <para>This example creates a new static mapping for a printer on the LAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "OPNSenseDHCPStaticMapping")]
    [OutputType(typeof(string))]
    public class NewOPNSenseDHCPStaticMappingCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface to create the static mapping for.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The MAC address of the static mapping.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string MacAddress { get; set; }

        /// <summary>
        /// <para type="description">The IP address of the static mapping.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
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
            var dhcpService = new DHCPService(ApiClient, Logger);

            var mapping = new DHCPStaticMappingConfig
            {
                MacAddress = MacAddress,
                IpAddress = IpAddress,
                Hostname = Hostname,
                Description = Description,
                Enabled = Enabled.IsPresent ? "1" : "0"
            };

            // Use our safe execution method
            var createResult = ExecuteAsyncTask(() => dhcpService.CreateStaticMappingAsync(Interface, mapping));

            // Only continue if no exception occurred
            if (ProcessingException != null || createResult == null)
            {
                return;
            }

            WriteVerbose($"Created static mapping with UUID {createResult.Uuid}");

            // Apply the changes if requested
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
