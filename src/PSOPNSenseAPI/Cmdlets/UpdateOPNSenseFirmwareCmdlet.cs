using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Updates the firmware on an OPNSense firewall.</para>
    /// <para type="description">The Update-OPNSenseFirmware cmdlet updates the firmware on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Update firmware</para>
    ///     <code>Update-OPNSenseFirmware</code>
    ///     <para>This example updates the firmware on the OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Update firmware and wait for completion</para>
    ///     <code>Update-OPNSenseFirmware -Wait</code>
    ///     <para>This example updates the firmware on the OPNSense firewall and waits for the update to complete.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Update, "OPNSenseFirmware", SupportsShouldProcess = true)]
    [OutputType(typeof(void))]
    public class UpdateOPNSenseFirmwareCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Waits for the update to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// <para type="description">The timeout in seconds to wait for the update to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 3600)]
        public int Timeout { get; set; } = 600;

        /// <summary>
        /// <para type="description">The interval in seconds between status checks.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 60)]
        public int Interval { get; set; } = 5;

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            if (!ShouldProcess("OPNSense firewall", "Update firmware"))
            {
                return;
            }

            var firmwareService = new FirmwareService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => firmwareService.UpdateAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Firmware update initiated: {result.Status}");
            WriteObject($"Firmware update initiated: {result.Status}");

            if (Wait.IsPresent)
            {
                WriteVerbose($"Waiting for firmware update to complete (timeout: {Timeout} seconds, interval: {Interval} seconds)");
                WriteObject("Waiting for firmware update to complete...");

                DateTime startTime = DateTime.Now;
                bool completed = false;

                while (DateTime.Now - startTime < TimeSpan.FromSeconds(Timeout))
                {
                    // Use our safe execution method
                    var statusResult = ExecuteAsyncTask(() => firmwareService.GetStatusAsync());

                    // Break if an exception occurred
                    if (ProcessingException != null || statusResult == null)
                    {
                        break;
                    }

                    if (statusResult.Status == "done")
                    {
                        WriteVerbose("Firmware update completed successfully");
                        WriteObject("Firmware update completed successfully");
                        completed = true;
                        break;
                    }
                    else if (statusResult.Status == "error")
                    {
                        // Store the exception to be processed in ProcessRecord
                        ProcessingException = new Exception($"Firmware update failed: {statusResult.Log}");
                        completed = true;
                        break;
                    }

                    WriteVerbose($"Firmware update status: {statusResult.Status}");
                    Thread.Sleep(Interval * 1000);
                }

                if (!completed && ProcessingException == null)
                {
                    WriteWarning($"Timed out waiting for firmware update to complete after {Timeout} seconds");
                }
            }
        }
    }
}
