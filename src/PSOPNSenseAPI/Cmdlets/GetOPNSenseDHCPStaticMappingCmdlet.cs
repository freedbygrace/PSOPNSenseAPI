using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets DHCP static mappings from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseDHCPStaticMapping cmdlet retrieves DHCP static mappings from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all static mappings for an interface</para>
    ///     <code>Get-OPNSenseDHCPStaticMapping -Interface "lan"</code>
    ///     <para>This example retrieves all static mappings for the LAN interface.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific static mapping by UUID</para>
    ///     <code>Get-OPNSenseDHCPStaticMapping -Interface "lan" -Uuid "9e4ec4f0-9dd1-4fa3-8c1d-8a8e9d772b0f"</code>
    ///     <para>This example retrieves a specific static mapping by its UUID.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseDHCPStaticMapping")]
    [OutputType(typeof(DHCPStaticMapping), typeof(DHCPStaticMappingDetail))]
    public class GetOPNSenseDHCPStaticMappingCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The interface to get static mappings for.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Interface { get; set; }

        /// <summary>
        /// <para type="description">The UUID of the static mapping to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = "ByUuid")]
        [ValidateNotNullOrEmpty]
        public string Uuid { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var dhcpService = new DHCPService(ApiClient, Logger);

                if (ParameterSetName == "ByUuid")
                {
                    var task = Task.Run(async () => await dhcpService.GetStaticMappingAsync(Interface, Uuid));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Mapping);
                }
                else
                {
                    var task = Task.Run(async () => await dhcpService.GetStaticMappingsAsync(Interface));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Rows, true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
