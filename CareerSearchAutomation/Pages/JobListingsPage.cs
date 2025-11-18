using SearchAutomation.Locators;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SearchAutomation.Pages
{
    public class JobListingsPage : BasePage
    {
        public JobListingsPage(IWebDriver driver) : base(driver)
        {
        }

        public void SelectViewAndApplyFromLastResult()
        {
            ClickLast(JobListingsPageLocators.JobResults);
        }
    }
}
