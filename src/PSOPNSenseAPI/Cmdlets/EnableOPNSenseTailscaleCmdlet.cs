using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Enables and configures Tailscale on an OPNSense firewall.</para>
    /// <para type="description">The Enable-OPNSenseTailscale cmdlet enables and configures Tailscale on an OPNSense firewall. If the Tailscale plugin is not installed, it will be installed automatically.</para>
    /// <example>
    ///     <para>Example 1: Enable Tailscale with default settings</para>
    ///     <code>Enable-OPNSenseTailscale</code>
    ///     <para>This example enables Tailscale with default settings.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Enable Tailscale with custom settings</para>
    ///     <code>Enable-OPNSenseTailscale -AcceptDns -AcceptRoutes -Hostname "opnsense-firewall"</code>
    ///     <para>This example enables Tailscale and configures it to accept DNS and routes from Tailscale, and sets the hostname.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Enable, "OPNSenseTailscale", SupportsShouldProcess = true)]
    [OutputType(typeof(PSObject))]
    public class EnableOPNSenseTailscaleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Whether to accept DNS configuration from Tailscale.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AcceptDns { get; set; }

        /// <summary>
        /// <para type="description">Whether to accept routes from Tailscale.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AcceptRoutes { get; set; }

        /// <summary>
        /// <para type="description">Whether to advertise this node as an exit node.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AdvertiseExitNode { get; set; }

        /// <summary>
        /// <para type="description">Whether to advertise routes to the Tailscale network.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AdvertiseRoutes { get; set; }

        /// <summary>
        /// <para type="description">The routes to advertise to the Tailscale network (e.g., "192.168.1.0/24,10.0.0.0/8").</para>
        /// <para type="description">Multiple routes should be comma-separated.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] SubnetRoutes { get; set; }

        /// <summary>
        /// <para type="description">The hostname to use for this node in the Tailscale network.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Hostname { get; set; }

        /// <summary>
        /// <para type="description">The login server to use (leave empty for default).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string LoginServer { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable SSH access through Tailscale.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter EnableSsh { get; set; }

        /// <summary>
        /// <para type="description">Whether to use an ephemeral node key (node will disappear from network when restarted).</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Ephemeral { get; set; }

        /// <summary>
        /// <para type="description">Whether to reset the Tailscale state on service start.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter ResetOnStart { get; set; }

        /// <summary>
        /// <para type="description">Whether to start the Tailscale service after enabling it.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Start { get; set; } = true;

        /// <summary>
        /// <para type="description">Whether to restart the Tailscale service if it's already running.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Restart { get; set; }

        /// <summary>
        /// <para type="description">Whether to force installation of the Tailscale plugin if it's not already installed.</para>
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
                if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Install Tailscale plugin"))
                {
                    WriteWarning("Tailscale plugin is not installed and -Force was not specified. Operation cancelled.");
                    return;
                }

                WriteVerbose("Tailscale plugin is not installed. Installing...");
                var installResult = ExecuteAsyncTask(() => tailscaleService.InstallPluginAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null)
                {
                    return;
                }

                if (!installResult)
                {
                    ProcessingException = new Exception("Failed to install Tailscale plugin.");
                    return;
                }

                WriteVerbose("Tailscale plugin installed successfully.");
            }

            // Get current settings
            var settingsResult = ExecuteAsyncTask(() => tailscaleService.GetSettingsAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || settingsResult == null)
            {
                return;
            }

            var currentSettings = settingsResult.General;

            // Process subnet routes if provided
            string routesToAdvertise = currentSettings.RoutesToAdvertise;
            if (SubnetRoutes != null && SubnetRoutes.Length > 0)
            {
                routesToAdvertise = string.Join(",", SubnetRoutes);
                WriteVerbose($"Advertising subnet routes: {routesToAdvertise}");

                // If routes are specified but AdvertiseRoutes is not set, enable it automatically
                if (!AdvertiseRoutes.IsPresent)
                {
                    WriteVerbose("Automatically enabling route advertisement because subnet routes were specified.");
                    AdvertiseRoutes = true;
                }
            }

            // Create new settings
            var settings = new TailscaleSettings
            {
                Enabled = "1",
                AcceptDns = AcceptDns.IsPresent ? "1" : "0",
                AcceptRoutes = AcceptRoutes.IsPresent ? "1" : "0",
                AdvertiseExitNode = AdvertiseExitNode.IsPresent ? "1" : "0",
                AdvertiseRoutes = AdvertiseRoutes.IsPresent ? "1" : "0",
                RoutesToAdvertise = routesToAdvertise,
                Hostname = Hostname ?? currentSettings.Hostname,
                LoginServer = LoginServer ?? currentSettings.LoginServer,
                Ssh = EnableSsh.IsPresent ? "1" : "0",
                Ephemeral = Ephemeral.IsPresent ? "1" : "0",
                ResetOnStart = ResetOnStart.IsPresent ? "1" : "0"
            };

            if (!ShouldProcess("OPNSense firewall", "Enable and configure Tailscale"))
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

            // Get current status
            var status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

            // Start or restart the service if requested
            if (Restart.IsPresent || (Start.IsPresent && !status.Running))
            {
                if (status.Running)
                {
                    if (Restart.IsPresent)
                    {
                        WriteVerbose("Restarting Tailscale service...");
                        var restartResult = ExecuteAsyncTask(() => tailscaleService.RestartServiceAsync());

                        // Only continue if no exception occurred
                        if (ProcessingException != null || restartResult == null)
                        {
                            return;
                        }

                        WriteVerbose($"Tailscale service restarted: {restartResult.Status}");
                    }
                }
                else if (Start.IsPresent)
                {
                    WriteVerbose("Starting Tailscale service...");
                    var startResult = ExecuteAsyncTask(() => tailscaleService.StartServiceAsync());

                    // Only continue if no exception occurred
                    if (ProcessingException != null || startResult == null)
                    {
                        return;
                    }

                    WriteVerbose($"Tailscale service started: {startResult.Status}");
                }
            }

            // Get updated status
            status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

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
            result.Properties.Add(new PSNoteProperty("Settings", settings));

            WriteObject(result);
        }
    }
}
