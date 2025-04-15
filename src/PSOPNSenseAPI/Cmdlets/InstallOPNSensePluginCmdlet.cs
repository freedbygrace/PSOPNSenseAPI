using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Installs a plugin on an OPNSense firewall.</para>
    /// <para type="description">The Install-OPNSensePlugin cmdlet installs a plugin on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Install a plugin</para>
    ///     <code>Install-OPNSensePlugin -Name "os-acme-client"</code>
    ///     <para>This example installs the ACME client plugin on the OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Install a plugin and wait for completion</para>
    ///     <code>Install-OPNSensePlugin -Name "os-acme-client" -Wait</code>
    ///     <para>This example installs the ACME client plugin on the OPNSense firewall and waits for the installation to complete.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Install, "OPNSensePlugin", SupportsShouldProcess = true)]
    [OutputType(typeof(void))]
    public class InstallOPNSensePluginCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The name of the plugin to install.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Waits for the installation to complete.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// <para type="description">The timeout in seconds to wait for the installation to complete.</para>
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
        protected override void ProcessRecord()
        {
            try
            {
                if (!ShouldProcess(Name, "Install plugin"))
                {
                    return;
                }

                var pluginService = new PluginService(ApiClient, Logger);

                var task = Task.Run(async () => await pluginService.InstallPluginAsync(Name));
                var result = task.GetAwaiter().GetResult();

                WriteVerbose($"Plugin installation initiated: {result.Status}");
                WriteObject($"Plugin installation initiated: {result.Status}");

                if (Wait.IsPresent)
                {
                    WriteVerbose($"Waiting for plugin installation to complete (timeout: {Timeout} seconds, interval: {Interval} seconds)");
                    WriteObject("Waiting for plugin installation to complete...");

                    DateTime startTime = DateTime.Now;
                    bool completed = false;

                    while (DateTime.Now - startTime < TimeSpan.FromSeconds(Timeout))
                    {
                        var statusTask = Task.Run(async () => await pluginService.GetPluginStatusAsync());
                        var statusResult = statusTask.GetAwaiter().GetResult();

                        if (statusResult.Status == "done")
                        {
                            WriteVerbose("Plugin installation completed successfully");
                            WriteObject("Plugin installation completed successfully");
                            completed = true;
                            break;
                        }
                        else if (statusResult.Status == "error")
                        {
                            WriteError(new ErrorRecord(
                                new Exception($"Plugin installation failed: {statusResult.Log}"),
                                "PluginInstallationFailed",
                                ErrorCategory.InvalidOperation,
                                Name));
                            completed = true;
                            break;
                        }

                        WriteVerbose($"Plugin installation status: {statusResult.Status}");
                        Thread.Sleep(Interval * 1000);
                    }

                    if (!completed)
                    {
                        WriteWarning($"Timed out waiting for plugin installation to complete after {Timeout} seconds");
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
