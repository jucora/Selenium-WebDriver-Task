using NUnit.Framework;
using Reqnroll;
using OpenQA.Selenium;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;

[Binding]
public class GlobalSearchSteps : BasePage
{
    private NavbarComponent navbar;
    private SearchPage searchPage;
    private IEnumerable<string> searchResults;

    public GlobalSearchSteps(IWebDriver driver, ILogger logger) : base(driver, logger) {}

    [Given("the user opens the global search")]
    public void GivenTheUserOpensTheGlobalSearch()
    {
        navbar = new NavbarComponent(Driver, Logger);
        navbar.ClickMagnifierIcon();
    }

    [When("the user searches for \"(.*)\"")]
    public void WhenTheUserSearchesFor(string keyword)
    {
        navbar.EnterSearchKeyword(keyword);
        searchPage = navbar.ClickFindButton();

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
