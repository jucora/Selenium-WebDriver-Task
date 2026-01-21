namespace TestAutomationFramework.Core.Configuration.Ui
{
    /// <summary>
    /// Interface that defines the methods for obtaining the framework configuration
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