using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace CareerSearchAutomation.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver Create(string browser = "firefox")
        {
            IWebDriver driver = browser.ToLower() switch
            {
                "firefox" => new FirefoxDriver(),
                _ => new ChromeDriver()
            };

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
