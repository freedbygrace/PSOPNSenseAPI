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
            // Only write warning if WarningPreference is not SilentlyContinue
            var cmdlet = _cmdlet as PSCmdlet;
            if (cmdlet != null)
            {
                // Check if WarningAction is explicitly set or if WarningPreference is not SilentlyContinue
                if (cmdlet.MyInvocation.BoundParameters.ContainsKey("WarningAction"))
                {
                    // If WarningAction is explicitly set, respect it
                    _cmdlet.WriteWarning(message);
                }
                else
                {
                    // Get the current WarningPreference
                    var warningPreference = cmdlet.SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.SilentlyContinue);

                    // Only write warning if WarningPreference is not SilentlyContinue
                    if (!ActionPreference.SilentlyContinue.Equals(warningPreference))
                    {
                        _cmdlet.WriteWarning(message);
                    }
                }
            }
            else
            {
                // Fallback for non-PSCmdlet contexts - suppress by default
                // _cmdlet.WriteWarning(message);
            }
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error(string message)
        {
            // Store the error message but don't call WriteError directly
            // The cmdlet will handle writing the error in ProcessRecord
            // Only write warning if WarningPreference is not SilentlyContinue
            var cmdlet = _cmdlet as PSCmdlet;
            if (cmdlet != null)
            {
                // Check if WarningAction is explicitly set or if WarningPreference is not SilentlyContinue
                if (cmdlet.MyInvocation.BoundParameters.ContainsKey("WarningAction"))
                {
                    // If WarningAction is explicitly set, respect it
                    _cmdlet.WriteWarning($"Error: {message}");
                }
                else
                {
                    // Get the current WarningPreference
                    var warningPreference = cmdlet.SessionState.PSVariable.GetValue("WarningPreference", ActionPreference.SilentlyContinue);

                    // Only write warning if WarningPreference is not SilentlyContinue
                    if (!ActionPreference.SilentlyContinue.Equals(warningPreference))
                    {
                        _cmdlet.WriteWarning($"Error: {message}");
                    }
                }
            }
            else
            {
                // Fallback for non-PSCmdlet contexts - suppress by default
                // _cmdlet.WriteWarning($"Error: {message}");
            }
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
