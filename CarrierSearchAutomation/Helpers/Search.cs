using CarrierSearchAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Carrier_Search_Automation.Helpers
{
    public class Search
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = WebDriverFactory.Create("chrome"); // Change to firefox if needed
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Explicit wait of 10 seconds
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    
    protected void navigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}
