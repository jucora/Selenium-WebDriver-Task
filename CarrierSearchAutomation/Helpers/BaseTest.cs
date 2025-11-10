using CarrierSearchAutomation.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Base_Search_Automation.Helpers
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        private const string BaseUrl = "https://www.epam.com/";

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
    
    protected void NavigateToUrl()
        {
            driver.Navigate().GoToUrl(BaseUrl);
        }
    }
}
