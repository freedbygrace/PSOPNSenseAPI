using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Uninstalls a plugin from an OPNSense firewall.</para>
    /// <para type="description">The Uninstall-OPNSensePlugin cmdlet uninstalls a plugin from an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Uninstall a plugin</para>
    ///     <code>Uninstall-OPNSensePlugin -Name "os-acme-client"</code>
    ///     <para>This example uninstalls the ACME client plugin from the OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Uninstall a plugin and wait for completion</para>
    ///     <code>Uninstall-OPNSensePlugin -Name "os-acme-client" -Wait</code>
    ///     <para>This example uninstalls the ACME client plugin from the OPNSense firewall and waits for the uninstallation to complete.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Uninstall, "OPNSensePlugin", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class UninstallOPNSensePluginCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the plugin to uninstall.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">Waits for the uninstallation to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// <para type="description">The timeout in seconds to wait for the uninstallation to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 3600)]
        public int Timeout { get; set; } = 300;

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
            if (!Force.IsPresent && !ShouldProcess(Name, "Uninstall plugin"))
            {
                return;
            }

            var pluginService = new PluginService(ApiClient, Logger);

            // Use our safe execution method
            var result = ExecuteAsyncTask(() => pluginService.UninstallPluginAsync(Name));

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Plugin uninstallation initiated: {result.Status}");
            WriteObject($"Plugin uninstallation initiated: {result.Status}");

            if (Wait.IsPresent)
            {
                WriteVerbose($"Waiting for plugin uninstallation to complete (timeout: {Timeout} seconds, interval: {Interval} seconds)");
                WriteObject("Waiting for plugin uninstallation to complete...");

                DateTime startTime = DateTime.Now;
                bool completed = false;

                while (DateTime.Now - startTime < TimeSpan.FromSeconds(Timeout))
                {
                    // Use our safe execution method
                    var statusResult = ExecuteAsyncTask(() => pluginService.GetPluginStatusAsync());

                    // Break if an exception occurred
                    if (ProcessingException != null || statusResult == null)
                    {
                        break;
                    }

                    if (statusResult.Status == "done")
                    {
                        WriteVerbose("Plugin uninstallation completed successfully");
                        WriteObject("Plugin uninstallation completed successfully");
                        completed = true;
                        break;
                    }
                    else if (statusResult.Status == "error")
                    {
                        // Store the exception to be processed in ProcessRecord
                        ProcessingException = new Exception($"Plugin uninstallation failed: {statusResult.Log}");
                        completed = true;
                        break;
                    }

                    WriteVerbose($"Plugin uninstallation status: {statusResult.Status}");
                    Thread.Sleep(Interval * 1000);
                }

                if (!completed && ProcessingException == null)
                {
                    WriteWarning($"Timed out waiting for plugin uninstallation to complete after {Timeout} seconds");
                }
            }
        }
    }
}
