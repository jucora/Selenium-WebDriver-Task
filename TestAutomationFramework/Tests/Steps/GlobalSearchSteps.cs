using NUnit.Framework;
using Reqnroll;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Tests.Context;

namespace TestAutomationFramework.Tests.Steps
{
    [Binding]
    public class GlobalSearchSteps
    {
        private SearchPage searchPage; 
        private IEnumerable<string> searchResults = null!;
        private readonly UiTestContext context;

        public GlobalSearchSteps(UiTestContext context)
        {
            this.context = context;
        }

        [Given("the user opens the global search")]
        public void GivenTheUserOpensTheGlobalSearch()
        {
            context.Navbar.ClickMagnifierIcon();
        }

        [When("the user searches for \"(.*)\"")]
        public void WhenTheUserSearchesFor(string keyword)
        {
            context.Navbar.EnterSearchKeyword(keyword);
            searchPage = context.Navbar.ClickFindButton();
        }

        [Then("all search results should contain \"(.*)\"")]
        public void ThenAllSearchResultsShouldContain(string keyword)
        {
            searchResults = searchPage.GetSearchResults(keyword);

            bool allContainKeyword = searchResults.All(text =>
                text.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            );

            Assert.That(allContainKeyword, Is.False,
                $"Not all links contain the word '{keyword}'.\n" +
                $"Texts: {string.Join(", ", searchResults)}");
        }
    }
}
