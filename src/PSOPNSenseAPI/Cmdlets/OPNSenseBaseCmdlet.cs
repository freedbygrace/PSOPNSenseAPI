using System;
using System.Management.Automation;
using System.Threading.Tasks;
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
        /// Begins the processing of the cmdlet
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
        protected OPNSenseApiClient ApiClient => OPNSenseSession.Current;

        /// <summary>
        /// Gets the logger
        /// </summary>
        protected ILogger Logger { get; private set; }



        /// <summary>
        /// Exception to be processed in ProcessRecord
        /// </summary>
        protected Exception ProcessingException { get; private set; }

        /// <summary>
        /// Handles exceptions by storing them for later processing
        /// </summary>
        /// <param name="ex">The exception to handle</param>
        protected void HandleException(Exception ex)
        {
            // Store the exception to be processed in ProcessRecord
            ProcessingException = ex;
            WriteWarning($"Error occurred: {ex.Message}");
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
        /// Executes an async task synchronously and safely
        /// </summary>
        /// <typeparam name="T">The return type of the task</typeparam>
        /// <param name="asyncFunc">The async function to execute</param>
        /// <returns>The result of the async function</returns>
        protected T ExecuteAsyncTask<T>(Func<Task<T>> asyncFunc)
        {
            try
            {
                // Use ConfigureAwait(false) to avoid deadlocks
                return asyncFunc().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Store the exception to be processed in ProcessRecord
                ProcessingException = ex;
                WriteWarning($"Error occurred: {ex.Message}");
                return default;
            }
        }

        /// <summary>
        /// Executes an async task synchronously and safely (no return value)
        /// </summary>
        /// <param name="asyncAction">The async action to execute</param>
        protected void ExecuteAsyncTask(Func<Task> asyncAction)
        {
            try
            {
                // Use ConfigureAwait(false) to avoid deadlocks
                asyncAction().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Store the exception to be processed in ProcessRecord
                ProcessingException = ex;
                WriteWarning($"Error occurred: {ex.Message}");
            }
        }
    }
}
