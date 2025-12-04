using OpenQA.Selenium;
using TestAutomationFramework.Core.Enums;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// Interface for the browser factory
    /// SOLID - Dependency Inversion Principle
    /// </summary>
    public interface IBrowserFactory
    {
        IWebDriver CreateDriver(BrowserType browserType);
    }
}