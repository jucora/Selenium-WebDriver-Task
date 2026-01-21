
using OpenQA.Selenium;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

namespace TestAutomationFramework.Tests.Context
{

    public class UiTestContext
    {
        public CookiesComponent Cookies { get; set; } = null!;
        public IWebDriver Driver { get; set; } = null!;
        public ILogger Logger { get; set; } = null!;
        public NavbarComponent Navbar { get; set; } = null!;
        public WaitHelper WaitHelper { get; set; } = null!;
    }
}
