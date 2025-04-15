using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Restores an OPNSense firewall configuration from a backup.</para>
    /// <para type="description">The Restore-OPNSenseConfig cmdlet restores an OPNSense firewall configuration from a backup.</para>
    /// <example>
    ///     <para>Example 1: Restore a configuration backup</para>
    ///     <code>Restore-OPNSenseConfig -Filename "config-backup-20250414-1234.xml"</code>
    ///     <para>This example restores an OPNSense firewall configuration from a backup file.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Restore, "OPNSenseConfig", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RestoreOPNSenseConfigCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The filename of the backup to restore.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Filename { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                if (!Force.IsPresent && !ShouldProcess(Filename, "Restore configuration backup"))
                {
                    return;
                }

                var configService = new ConfigService(ApiClient, Logger);

                var task = Task.Run(async () => await configService.RestoreConfigBackupAsync(Filename));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Restored configuration backup: {result.Status}");
                WriteWarning("The firewall is restarting. You may need to reconnect after it comes back online.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
