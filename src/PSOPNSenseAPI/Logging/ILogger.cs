namespace PSOPNSenseAPI.Logging
{
    /// <summary>
    /// Interface for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(string message);

        /// <summary>
        /// Logs an informational message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Information(string message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warning(string message);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(string message);

        /// <summary>
        /// Logs a verbose message
        /// </summary>
        /// <param name="message">The message to log</param>
        void Verbose(string message);
    }
}
