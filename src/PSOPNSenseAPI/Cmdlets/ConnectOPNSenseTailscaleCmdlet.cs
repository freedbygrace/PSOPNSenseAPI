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
                if (!InstallIfMissing.IsPresent)
                {
                    ProcessingException = new Exception("Tailscale plugin is not installed. Use -InstallIfMissing to install it.");
                    return;
                }

                if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Install Tailscale plugin"))
                {
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

            // Get current status
            var status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

            // Enable Tailscale if needed
            if (!status.Enabled && EnableIfDisabled.IsPresent)
            {
                WriteVerbose("Tailscale is disabled. Enabling...");

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
                var updateResult = ExecuteAsyncTask(() => tailscaleService.UpdateSettingsAsync(settings));

                // Only continue if no exception occurred
                if (ProcessingException != null || updateResult == null)
                {
                    return;
                }

                WriteVerbose($"Tailscale settings updated: {updateResult.Result}");

                // Get updated status
                status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || status == null)
                {
                    return;
                }
            }

            // Start the service if needed
            if (!status.Running && StartIfStopped.IsPresent)
            {
                WriteVerbose("Tailscale service is not running. Starting...");
                var startResult = ExecuteAsyncTask(() => tailscaleService.StartServiceAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || startResult == null)
                {
                    return;
                }

                WriteVerbose($"Tailscale service started: {startResult.Status}");

                // Get updated status
                status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

                // Only continue if no exception occurred
                if (ProcessingException != null || status == null)
                {
                    return;
                }
            }

            // Check if Tailscale is enabled and running
            if (!status.Enabled)
            {
                ProcessingException = new Exception("Tailscale is not enabled. Use -EnableIfDisabled to enable it.");
                return;
            }

            if (!status.Running)
            {
                ProcessingException = new Exception("Tailscale service is not running. Use -StartIfStopped to start it.");
                return;
            }

            if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Connect to Tailscale network"))
            {
                return;
            }

            // Connect to Tailscale
            WriteVerbose("Connecting to Tailscale network...");
            var connectResult = ExecuteAsyncTask(() => tailscaleService.ConnectAsync(AuthKey));

            // Only continue if no exception occurred
            if (ProcessingException != null || connectResult == null)
            {
                return;
            }

            WriteVerbose($"Tailscale connection status: {connectResult.Status}");
            WriteVerbose($"Tailscale connection message: {connectResult.Message}");

            // Get updated status
            status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

            // Get interfaces
            var interfaces = ExecuteAsyncTask(() => tailscaleService.GetInterfacesAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || interfaces == null)
            {
                return;
            }

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
    }
}
