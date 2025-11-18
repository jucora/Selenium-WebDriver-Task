using SearchAutomation.Locators;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SearchAutomation.Pages
{
    public class SearchPage : BasePage
    {
        public SearchPage(IWebDriver driver) : base(driver)
        {
        }
        public List<string> GetSearchResults(string keyword)
        {
            var links = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                JobListingsPageLocators.SearchResultLinks
            ));

            return links.Select(link => link.Text.Trim()).ToList();
        }
    }
}
