using OpenQA.Selenium;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Components
{
    public class CookiesComponent : BasePage
    {
        private static readonly By AcceptCookiesButton =
            By.CssSelector("button[id=onetrust-accept-btn-handler]");

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public CookiesComponent(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("CookiesComponent initialized");
        }

        public void AcceptCookiesIfPresent()
        {
            try
            {
                Click(AcceptCookiesButton);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Cookies acceptance button not found; proceeding without accepting cookies.");
            }
        }
    }
}
