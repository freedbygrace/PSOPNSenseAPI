using System;
using System.Management.Automation;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// Base class for OPNSense cmdlets that use the new API client
    /// </summary>
    public abstract class OPNSenseCmdlet : PSCmdlet
    {
        /// <summary>
        /// Gets the session state
        /// </summary>
        protected new OPNSenseSessionState SessionState => OPNSenseSessionState.Instance;

        /// <summary>
        /// Initializes the cmdlet
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (SessionState.ApiClient == null)
            {
                ThrowTerminatingError(new ErrorRecord(
                    new InvalidOperationException("Not connected to an OPNSense firewall. Use Connect-OPNSense first."),
                    "NotConnected",
                    ErrorCategory.ConnectionError,
                    null));
            }
        }
    }
}
