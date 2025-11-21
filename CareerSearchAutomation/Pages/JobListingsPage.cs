using OpenQA.Selenium;
using SearchAutomation.Locators.Pages;

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
