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
    [CmdletBinding()]
    public abstract class OPNSenseBaseCmdlet : PSCmdlet
    {
        /// <summary>
        /// Begins the processing of the cmdlet
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            // Set default WarningPreference to SilentlyContinue if not explicitly set
            if (!MyInvocation.BoundParameters.ContainsKey("WarningAction"))
            {
                // Store the original preference
                var originalWarningPreference = SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.Continue);

                // Only set it if it's not already SilentlyContinue
                if (!ActionPreference.SilentlyContinue.Equals(originalWarningPreference))
                {
                    // Set the preference for this cmdlet execution
                    SessionState.PSVariable.Set("WarningPreference", ActionPreference.SilentlyContinue);
                }
            }

            Logger = new PowerShellLogger(this);

            // Skip connection check for Connect-OPNSense and Get-OPNSenseConnection cmdlets
            var cmdletType = GetType();
            var sessionState = OPNSenseSessionState.Instance;
            if (cmdletType != typeof(ConnectOPNSenseCmdlet) && cmdletType != typeof(GetOPNSenseConnectionCmdlet) &&
                (sessionState.ApiClient == null || !sessionState.ApiClient.IsConnected))
            {
                ThrowTerminatingError(new ErrorRecord(
                    new InvalidOperationException("Not connected to an OPNSense firewall. Use Connect-OPNSense first."),
                    "NotConnected",
                    ErrorCategory.ConnectionError,
                    null));
            }
        }

        /// <summary>
        /// Processes the cmdlet
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                ProcessRecordInternal();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                // Process any exceptions that occurred
                ProcessExceptions();
            }
        }

        /// <summary>
        /// Internal method for processing the cmdlet
        /// </summary>
        protected virtual void ProcessRecordInternal()
        {
            // Override in derived classes
        }

        /// <summary>
        /// Ends the processing of the cmdlet
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
        /// <summary>
        /// Gets the API client
        /// </summary>
        protected OPNSenseApiClient ApiClient => OPNSenseSessionState.Instance.ApiClient;

        /// <summary>
        /// Gets the logger
        /// </summary>
        protected ILogger Logger { get; set; }



        /// <summary>
        /// Exception to be processed in ProcessRecord
        /// </summary>
        protected Exception ProcessingException { get; set; }

        /// <summary>
        /// Handles exceptions by storing them for later processing
        /// </summary>
        /// <param name="ex">The exception to handle</param>
        protected void HandleException(Exception ex)
        {
            // Store the exception to be processed in ProcessRecord
            ProcessingException = ex;

            // Only write warning if WarningPreference is not SilentlyContinue
            if (MyInvocation.BoundParameters.ContainsKey("WarningAction") ||
                !ActionPreference.SilentlyContinue.Equals(SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.Continue)))
            {
                WriteWarning($"Error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Processes any stored exceptions
        /// </summary>
        protected void ProcessExceptions()
        {
            if (ProcessingException != null)
            {
                if (ProcessingException is OPNSenseApiException apiEx)
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
                        ProcessingException,
                        "OPNSenseError",
                        ErrorCategory.NotSpecified,
                        null));
                }

                // Clear the exception after processing
                ProcessingException = null;
            }
        }

        /// <summary>
        /// Safely executes a function and handles exceptions
        /// </summary>
        /// <typeparam name="T">The return type of the function</typeparam>
        /// <param name="func">The function to execute</param>
        /// <returns>The result of the function</returns>
        protected T ExecuteSafely<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                // Store the exception to be processed in ProcessRecord
                ProcessingException = ex;

                // Only write warning if WarningPreference is not SilentlyContinue
                if (MyInvocation.BoundParameters.ContainsKey("WarningAction") ||
                    !ActionPreference.SilentlyContinue.Equals(SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.Continue)))
                {
                    WriteWarning($"Error occurred: {ex.Message}");
                }
                return default;
            }
        }

        /// <summary>
        /// Safely executes an action and handles exceptions
        /// </summary>
        /// <param name="action">The action to execute</param>
        protected void ExecuteSafely(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // Store the exception to be processed in ProcessRecord
                ProcessingException = ex;

                // Only write warning if WarningPreference is not SilentlyContinue
                if (MyInvocation.BoundParameters.ContainsKey("WarningAction") ||
                    !ActionPreference.SilentlyContinue.Equals(SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.Continue)))
                {
                    WriteWarning($"Error occurred: {ex.Message}");
                }
            }
        }
    }
}

