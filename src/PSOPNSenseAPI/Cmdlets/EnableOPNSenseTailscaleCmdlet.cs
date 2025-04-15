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
                    if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Install Tailscale plugin"))
                    {
                        WriteWarning("Tailscale plugin is not installed and -Force was not specified. Operation cancelled.");
                        return;
                    }

                    WriteVerbose("Tailscale plugin is not installed. Installing...");
                    var installTask = Task.Run(async () => await tailscaleService.InstallPluginAsync());
                    var installResult = installTask.GetAwaiter().GetResult();

                    if (!installResult)
                    {
                        WriteError(new ErrorRecord(
                            new Exception("Failed to install Tailscale plugin."),
                            "TailscalePluginInstallFailed",
                            ErrorCategory.InvalidOperation,
                            null));
                        return;
                    }

                    WriteVerbose("Tailscale plugin installed successfully.");
                }

                // Get current settings
                var settingsTask = Task.Run(async () => await tailscaleService.GetSettingsAsync());
                var currentSettings = settingsTask.GetAwaiter().GetResult().General;

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
                var updateTask = Task.Run(async () => await tailscaleService.UpdateSettingsAsync(settings));
                var updateResult = updateTask.GetAwaiter().GetResult();

                WriteVerbose($"Tailscale settings updated: {updateResult.Result}");

                // Get current status
                var statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                var status = statusTask.GetAwaiter().GetResult();

                // Start or restart the service if requested
                if (Restart.IsPresent || (Start.IsPresent && !status.Running))
                {
                    if (status.Running)
                    {
                        if (Restart.IsPresent)
                        {
                            WriteVerbose("Restarting Tailscale service...");
                            var restartTask = Task.Run(async () => await tailscaleService.RestartServiceAsync());
                            var restartResult = restartTask.GetAwaiter().GetResult();
                            WriteVerbose($"Tailscale service restarted: {restartResult.Status}");
                        }
                    }
                    else if (Start.IsPresent)
                    {
                        WriteVerbose("Starting Tailscale service...");
                        var startTask = Task.Run(async () => await tailscaleService.StartServiceAsync());
                        var startResult = startTask.GetAwaiter().GetResult();
                        WriteVerbose($"Tailscale service started: {startResult.Status}");
                    }
                }

                // Get updated status
                statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                status = statusTask.GetAwaiter().GetResult();

                // Create result object
                var result = new PSObject();
                result.Properties.Add(new PSNoteProperty("PluginInstalled", true));
                result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
                result.Properties.Add(new PSNoteProperty("Running", status.Running));
                result.Properties.Add(new PSNoteProperty("Status", status.Status));
                result.Properties.Add(new PSNoteProperty("Settings", settings));

                WriteObject(result);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
