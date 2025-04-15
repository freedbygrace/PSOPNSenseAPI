using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets the system DNS configuration from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseSystemDNS cmdlet retrieves the system DNS configuration from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get system DNS configuration</para>
    ///     <code>Get-OPNSenseSystemDNS</code>
    ///     <para>This example retrieves the system DNS configuration from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseSystemDNS")]
    [OutputType(typeof(SystemDNSConfig))]
    public class GetOPNSenseSystemDNSCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var systemDNSService = new SystemDNSService(ApiClient, Logger);

                var task = Task.Run(async () => await systemDNSService.GetSystemDNSAsync());
                var result = task.GetAwaiter().GetResult();

                WriteObject(result.System);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
