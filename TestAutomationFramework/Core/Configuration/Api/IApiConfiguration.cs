namespace TestAutomationFramework.Core.Configuration.Api
{
    /// <summary>
    /// Interface that defines the methods for obtaining the framework configuration
    /// </summary>
    public interface IApiConfiguration
    {
        string GetBaseUrl();
        string GetTestUser();
    }
}