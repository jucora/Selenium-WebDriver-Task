using NUnit.Framework;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Tests;

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

            //GlobalSearchValidator.ValidateLinkTexts(searchResults, keyword);
            bool allContainKeyword = searchResults.All(text =>
                text.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            );

            Assert.That(allContainKeyword, Is.False, // should be true?
                $"Not all links contain the word '{keyword}'.\n" +
                $"Texts: {string.Join(", ", searchResults)}");
        }
    }
}
