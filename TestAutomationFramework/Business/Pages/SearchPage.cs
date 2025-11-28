using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages
{
    public class SearchPage : BasePage
    {
        private static readonly By SearchResultLinks =
            By.CssSelector(".search-results__title a");

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public SearchPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("SearchPage initialized");
        }
        public List<string> GetSearchResults(string keyword)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.GetExplicitWait()));
            var links = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                SearchResultLinks
            ));

            return links.Select(link => link.Text.Trim()).ToList();
        }
    }
}
