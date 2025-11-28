using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages
{
    public class AboutPage : BasePage
    {
        private static readonly By DownloadButton =
            By.CssSelector("div[class='colctrl__holder'] span[class='button__content button__content--desktop']");

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public AboutPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("AboutPage initialized");
        }

        public void ClickDownloadButton()
        {
            Click(DownloadButton, "Download Button");
        }
    }
}
