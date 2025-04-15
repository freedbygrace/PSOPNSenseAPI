using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets interfaces from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseInterface cmdlet retrieves interfaces from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all interfaces</para>
    ///     <code>Get-OPNSenseInterface</code>
    ///     <para>This example retrieves all interfaces from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get a specific interface by name</para>
    ///     <code>Get-OPNSenseInterface -Name "wan"</code>
    ///     <para>This example retrieves a specific interface by its name.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseInterface")]
    [OutputType(typeof(InterfaceInfo), typeof(InterfaceDetail))]
    public class GetOPNSenseInterfaceCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the interface to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = "ByName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var interfaceService = new InterfaceService(ApiClient, Logger);

                if (ParameterSetName == "ByName")
                {
                    var task = Task.Run(async () => await interfaceService.GetInterfaceDetailAsync(Name));
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Interface);
                }
                else
                {
                    var task = Task.Run(async () => await interfaceService.GetInterfacesAsync());
                    var result = task.GetAwaiter().GetResult();
                    WriteObject(result.Interfaces, true);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
