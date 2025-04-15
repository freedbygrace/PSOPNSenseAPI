using System.Management.Automation;

namespace PSOPNSenseAPI.Logging
{
    /// <summary>
    /// Logger that uses PowerShell cmdlets for logging
    /// </summary>
    public class PowerShellLogger : ILogger
    {
        private readonly PSCmdlet _cmdlet;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellLogger"/> class
        /// </summary>
        /// <param name="cmdlet">The PowerShell cmdlet to use for logging</param>
        public PowerShellLogger(PSCmdlet cmdlet)
        {
            _cmdlet = cmdlet;
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Debug(string message)
        {
            _cmdlet.WriteDebug(message);
        }

        /// <summary>
        /// Logs an informational message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Information(string message)
        {
            _cmdlet.WriteVerbose(message);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Warning(string message)
        {
            _cmdlet.WriteWarning(message);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error(string message)
        {
            _cmdlet.WriteError(new ErrorRecord(
                new System.Exception(message),
                "OPNSenseApiError",
                ErrorCategory.NotSpecified,
                null));
        }

        /// <summary>
        /// Logs a verbose message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Verbose(string message)
        {
            _cmdlet.WriteVerbose(message);
        }
    }
}
