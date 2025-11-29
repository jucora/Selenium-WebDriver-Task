using NLog;

namespace TestAutomationFramework.Core.Logging
{
    /// <summary>
    /// Logger implementation using NLog
    /// Supports logging to console and file simultaneously
    /// </summary>
    public class Logger : ILogger
    {
        private readonly NLog.Logger logger;

        /// <summary>
        /// Constructor that initializes the NLog logger
        /// </summary>
        public Logger()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Constructor that allows specifying the logger name
        /// Useful for identifying which class the log originates from
        /// </summary>
        public Logger(string loggerName)
        {
            logger = LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Logs an informational message
        /// Used for the normal application flow
        /// </summary>
        public void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Logs a debug message
        /// Used for detailed information during development
        /// </summary>
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Logs a warning
        /// Used for situations that are not errors but require attention
        /// </summary>
        public void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Logs an error without an exception
        /// </summary>
        public void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Logs an error with the full exception
        /// Includes stack trace for debugging
        /// </summary>
        public void Error(string message, Exception exception)
        {
            logger.Error(exception, message);
        }
    }
}