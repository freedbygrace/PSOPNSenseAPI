namespace PSOPNSenseAPI.Logging
{
    /// <summary>
    /// A logger that does nothing
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Debug(string message)
        {
            // Do nothing
        }

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Information(string message)
        {
            // Do nothing
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Warning(string message)
        {
            // Do nothing
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error(string message)
        {
            // Do nothing
        }

        /// <summary>
        /// Logs a verbose message
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Verbose(string message)
        {
            // Do nothing
        }
    }
}
