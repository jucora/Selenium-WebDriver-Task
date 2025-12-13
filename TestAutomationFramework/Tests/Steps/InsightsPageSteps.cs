using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;

[Binding]
public class InsightsPageSteps : BasePage
{
    private InsightsPage insightsPage;
    private string slideTitle;
    private string articleTitle;

    public InsightsPageSteps(IWebDriver driver, ILogger logger) : base(driver, logger){}

    [Given("the user is on the Insights page")]
    public void GivenUserIsOnInsightsPage()
    {
        Logger.Info("Navigating to EPAM homepage...");
        var navbar = new NavbarComponent(Driver, Logger);
        insightsPage = navbar.ClickInsightsLink();
    }

    [When("the user swipes the carousel")]
    public void WhenUserSwipesCarousel()
    {
        insightsPage.SwipeCarousel();
        slideTitle = insightsPage.GetActiveSlideTitle();

        Logger.Info($"Captured slide title: {slideTitle}");
    }

    [When("the user opens the article via Read More")]
    public void WhenUserOpensArticleViaReadMore()
    {
        insightsPage.ClickReadMoreLink();
        articleTitle = insightsPage.GetArticleTitle();

        Logger.Info($"Captured article title: {articleTitle}");
    }

    [Then("the article title should match the slide title")]
    public void ThenArticleTitleShouldMatchSlideTitle()
    {
        Assert.That(articleTitle, Is.EqualTo(slideTitle),
            $"Slide title '{slideTitle}' does not match article title '{articleTitle}'");
    }
}
