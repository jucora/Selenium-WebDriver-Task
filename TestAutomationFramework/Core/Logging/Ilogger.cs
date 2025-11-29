namespace TestAutomationFramework.Core.Logging
{
    /// <summary>
    /// Interface for the logging system
    /// Allows changing the implementation without affecting the rest of the code (SOLID - Dependency Inversion)
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