using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Enums;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.WebDriver;

namespace TestAutomationFramework.Tests.Hooks
{
    [Binding]
    public class ReqnrollDriverHooks
    {
        private readonly IObjectContainer Container;
        private IWebDriver Driver;
        private ILogger Logger;
        private IConfiguration Config;

        //
        protected NavbarComponent navbar = null!;
        protected CookiesComponent cookies = null!;
        //

        public ReqnrollDriverHooks(IObjectContainer container)
        {
            Container = container;
        }

        [BeforeScenario]
        public void Setup()
        {
            // ======== CONFIGURATION ========
            Config = ConfigurationManager.Instance;
            Container.RegisterInstanceAs(Config);

            // ======== LOGGER ========
            Logger = new Logger("ReqnrollTest");
            Container.RegisterInstanceAs(Logger);

            // ======== WEBDRIVER ========
            BrowserFactory browserFactory = new BrowserFactory(Logger);
            Driver = browserFactory.CreateDriver(BrowserType.Chrome);
            Container.RegisterInstanceAs(Driver);

            Logger.Info("WebDriver initialized for Reqnroll scenario.");

            //
            Driver.Navigate().GoToUrl("https://www.epam.com/");

            Driver.Manage().Window.Maximize();

            cookies = new CookiesComponent(Driver, Logger);
            cookies.AcceptCookiesIfPresent();
        }

        [AfterScenario]
        public void TearDown()
        {
            Driver?.Quit();
        }
    }
}
