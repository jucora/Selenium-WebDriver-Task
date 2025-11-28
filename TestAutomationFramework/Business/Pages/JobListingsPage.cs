using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages
{
    public class JobListingsPage : BasePage
    {
        private static readonly By JobResults =
            By.XPath("//a[contains(text(),'View and apply')]");

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public JobListingsPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("JobListingsPage initialized");
        }

        public void SelectViewAndApplyFromLastResult()
        {
            ClickLast(JobResults);
        }
    }
}
