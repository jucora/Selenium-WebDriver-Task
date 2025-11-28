using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages
{
    public class CareersPage : BasePage
    {
        private static string selectedLocation = string.Empty;

        private readonly By KeywordsField =
            By.Id("new_form_job_search-keyword");

        private readonly By LocationField =
            By.XPath($"//li[contains(@class, 'select2-results__option') and normalize-space(text())='{selectedLocation}']");

        private readonly By LocationDropdown =
            By.CssSelector(".select2-selection__rendered");

        private readonly By RemoteOption =
            By.CssSelector("label[for='id-93414a92-598f-316d-b965-9eb0dfefa42d-remote']");

        private readonly By FindButton =
            By.CssSelector("button[type='submit']");

        private static void SetLocation(string location)
        {
            selectedLocation = location;
        }

        private const string Location = "All Locations";

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public CareersPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("CareersPage initialized");
        }

        public CareersPage EnterKeyword(string keyword)
        {
            SendKeys(KeywordsField, keyword, "Keyword or job ID input");
            return this;
        }

        public CareersPage SelectLocation()
        {
            SetLocation(Location);
           
            selectedLocation = GetText(LocationDropdown); // revisar

            if (!selectedLocation.Contains(Location, StringComparison.OrdinalIgnoreCase))
            {
                Click(LocationDropdown);
                Click(LocationField);
            }

            return this;
        }

        public CareersPage SelectRemoteOption()
        {
            Click(RemoteOption);
            return this;
        }

        public JobListingsPage ClickFindButton()
        {
            Click(FindButton);
            return new JobListingsPage(Driver, Logger);
        }
    }
}
