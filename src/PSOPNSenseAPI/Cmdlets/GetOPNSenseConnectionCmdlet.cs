using System;
using System.Management.Automation;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// <para type="synopsis">Gets information about the current OPNSense connection.</para>
    /// <para type="description">The Get-OPNSenseConnection cmdlet retrieves information about the current connection to an OPNSense firewall.</para>
    /// <example>
    ///     <para>Example 1: Get the current connection information</para>
    ///     <code>Get-OPNSenseConnection</code>
    ///     <para>This example retrieves information about the current connection to an OPNSense firewall.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "OPNSenseConnection")]
    [OutputType(typeof(PSObject))]
    public class GetOPNSenseConnectionCmdlet : OPNSenseBaseCmdlet
    {
        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void BeginProcessing()
        {
            // Override the base implementation to avoid checking for connection
            // since this cmdlet is used to check the connection status
            base.BeginProcessing();
            Logger = new PowerShellLogger(this);
        }

        protected override void ProcessRecordInternal()
        {
            if (!OPNSenseSession.IsConnected)
            {
                WriteWarning("Not connected to any OPNSense firewall.");
                return;
            }

            var connectionInfo = new PSObject();
            connectionInfo.Properties.Add(new PSNoteProperty("Server", OPNSenseSession.BaseUrl));
            connectionInfo.Properties.Add(new PSNoteProperty("Connected", OPNSenseSession.IsConnected));

            WriteObject(connectionInfo);
        }
    }
}
