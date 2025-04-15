using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets the status of Tailscale on an OPNSense firewall.</para>
    /// <para type="description">The Get-OPNSenseTailscaleStatus cmdlet retrieves the current status of Tailscale on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get Tailscale status</para>
    ///     <code>Get-OPNSenseTailscaleStatus</code>
    ///     <para>This example retrieves the current status of Tailscale on the connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseTailscaleStatus")]
    [OutputType(typeof(PSObject))]
    public class GetOPNSenseTailscaleStatusCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Whether to include detailed information about Tailscale interfaces.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeInterfaces { get; set; }

        /// <summary>
        /// <para type="description">Whether to include Tailscale settings.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter IncludeSettings { get; set; }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var tailscaleService = new TailscaleService(ApiClient, Logger);

                // Check if the plugin is installed
                var isInstalledTask = Task.Run(async () => await tailscaleService.IsPluginInstalledAsync());
                var isInstalled = isInstalledTask.GetAwaiter().GetResult();

                if (!isInstalled)
                {
                    WriteWarning("Tailscale plugin is not installed on the OPNSense firewall.");

                    var statusResult = new PSObject();
                    statusResult.Properties.Add(new PSNoteProperty("PluginInstalled", false));
                    statusResult.Properties.Add(new PSNoteProperty("Running", false));
                    statusResult.Properties.Add(new PSNoteProperty("Enabled", false));
                    statusResult.Properties.Add(new PSNoteProperty("Status", "Plugin not installed"));

                    WriteObject(statusResult);
                    return;
                }

                // Get the status
                var statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                var status = statusTask.GetAwaiter().GetResult();

                var result = new PSObject();
                result.Properties.Add(new PSNoteProperty("PluginInstalled", true));
                result.Properties.Add(new PSNoteProperty("Running", status.Running));
                result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
                result.Properties.Add(new PSNoteProperty("Status", status.Status));

                // Get interfaces if requested
                if (IncludeInterfaces.IsPresent)
                {
                    var interfacesTask = Task.Run(async () => await tailscaleService.GetInterfacesAsync());
                    var interfaces = interfacesTask.GetAwaiter().GetResult();

                    result.Properties.Add(new PSNoteProperty("Interfaces", interfaces.Interfaces));
                }

                // Get settings if requested
                if (IncludeSettings.IsPresent)
                {
                    var settingsTask = Task.Run(async () => await tailscaleService.GetSettingsAsync());
                    var settings = settingsTask.GetAwaiter().GetResult();

                    result.Properties.Add(new PSNoteProperty("Settings", settings.General));
                }

                WriteObject(result);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
