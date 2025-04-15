using System;
using System.Management.Automation;
using System.Threading.Tasks;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Disconnects an OPNSense firewall from a Tailscale network.</para>
    /// <para type="description">The Disconnect-OPNSenseTailscale cmdlet disconnects an OPNSense firewall from a Tailscale network.</para>
    /// <example>
    ///     <para>Example 1: Disconnect from Tailscale network</para>
    ///     <code>Disconnect-OPNSenseTailscale</code>
    ///     <para>This example disconnects the OPNSense firewall from the Tailscale network.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommunications.Disconnect, "OPNSenseTailscale", SupportsShouldProcess = true)]
    [OutputType(typeof(PSObject))]
    public class DisconnectOPNSenseTailscaleCmdlet : OPNSenseBaseCmdlet
    {
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
                    WriteWarning("Tailscale plugin is not installed on the OPNSense firewall.");
                    return;
                }

                // Get current status
                var statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                var status = statusTask.GetAwaiter().GetResult();

                if (!status.Running)
                {
                    WriteWarning("Tailscale service is not running.");
                    return;
                }

                if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Disconnect from Tailscale network"))
                {
                    return;
                }

                // Disconnect from Tailscale
                WriteVerbose("Disconnecting from Tailscale network...");
                var disconnectTask = Task.Run(async () => await tailscaleService.DisconnectAsync());
                var disconnectResult = disconnectTask.GetAwaiter().GetResult();

                WriteVerbose($"Tailscale disconnection status: {disconnectResult.Status}");

                // Get updated status
                statusTask = Task.Run(async () => await tailscaleService.GetStatusAsync());
                status = statusTask.GetAwaiter().GetResult();

                // Create result object
                var result = new PSObject();
                result.Properties.Add(new PSNoteProperty("Status", status.Status));
                result.Properties.Add(new PSNoteProperty("Running", status.Running));
                result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
                result.Properties.Add(new PSNoteProperty("DisconnectionStatus", disconnectResult.Status));

                WriteObject(result);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
