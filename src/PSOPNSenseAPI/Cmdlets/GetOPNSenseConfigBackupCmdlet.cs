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
        protected override void ProcessRecord()
        {
            try
            {
                var configService = new ConfigService(ApiClient, Logger);

                var task = Task.Run(async () => await configService.GetConfigBackupsAsync());
                var result = task.GetAwaiter().GetResult();

                WriteObject(result.Backups, true);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
