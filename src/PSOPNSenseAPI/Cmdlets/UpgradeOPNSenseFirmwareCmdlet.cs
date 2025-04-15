using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Upgrades the firmware on an OPNSense firewall.</para>
    /// <para type="description">The Upgrade-OPNSenseFirmware cmdlet upgrades the firmware on an OPNSense firewall to a new major version.</para>
    /// <example>
    ///     <para>Example 1: Upgrade firmware</para>
    ///     <code>Upgrade-OPNSenseFirmware</code>
    ///     <para>This example upgrades the firmware on the OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Upgrade firmware and wait for completion</para>
    ///     <code>Upgrade-OPNSenseFirmware -Wait</code>
    ///     <para>This example upgrades the firmware on the OPNSense firewall and waits for the upgrade to complete.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "OPNSenseFirmwareUpgrade", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class UpgradeOPNSenseFirmwareCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">Waits for the upgrade to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// <para type="description">The timeout in seconds to wait for the upgrade to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 3600)]
        public int Timeout { get; set; } = 1200;

        /// <summary>
        /// <para type="description">The interval in seconds between status checks.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 60)]
        public int Interval { get; set; } = 10;

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Upgrade firmware"))
            {
                return;
            }

            var firmwareService = new FirmwareService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => firmwareService.UpgradeAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Firmware upgrade initiated: {result.Status}");
            WriteObject($"Firmware upgrade initiated: {result.Status}");

            if (Wait.IsPresent)
            {
                WriteVerbose($"Waiting for firmware upgrade to complete (timeout: {Timeout} seconds, interval: {Interval} seconds)");
                WriteObject("Waiting for firmware upgrade to complete...");

                DateTime startTime = DateTime.Now;
                bool completed = false;

                while (DateTime.Now - startTime < TimeSpan.FromSeconds(Timeout))
                {
                    try
                    {
                        // Use our safe execution method
                        var statusResult = ExecuteAsyncTask(() => firmwareService.GetStatusAsync());

                        // Skip this iteration if an exception occurred
                        if (ProcessingException != null || statusResult == null)
                        {
                            // Clear the exception since we're ignoring it during the wait
                            ProcessingException = null;
                            WriteVerbose("Error checking status (firewall might be rebooting)");
                            Thread.Sleep(Interval * 1000);
                            continue;
                        }

                        if (statusResult.Status == "done")
                        {
                            WriteVerbose("Firmware upgrade completed successfully");
                            WriteObject("Firmware upgrade completed successfully");
                            completed = true;
                            break;
                        }
                        else if (statusResult.Status == "error")
                        {
                            // Store the exception to be processed in ProcessRecord
                            ProcessingException = new Exception($"Firmware upgrade failed: {statusResult.Log}");
                            completed = true;
                            break;
                        }

                        WriteVerbose($"Firmware upgrade status: {statusResult.Status}");
                    }
                    catch (Exception ex)
                    {
                        // The firewall might be rebooting, so we'll ignore errors during the wait
                        WriteVerbose($"Error checking status (firewall might be rebooting): {ex.Message}");
                    }

                    Thread.Sleep(Interval * 1000);
                }

                if (!completed && ProcessingException == null)
                {
                    WriteWarning($"Timed out waiting for firmware upgrade to complete after {Timeout} seconds");
                }
            }
        }
    }
}
