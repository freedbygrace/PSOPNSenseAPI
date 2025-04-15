using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Disables Tailscale on an OPNSense firewall.</para>
    /// <para type="description">The Disable-OPNSenseTailscale cmdlet disables Tailscale on an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Disable Tailscale</para>
    ///     <code>Disable-OPNSenseTailscale</code>
    ///     <para>This example disables Tailscale on the connected OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Disable Tailscale and stop the service</para>
    ///     <code>Disable-OPNSenseTailscale -Stop</code>
    ///     <para>This example disables Tailscale and stops the Tailscale service.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Disable, "OPNSenseTailscale", SupportsShouldProcess = true)]
    [OutputType(typeof(PSObject))]
    public class DisableOPNSenseTailscaleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Whether to stop the Tailscale service after disabling it.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Stop { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

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
                return;
            }

            // Get current settings
            var settingsResult = ExecuteAsyncTask(() => tailscaleService.GetSettingsAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || settingsResult == null)
            {
                return;
            }

            var currentSettings = settingsResult.General;

            // Create new settings with disabled flag
            var settings = new TailscaleSettings
            {
                Enabled = "0",
                AcceptDns = currentSettings.AcceptDns,
                AcceptRoutes = currentSettings.AcceptRoutes,
                AdvertiseExitNode = currentSettings.AdvertiseExitNode,
                AdvertiseRoutes = currentSettings.AdvertiseRoutes,
                RoutesToAdvertise = currentSettings.RoutesToAdvertise,
                Hostname = currentSettings.Hostname,
                LoginServer = currentSettings.LoginServer,
                Ssh = currentSettings.Ssh,
                Ephemeral = currentSettings.Ephemeral,
                ResetOnStart = currentSettings.ResetOnStart
            };

            if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Disable Tailscale"))
            {
                return;
            }

            // Update settings
            var updateResult = ExecuteAsyncTask(() => tailscaleService.UpdateSettingsAsync(settings));

            // Only continue if no exception occurred
            if (ProcessingException != null || updateResult == null)
            {
                return;
            }

            WriteVerbose($"Tailscale settings updated: {updateResult.Result}");

            // Stop the service if requested
            if (Stop.IsPresent)
            {
                WriteVerbose("Stopping Tailscale service...");
                var stopResult = ExecuteAsyncTask(() => tailscaleService.StopServiceAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || stopResult == null)
                {
                    return;
                }

                WriteVerbose($"Tailscale service stopped: {stopResult.Status}");
            }

            // Get updated status
            var status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

            // Create result object
            var result = new PSObject();
            result.Properties.Add(new PSNoteProperty("PluginInstalled", true));
            result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
            result.Properties.Add(new PSNoteProperty("Running", status.Running));
            result.Properties.Add(new PSNoteProperty("Status", status.Status));

            WriteObject(result);
        }
    }
}
