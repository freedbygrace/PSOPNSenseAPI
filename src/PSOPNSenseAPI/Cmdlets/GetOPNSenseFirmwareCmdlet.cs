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
        protected override void ProcessRecordInternal()
        {
            var firmwareService = new FirmwareService(ApiClient, Logger);

            switch (ParameterSetName)
            {
                case "Changelog":
                    // Use our safe execution method
                    var changelogResult = ExecuteAsyncTask(() => firmwareService.GetChangelogAsync());

                    // Only continue if no exception occurred
                    if (ProcessingException != null || changelogResult == null)
                    {
                        return;
                    }

                    WriteObject(changelogResult.Changelog);
                    break;

                case "Audit":
                    // Use our safe execution method
                    var auditResult = ExecuteAsyncTask(() => firmwareService.GetAuditAsync());

                    // Only continue if no exception occurred
                    if (ProcessingException != null || auditResult == null)
                    {
                        return;
                    }

                    WriteObject(auditResult.Audit, true);
                    break;

                case "Health":
                    // Use our safe execution method
                    var healthResult = ExecuteAsyncTask(() => firmwareService.GetHealthAsync());

                    // Only continue if no exception occurred
                    if (ProcessingException != null || healthResult == null)
                    {
                        return;
                    }

                    WriteObject(healthResult.Health);
                    break;

                case "Check":
                    // Use our safe execution method
                    var checkResult = ExecuteAsyncTask(() => firmwareService.CheckForUpdatesAsync());

                    // Only continue if no exception occurred
                    if (ProcessingException != null || checkResult == null)
                    {
                        return;
                    }

                    WriteObject($"Check for updates: {checkResult.Status}");
                    break;

                default:
                    // Use our safe execution method
                    var statusResult = ExecuteAsyncTask(() => firmwareService.GetStatusAsync());

                    // Only continue if no exception occurred
                    if (ProcessingException != null || statusResult == null)
                    {
                        return;
                    }

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
    }
}
