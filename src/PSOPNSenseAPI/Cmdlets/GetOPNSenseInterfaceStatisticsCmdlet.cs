using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets interface statistics from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseInterfaceStatistics cmdlet retrieves interface statistics from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all interface statistics</para>
    ///     <code>Get-OPNSenseInterfaceStatistics</code>
    ///     <para>This example retrieves statistics for all interfaces from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get statistics for a specific interface</para>
    ///     <code>Get-OPNSenseInterfaceStatistics | Where-Object { $_.Name -eq "wan" }</code>
    ///     <para>This example retrieves statistics for the WAN interface.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseInterfaceStatistics")]
    [OutputType(typeof(PSObject))]
    public class GetOPNSenseInterfaceStatisticsCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var interfaceService = new InterfaceService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => interfaceService.GetInterfaceStatisticsAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            foreach (var kvp in result.Statistics)
            {
                var stats = new PSObject();
                stats.Properties.Add(new PSNoteProperty("Name", kvp.Key));
                stats.Properties.Add(new PSNoteProperty("InPackets", kvp.Value.InPackets));
                stats.Properties.Add(new PSNoteProperty("InBytes", kvp.Value.InBytes));
                stats.Properties.Add(new PSNoteProperty("InErrors", 0));
                stats.Properties.Add(new PSNoteProperty("OutPackets", kvp.Value.OutPackets));
                stats.Properties.Add(new PSNoteProperty("OutBytes", kvp.Value.OutBytes));
                stats.Properties.Add(new PSNoteProperty("OutErrors", 0));
                stats.Properties.Add(new PSNoteProperty("Collisions", 0));

                WriteObject(stats);
            }
        }
    }
}
