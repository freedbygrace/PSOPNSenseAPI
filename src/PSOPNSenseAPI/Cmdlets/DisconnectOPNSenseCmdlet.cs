using System;
using System.Management.Automation;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Disconnects from an OPNSense firewall.</para>
    /// <para type="description">The Disconnect-OPNSense cmdlet closes the connection to an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Disconnect from an OPNSense firewall</para>
    ///     <code>Disconnect-OPNSense</code>
    ///     <para>This example disconnects from the currently connected OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommunications.Disconnect, "OPNSense")]
    [OutputType(typeof(void))]
    public class DisconnectOPNSenseCmdlet : PSCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                if (!OPNSenseSession.IsConnected)
                {
                    WriteWarning("Not connected to any OPNSense firewall.");
                    return;
                }

                var baseUrl = OPNSenseSession.BaseUrl;
                OPNSenseSession.Current?.Dispose();
                OPNSenseSession.Current = null;

                WriteVerbose($"Disconnected from OPNSense firewall at {baseUrl}");
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "DisconnectionFailed",
                    ErrorCategory.ConnectionError,
                    null));
            }
        }
    }
}
