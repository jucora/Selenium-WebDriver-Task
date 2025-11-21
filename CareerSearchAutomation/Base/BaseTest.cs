using CareerSearchAutomation.Core.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SearchAutomation.Components;
using SearchAutomation.Core;

namespace SearchAutomation.Base
{
    public abstract class BaseTest
    {
        protected IWebDriver driver = null!;
        protected WebDriverWait wait = null!;
        protected NavbarComponent navbar = null!;
        protected CookiesComponent cookies = null!;

        [SetUp]
        public void Setup()
        {
            driver = WebDriverFactory.Create(BrowserType.Chrome); // Change to firefox if needed
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); // Explicit wait of 20 seconds

            navbar = new NavbarComponent(driver);
            cookies = new CookiesComponent(driver);

            driver.Navigate().GoToUrl("https://www.epam.com/");
            cookies.AcceptCookiesIfPresent();
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
