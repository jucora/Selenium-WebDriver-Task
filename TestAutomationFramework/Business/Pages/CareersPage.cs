using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages
{
    public class CareersPage : BasePage
    {
        private readonly By StartSearchLink = 
            By.CssSelector("div[class='pinned-button'] a[class='button-body']");

        private readonly By KeywordsField =
            By.Name("search");

        private readonly By RemoteOption =
            By.XPath("//label[contains(@for,'checkbox-vacancy_type-Remote-')]");

        private readonly By LocationDropDownX =
            By.CssSelector(
                ".Dropdown_defaultOption__pvL_3.ym-disable-keys.dropdown__indicator.Dropdown_defaultOption__pvL_3.ym-disable-keys.dropdown__clear-indicator.css-1xc3v61-indicatorContainer");

        private readonly By FindButton =
            By.CssSelector("button[name='submit_search_box_button']");

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public CareersPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("CareersPage initialized");
        }

        public CareersPage ClickStartSearchLink()
        {
            Click(StartSearchLink, "Start your search here link");
            return this;
        }

        public CareersPage EnterKeyword(string keyword)
        {
            SendKeys(KeywordsField, keyword, "Keyword or job ID input");
            return this;
        }

        public CareersPage SelectRemoteOption()
        {
            Click(RemoteOption);
            return this;
        }

        public CareersPage cleanLocationFilter()
        {
            Click(LocationDropDownX);
            return this;
        }

        public JobListingsPage ClickFindButton()
        {
            Click(FindButton);
            return new JobListingsPage(Driver, Logger);
        }
    }
}
