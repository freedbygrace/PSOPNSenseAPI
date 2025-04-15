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
        protected override void ProcessRecordInternal()
        {
            var tailscaleService = new TailscaleService(ApiClient, Logger);

            // Check if the plugin is installed
            var isInstalled = ExecuteAsyncTask(() => tailscaleService.IsPluginInstalledAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null)
            {
                return;
            }

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
            var status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

            var result = new PSObject();
            result.Properties.Add(new PSNoteProperty("PluginInstalled", true));
            result.Properties.Add(new PSNoteProperty("Running", status.Running));
            result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
            result.Properties.Add(new PSNoteProperty("Status", status.Status));

            // Get interfaces if requested
            if (IncludeInterfaces.IsPresent)
            {
                var interfaces = ExecuteAsyncTask(() => tailscaleService.GetInterfacesAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || interfaces == null)
                {
                    return;
                }

                result.Properties.Add(new PSNoteProperty("Interfaces", interfaces.Interfaces));
            }

            // Get settings if requested
            if (IncludeSettings.IsPresent)
            {
                var settings = ExecuteAsyncTask(() => tailscaleService.GetSettingsAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || settings == null)
                {
                    return;
                }

                result.Properties.Add(new PSNoteProperty("Settings", settings.General));
            }

            WriteObject(result);
        }
    }
}
