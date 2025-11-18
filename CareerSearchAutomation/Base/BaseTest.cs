using SearchAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SearchAutomation.Base
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = WebDriverFactory.Create("chrome"); // Change to firefox if needed
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20)); // Explicit wait of 20 seconds

            driver.Navigate().GoToUrl("https://www.epam.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
