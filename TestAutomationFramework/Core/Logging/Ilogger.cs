// TestAutomationFramework.Core/Logging/ILogger.cs

namespace TestAutomationFramework.Core.Logging
{
    /// <summary>
    /// Interfaz para el sistema de logging
    /// Permite cambiar la implementación sin afectar el resto del código (SOLID - Dependency Inversion)
    /// </summary>
    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Warn(string message);
        void Error(string message);
        void Error(string message, System.Exception exception);
    }
}