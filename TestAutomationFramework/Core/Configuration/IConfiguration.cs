// TestAutomationFramework.Core/Configuration/IConfiguration.cs

namespace TestAutomationFramework.Core.Configuration
{
    /// <summary>
    /// Interfaz que define los métodos para obtener configuración del framework
    /// </summary>
    public interface IConfiguration
    {
        string GetBrowser();
        string GetEnvironment();
        string GetBaseUrl();
        int GetImplicitWait();
        int GetExplicitWait();
        string GetLogLevel();
        string GetScreenshotPath();
    }
}