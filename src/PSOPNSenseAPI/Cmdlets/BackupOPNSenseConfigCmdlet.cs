using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Creates a backup of the OPNSense firewall configuration.</para>
    /// <para type="description">The Backup-OPNSenseConfig cmdlet creates a backup of the OPNSense firewall configuration.</para>
    /// <example>
    ///     <para>Example 1: Create a backup</para>
    ///     <code>Backup-OPNSenseConfig</code>
    ///     <para>This example creates a backup of the OPNSense firewall configuration with an automatically generated filename.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Create a backup with a specific filename</para>
    ///     <code>Backup-OPNSenseConfig -Filename "pre-upgrade-backup"</code>
    ///     <para>This example creates a backup of the OPNSense firewall configuration with a specific filename.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Backup, "OPNSenseConfig")]
    [OutputType(typeof(string))]
    public class BackupOPNSenseConfigCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The filename for the backup.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0)]
        public string Filename { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var configService = new ConfigService(ApiClient, Logger);

                var task = Task.Run(async () => await configService.CreateConfigBackupAsync(Filename));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Created configuration backup: {result.Filename}");
                WriteObject(result.Filename);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
