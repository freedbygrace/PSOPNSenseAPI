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

            // Get current status
            var status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

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
            var disconnectResult = ExecuteAsyncTask(() => tailscaleService.DisconnectAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || disconnectResult == null)
            {
                return;
            }

            WriteVerbose($"Tailscale disconnection status: {disconnectResult.Status}");

            // Get updated status
            status = ExecuteAsyncTask(() => tailscaleService.GetStatusAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || status == null)
            {
                return;
            }

            // Create result object
            var result = new PSObject();
            result.Properties.Add(new PSNoteProperty("Status", status.Status));
            result.Properties.Add(new PSNoteProperty("Running", status.Running));
            result.Properties.Add(new PSNoteProperty("Enabled", status.Enabled));
            result.Properties.Add(new PSNoteProperty("DisconnectionStatus", disconnectResult.Status));

            WriteObject(result);
        }
    }
}
