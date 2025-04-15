using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Connects an OPNSense firewall to a Tailscale network.</para>
    /// <para type="description">The Connect-OPNSenseTailscale cmdlet connects an OPNSense firewall to a Tailscale network using an authentication key.</para>
    /// <example>
    ///     <para>Example 1: Connect to Tailscale network</para>
    ///     <code>Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456"</code>
    ///     <para>This example connects the OPNSense firewall to a Tailscale network using the specified authentication key.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Connect to Tailscale network with auto-installation</para>
    ///     <code>Connect-OPNSenseTailscale -AuthKey "tskey-auth-abcdef123456" -InstallIfMissing</code>
    ///     <para>This example installs the Tailscale plugin if it's not already installed, then connects to the Tailscale network.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "OPNSenseTailscale", SupportsShouldProcess = true)]
    [OutputType(typeof(PSObject))]
    public class ConnectOPNSenseTailscaleCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">The Tailscale authentication key to use for connecting.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string AuthKey { get; set; }

        /// <summary>
        /// <para type="description">Whether to install the Tailscale plugin if it's not already installed.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter InstallIfMissing { get; set; }

        /// <summary>
        /// <para type="description">Whether to enable Tailscale if it's not already enabled.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter EnableIfDisabled { get; set; }

        /// <summary>
        /// <para type="description">Whether to start the Tailscale service if it's not already running.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter StartIfStopped { get; set; }

        /// <summary>
        /// <para type="description">Whether to advertise routes to the Tailscale network.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AdvertiseRoutes { get; set; }

        /// <summary>
        /// <para type="description">The subnet routes to advertise to the Tailscale network (e.g., "192.168.1.0/24", "10.0.0.0/8").</para>
        /// <para type="description">Multiple routes can be specified.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string[] SubnetRoutes { get; set; }

        /// <summary>
        /// <para type="description">Whether to advertise this node as an exit node.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter AdvertiseExitNode { get; set; }

        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
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
                    if (!InstallIfMissing.IsPresent)
                    {
                        WriteError(new ErrorRecord(
                            new Exception("Tailscale plugin is not installed. Use -InstallIfMissing to install it."),
                            "TailscalePluginNotInstalled",
                            ErrorCategory.InvalidOperation,
                            null));
                        return;
                    }

                    if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Install Tailscale plugin"))
                    {
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

                // Get current status
                var statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                var status = statusTask.GetAwaiter().GetResult();

                // Enable Tailscale if needed
                if (!status.Enabled && EnableIfDisabled.IsPresent)
                {
                    WriteVerbose("Tailscale is disabled. Enabling...");

                    // Get current settings
                    var settingsTask = Task.Run(async () => await tailscaleService.GetSettingsAsync());
                    var currentSettings = settingsTask.GetAwaiter().GetResult().General;

                    // Process subnet routes if provided
                    string routesToAdvertise = currentSettings.RoutesToAdvertise;
                    bool advertiseRoutes = AdvertiseRoutes.IsPresent || currentSettings.AdvertiseRoutes == "1";
                    bool advertiseExitNode = AdvertiseExitNode.IsPresent || currentSettings.AdvertiseExitNode == "1";

                    if (SubnetRoutes != null && SubnetRoutes.Length > 0)
                    {
                        routesToAdvertise = string.Join(",", SubnetRoutes);
                        WriteVerbose($"Advertising subnet routes: {routesToAdvertise}");

                        // If routes are specified but AdvertiseRoutes is not set, enable it automatically
                        if (!advertiseRoutes)
                        {
                            WriteVerbose("Automatically enabling route advertisement because subnet routes were specified.");
                            advertiseRoutes = true;
                        }
                    }

                    // Create new settings with enabled flag
                    var settings = new TailscaleSettings
                    {
                        Enabled = "1",
                        AcceptDns = currentSettings.AcceptDns,
                        AcceptRoutes = currentSettings.AcceptRoutes,
                        AdvertiseExitNode = advertiseExitNode ? "1" : "0",
                        AdvertiseRoutes = advertiseRoutes ? "1" : "0",
                        RoutesToAdvertise = routesToAdvertise,
                        Hostname = currentSettings.Hostname,
                        LoginServer = currentSettings.LoginServer,
                        Ssh = currentSettings.Ssh,
                        Ephemeral = currentSettings.Ephemeral,
                        ResetOnStart = currentSettings.ResetOnStart
                    };

                    // Update settings
                    var updateTask = Task.Run(async () => await tailscaleService.UpdateSettingsAsync(settings));
                    var updateResult = updateTask.GetAwaiter().GetResult();

                    WriteVerbose($"Tailscale settings updated: {updateResult.Result}");

                    // Get updated status
                    statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                    status = statusTask.GetAwaiter().GetResult();
                }

                // Start the service if needed
                if (!status.Running && StartIfStopped.IsPresent)
                {
                    WriteVerbose("Tailscale service is not running. Starting...");
                    var startTask = Task.Run(async () => await tailscaleService.StartServiceAsync());
                    var startResult = startTask.GetAwaiter().GetResult();
                    WriteVerbose($"Tailscale service started: {startResult.Status}");

                    // Get updated status
                    statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                    status = statusTask.GetAwaiter().GetResult();
                }

                // Check if Tailscale is enabled and running
                if (!status.Enabled)
                {
                    WriteError(new ErrorRecord(
                        new Exception("Tailscale is not enabled. Use -EnableIfDisabled to enable it."),
                        "TailscaleNotEnabled",
                        ErrorCategory.InvalidOperation,
                        null));
                    return;
                }

                if (!status.Running)
                {
                    WriteError(new ErrorRecord(
                        new Exception("Tailscale service is not running. Use -StartIfStopped to start it."),
                        "TailscaleNotRunning",
                        ErrorCategory.InvalidOperation,
                        null));
                    return;
                }

                if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Connect to Tailscale network"))
                {
                    return;
                }

                // Connect to Tailscale
                WriteVerbose("Connecting to Tailscale network...");
                var connectTask = Task.Run(async () => await tailscaleService.ConnectAsync(AuthKey));
                var connectResult = connectTask.GetAwaiter().GetResult();

                WriteVerbose($"Tailscale connection status: {connectResult.Status}");
                WriteVerbose($"Tailscale connection message: {connectResult.Message}");

                // Get updated status
                statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                status = statusTask.GetAwaiter().GetResult();

                // Get interfaces
                var interfacesTask = Task.Run(async () => await tailscaleService.GetInterfacesAsync());
                var interfaces = interfacesTask.GetAwaiter().GetResult();

                // Create result object
                var result = new PSObject();
                result.Properties.Add(new PSNoteProperty("Status", status.Status));
                result.Properties.Add(new PSNoteProperty("Running", status.Running));
                result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
                result.Properties.Add(new PSNoteProperty("ConnectionStatus", connectResult.Status));
                result.Properties.Add(new PSNoteProperty("ConnectionMessage", connectResult.Message));
                result.Properties.Add(new PSNoteProperty("Interfaces", interfaces.Interfaces));

                WriteObject(result);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
