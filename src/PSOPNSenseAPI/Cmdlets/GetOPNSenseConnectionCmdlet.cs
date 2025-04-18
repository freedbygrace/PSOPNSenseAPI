using System;
using System.Management.Automation;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

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
        protected override void ProcessRecordInternal()
        {
            var sessionState = OPNSenseSessionState.Instance;
            // Create a logger
            var logger = new PowerShellLogger(this);

            if (sessionState.ApiClient == null)
            {
                // Only write warning if WarningPreference is not SilentlyContinue
                if (MyInvocation.BoundParameters.ContainsKey("WarningAction") ||
                    !ActionPreference.SilentlyContinue.Equals(SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.Continue)))
                {
                    WriteWarning("Not connected to any OPNSense firewall.");
                }

                // Return an empty connection info object
                var emptyConnectionInfo = new PSObject();
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("Server", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("Connected", false));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("ApiVersion", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("ProductName", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("ProductVersion", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("OsVersion", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("Hostname", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("SystemTime", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("Uptime", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("CpuUsage", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("MemoryUsage", null));
                emptyConnectionInfo.Properties.Add(new PSNoteProperty("LastUpdate", null));

                WriteObject(emptyConnectionInfo);
                return;
            }

            // Create and return detailed connection info object
            var connectionInfoHelper = new OPNSenseConnectionInfo(sessionState.ApiClient, logger);
            var connectionInfo = connectionInfoHelper.GetConnectionInfo();

            WriteObject(connectionInfo);
        }
    }
}

