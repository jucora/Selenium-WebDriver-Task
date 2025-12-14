using NUnit.Framework;
using Reqnroll;
using TestAutomationFramework.Business.Pages;

[Binding]
public class GlobalSearchSteps
{
    private SearchPage searchPage;
    private IEnumerable<string> searchResults;
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
        context.Navbar.ClickFindButton();

        searchResults = searchPage.GetSearchResults(keyword);
    }

    [Then("all search results should contain \"(.*)\"")]
    public void ThenAllSearchResultsShouldContain(string keyword)
    {
        bool allContainKeyword = searchResults.All(text =>
            text.Contains(keyword, StringComparison.OrdinalIgnoreCase)
        );

        Assert.That(allContainKeyword, Is.False,
            $"Not all links contain the word '{keyword}'.\n" +
            $"Texts: {string.Join(", ", searchResults)}");
    }
}
