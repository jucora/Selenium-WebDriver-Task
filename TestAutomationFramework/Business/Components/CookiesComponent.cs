using OpenQA.Selenium;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

namespace TestAutomationFramework.Business.Components
{
    public class CookiesComponent : BasePage
    {
        private static readonly By AcceptCookiesButton =
            By.Id("onetrust-accept-btn-handler");

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
                // Get button
                var button = WaitHelper.WaitForElementExists(AcceptCookiesButton);

                // Hide overlay that blocks clicking in Firefox
                ExecuteJavaScript(
                    "const e = document.getElementById('onetrust-group-container'); if(e) e.style.display='none';");

                // Scroll to the button
                ExecuteJavaScript("arguments[0].scrollIntoView(true);", button);

                // Try normal click
                try
                {
                    WaitHelper.WaitForElementClickable(AcceptCookiesButton).Click();
                }
                catch
                {
                    // If it fails, click with JavaScript
                    ExecuteJavaScript("arguments[0].click();", button);
                }
            }
            catch
            {
                // If there is no banner, continue without error
                Logger.Info("No se encontró el banner de cookies.");
            }
        }
    }
}
