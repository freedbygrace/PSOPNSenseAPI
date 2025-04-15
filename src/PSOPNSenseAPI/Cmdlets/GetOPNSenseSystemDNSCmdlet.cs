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
        protected override void ProcessRecordInternal()
        {
            var systemDNSService = new SystemDNSService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => systemDNSService.GetSystemDNSAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteObject(result.System);
        }
    }
}
