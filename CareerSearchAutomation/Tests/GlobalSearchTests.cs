using NUnit.Framework;
using SearchAutomation.Base;
using SearchAutomation.Pages;
using SearchAutomation.Validators;

namespace SearchAutomation.Tests
{
    [TestFixture]
    public class GlobalSearchTests : BaseTest
    {
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void ValidateUserCanSearchBasedOnCriteria(string keyword)
        {
            IndexPage indexPage = new IndexPage(driver);
            indexPage
                .AcceptCookies()
                .ClickMagnifierIcon()
                .EnterSearchKeyword(keyword);

            SearchPage searchPage = indexPage.ClickFindButton();
            var searchResults = searchPage
                .GetSearchResults(keyword);

            GlobalSearchValidator
                .ValidateLinkTexts(searchResults, keyword);
        }
    }
}
