using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets configuration backups from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseConfigBackup cmdlet retrieves configuration backups from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get all configuration backups</para>
    ///     <code>Get-OPNSenseConfigBackup</code>
    ///     <para>This example retrieves all configuration backups from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseConfigBackup")]
    [OutputType(typeof(ConfigBackup))]
    public class GetOPNSenseConfigBackupCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            var configService = new ConfigService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => configService.GetConfigBackupsAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteObject(result.Backups, true);
        }
    }
}
