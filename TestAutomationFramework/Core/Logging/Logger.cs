// TestAutomationFramework.Core/Logging/Logger.cs

using NLog;

namespace TestAutomationFramework.Core.Logging
{
    /// <summary>
    /// Implementación del logger usando NLog
    /// Soporta logging a consola y archivo simultáneamente
    /// </summary>
    public class Logger : ILogger
    {
        private readonly NLog.Logger _logger;

        /// <summary>
        /// Constructor que inicializa el logger de NLog
        /// </summary>
        public Logger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Constructor que permite especificar el nombre del logger
        /// Útil para identificar de qué clase proviene el log
        /// </summary>
        public Logger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Registra mensaje informativo
        /// Usado para flujo normal de la aplicación
        /// </summary>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Registra mensaje de debug
        /// Usado para información detallada durante desarrollo
        /// </summary>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Registra advertencia
        /// Usado para situaciones que no son errores pero requieren atención
        /// </summary>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Registra error sin excepción
        /// </summary>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Registra error con excepción completa
        /// Incluye stack trace para debugging
        /// </summary>
        public void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }
    }
}