
// TestAutomationFramework.Core/WebDriver/IBrowserFactory.cs

using OpenQA.Selenium;
using TestAutomationFramework.Core.Enums;

namespace TestAutomationFramework.Core.WebDriver
{
    /// <summary>
    /// Interfaz para la fábrica de navegadores
    /// SOLID - Dependency Inversion Principle
    /// </summary>
    public interface IBrowserFactory
    {
        IWebDriver CreateDriver(BrowserType browserType);
    }
}