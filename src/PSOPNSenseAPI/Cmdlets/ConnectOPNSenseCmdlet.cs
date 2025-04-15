using System;
using System.Management.Automation;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Connects to an OPNSense firewall.</para>
    /// <para type="description">The Connect-OPNSense cmdlet establishes a connection to an OPNSense firewall using the API.</para>
    /// <para type="description">This cmdlet must be called before using any other cmdlets in the module.</para>
    /// <example>
    ///     <para>Example 1: Connect to an OPNSense firewall</para>
    ///     <code>Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret"</code>
    ///     <para>This example connects to an OPNSense firewall at the specified URL using the provided API credentials.</para>
    /// </example>
    /// <example>
    ///     <para>Example 2: Connect to an OPNSense firewall with certificate validation disabled</para>
    ///     <code>Connect-OPNSense -Server "https://firewall.example.com" -ApiKey "your_api_key" -ApiSecret "your_api_secret" -SkipCertificateCheck</code>
    ///     <para>This example connects to an OPNSense firewall at the specified URL using the provided API credentials, skipping certificate validation.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "OPNSense")]
    [OutputType(typeof(void))]
    public class ConnectOPNSenseCmdlet : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The URL of the OPNSense firewall.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Server { get; set; }

        /// <summary>
        /// <para type="description">The API key for authentication.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string ApiKey { get; set; }

        /// <summary>
        /// <para type="description">The API secret for authentication.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        [ValidateNotNullOrEmpty]
        public string ApiSecret { get; set; }

        /// <summary>
        /// <para type="description">Skips certificate validation when connecting to the OPNSense firewall.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter SkipCertificateCheck { get; set; }

        /// <summary>
        /// <para type="description">Forces a new connection even if one already exists.</para>
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
                // Check if already connected
                if (OPNSenseSession.IsConnected && !Force.IsPresent)
                {
                    WriteWarning($"Already connected to {OPNSenseSession.BaseUrl}. Use -Force to reconnect.");
                    return;
                }

                // Dispose existing connection if there is one
                OPNSenseSession.Current?.Dispose();

                // Create a new logger
                var logger = new PowerShellLogger(this);

                // Create a new API client
                var client = new OPNSenseApiClient(Server, ApiKey, ApiSecret, SkipCertificateCheck.IsPresent, logger);

                // Set the current session
                OPNSenseSession.Current = client;

                WriteVerbose($"Connected to OPNSense firewall at {Server}");
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "ConnectionFailed",
                    ErrorCategory.ConnectionError,
                    Server));
            }
        }
    }
}
