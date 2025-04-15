using System;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Restarts an OPNSense firewall.</para>
    /// <para type="description">The Restart-OPNSenseFirewall cmdlet restarts an OPNSense firewall and optionally waits for it to come back online.</para>
    /// <example>
    ///     <para>Example 1: Restart a firewall</para>
    ///     <code>Restart-OPNSenseFirewall</code>
    ///     <para>This example restarts the OPNSense firewall.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Restart a firewall and wait for it to come back online</para>
    ///     <code>Restart-OPNSenseFirewall -Wait -Timeout 300</code>
    ///     <para>This example restarts the OPNSense firewall and waits up to 5 minutes for it to come back online.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Restart, "OPNSenseFirewall", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [OutputType(typeof(void))]
    public class RestartOPNSenseFirewallCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// <para type="description">Suppresses the confirmation prompt.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// <para type="description">Waits for the firewall to come back online after restarting.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// <para type="description">The timeout in seconds to wait for the firewall to come back online.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 3600)]
        public int Timeout { get; set; } = 180;

        /// <summary>
        /// <para type="description">The interval in seconds between connection attempts.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [ValidateRange(1, 60)]
        public int Interval { get; set; } = 5;

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecordInternal()
        {
            if (!Force.IsPresent && !ShouldProcess("OPNSense firewall", "Restart"))
            {
                return;
            }

            var systemService = new SystemService(ApiClient, Logger);

            // Store connection information for reconnection
            string baseUrl = OPNSenseSession.BaseUrl;
            string apiKey = null;
            string apiSecret = null;
            bool skipCertificateCheck = false;

            // Extract connection information from the current session
            if (ApiClient is OPNSenseApiClient client)
            {
                baseUrl = client.BaseUrl;
                apiKey = client.ApiKey;
                apiSecret = client.ApiSecret;
                skipCertificateCheck = client.SkipCertificateCheck;
            }

            // If we couldn't get the credentials, we can't reconnect
            if (Wait.IsPresent && (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret)))
            {
                WriteWarning("Could not retrieve API credentials from the current session. Will not attempt to reconnect after restart.");
                Wait = false;
            }

            // Restart the firewall
            var result = ExecuteAsyncTask(() => systemService.RebootAsync());

            // Only continue if no exception occurred
            if (ProcessingException != null || result == null)
            {
                return;
            }

            WriteVerbose($"Firewall restart initiated: {result.Status}");
            WriteObject("Firewall restart initiated. The firewall is now restarting.");

            // Wait for the firewall to come back online if requested
            if (Wait.IsPresent)
            {
                WriteVerbose($"Waiting for firewall to come back online (timeout: {Timeout} seconds, interval: {Interval} seconds)");
                WriteObject("Waiting for firewall to come back online...");

                // Disconnect the current session
                OPNSenseSession.Current?.Dispose();
                OPNSenseSession.Current = null;

                // Wait a bit for the firewall to start rebooting
                Thread.Sleep(5000);

                // Try to reconnect
                DateTime startTime = DateTime.Now;
                bool reconnected = false;

                while (DateTime.Now - startTime < TimeSpan.FromSeconds(Timeout))
                {
                    try
                    {
                        WriteVerbose($"Attempting to reconnect to {baseUrl}...");

                        // Create a new logger
                        var logger = new PowerShellLogger(this);

                        // Create a new API client
                        var newClient = new OPNSenseApiClient(baseUrl, apiKey, apiSecret, skipCertificateCheck, logger);

                        // Try to get the system status
                        var statusService = new SystemService(newClient, logger);

                        // We need to handle this specially because we're using a new client
                        // and can't use ExecuteAsyncTask which uses the existing client
                        try
                        {
                            // Use ConfigureAwait(false) to avoid deadlocks
                            var statusResult = statusService.GetStatusAsync().ConfigureAwait(false).GetAwaiter().GetResult();

                            // If we get here, the firewall is back online
                            WriteVerbose("Successfully reconnected to the firewall");
                            WriteObject($"Firewall is back online. Uptime: {statusResult.Uptime}");
                        }
                        catch (Exception innerEx)
                        {
                            // Just log and continue the retry loop
                            WriteVerbose($"Status check failed: {innerEx.Message}");
                            throw; // Re-throw to be caught by the outer catch
                        }

                        // Set the new session
                        OPNSenseSession.Current = newClient;
                        reconnected = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        WriteVerbose($"Reconnection attempt failed: {ex.Message}");
                        Thread.Sleep(Interval * 1000);
                    }
                }

                if (!reconnected)
                {
                    WriteWarning($"Timed out waiting for firewall to come back online after {Timeout} seconds");
                }
            }
        }
    }
}
