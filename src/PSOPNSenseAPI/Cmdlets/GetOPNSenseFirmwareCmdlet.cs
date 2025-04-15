using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets firmware information from an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseFirmware cmdlet retrieves firmware information from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get firmware status</para>
    ///     <code>Get-OPNSenseFirmware</code>
    ///     <para>This example retrieves the firmware status from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Get firmware changelog</para>
    ///     <code>Get-OPNSenseFirmware -Changelog</code>
    ///     <para>This example retrieves the firmware changelog from the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 3: Get firmware audit</para>
    ///     <code>Get-OPNSenseFirmware -Audit</code>
    ///     <para>This example retrieves the firmware audit from the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseFirmware")]
    [OutputType(typeof(PSObject))]
    public class GetOPNSenseFirmwareCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Gets the firmware changelog.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Changelog")]
        public SwitchParameter Changelog { get; set; }

        /// <summary>
        /// <para type="description">Gets the firmware audit.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Audit")]
        public SwitchParameter Audit { get; set; }

        /// <summary>
        /// <para type="description">Gets the firmware health.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Health")]
        public SwitchParameter Health { get; set; }

        /// <summary>
        /// <para type="description">Checks for firmware updates.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = "Check")]
        public SwitchParameter Check { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var firmwareService = new FirmwareService(ApiClient, Logger);

                switch (ParameterSetName)
                {
                    case "Changelog":
                        var changelogTask = Task.Run(async () => await firmwareService.GetChangelogAsync());
                        var changelogResult = changelogTask.GetAwaiter().GetResult();
                        WriteObject(changelogResult.Changelog);
                        break;

                    case "Audit":
                        var auditTask = Task.Run(async () => await firmwareService.GetAuditAsync());
                        var auditResult = auditTask.GetAwaiter().GetResult();
                        WriteObject(auditResult.Audit, true);
                        break;

                    case "Health":
                        var healthTask = Task.Run(async () => await firmwareService.GetHealthAsync());
                        var healthResult = healthTask.GetAwaiter().GetResult();
                        WriteObject(healthResult.Health);
                        break;

                    case "Check":
                        var checkTask = Task.Run(async () => await firmwareService.CheckForUpdatesAsync());
                        var checkResult = checkTask.GetAwaiter().GetResult();
                        WriteObject($"Check for updates: {checkResult.Status}");
                        break;

                    default:
                        var statusTask = Task.Run(async () => await firmwareService.GetStatusAsync());
                        var statusResult = statusTask.GetAwaiter().GetResult();

                        var firmware = new PSObject();
                        firmware.Properties.Add(new PSNoteProperty("Status", statusResult.Status));
                        firmware.Properties.Add(new PSNoteProperty("Connection", statusResult.Connection));
                        firmware.Properties.Add(new PSNoteProperty("DownloadSize", statusResult.DownloadSize));
                        firmware.Properties.Add(new PSNoteProperty("LastCheck", statusResult.LastCheck));
                        firmware.Properties.Add(new PSNoteProperty("UpgradeMessage", statusResult.UpgradeMessage));
                        firmware.Properties.Add(new PSNoteProperty("Updates", statusResult.Updates));
                        
                        WriteObject(firmware);
                        break;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
