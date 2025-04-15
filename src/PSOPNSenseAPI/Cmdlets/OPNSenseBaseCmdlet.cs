using System;
using System.Management.Automation;
using PSOPNSenseAPI.Logging;
using PSOPNSenseAPI.Models;
using PSOPNSenseAPI.Services;

namespace PSOPNSenseAPI.Cmdlets
{
    /// <summary>
    /// Base class for all OPNSense cmdlets
    /// </summary>
    public abstract class OPNSenseBaseCmdlet : PSCmdlet
    {
        /// <summary>
        /// Gets the API client
        /// </summary>
        protected OPNSenseApiClient ApiClient => OPNSenseSession.Current;

        /// <summary>
        /// Gets the logger
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        /// Initializes the cmdlet
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            Logger = new PowerShellLogger(this);

            if (!OPNSenseSession.IsConnected)
            {
                ThrowTerminatingError(new ErrorRecord(
                    new InvalidOperationException("Not connected to an OPNSense firewall. Use Connect-OPNSense first."),
                    "NotConnected",
                    ErrorCategory.ConnectionError,
                    null));
            }
        }

        /// <summary>
        /// Handles exceptions
        /// </summary>
        /// <param name="ex">The exception to handle</param>
        protected void HandleException(Exception ex)
        {
            if (ex is OPNSenseApiException apiEx)
            {
                WriteError(new ErrorRecord(
                    apiEx,
                    "OPNSenseApiError",
                    ErrorCategory.InvalidOperation,
                    null));
            }
            else
            {
                WriteError(new ErrorRecord(
                    ex,
                    "OPNSenseError",
                    ErrorCategory.NotSpecified,
                    null));
            }
        }
    }
}
