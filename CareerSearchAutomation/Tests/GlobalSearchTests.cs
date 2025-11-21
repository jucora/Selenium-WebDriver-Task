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
            navbar
                .ClickMagnifierIcon()
                .EnterSearchKeyword(keyword);

            SearchPage searchPage = navbar.ClickFindButton();
            var searchResults = searchPage
                .GetSearchResults(keyword);

            GlobalSearchValidator
                .ValidateLinkTexts(searchResults, keyword);
        }
    }
}
