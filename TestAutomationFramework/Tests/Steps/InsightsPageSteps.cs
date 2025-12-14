using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;

[Binding]
public class InsightsPageSteps
{
    private InsightsPage insightsPage;
    private string slideTitle;
    private string articleTitle;
    private readonly UiTestContext context;

    public InsightsPageSteps(UiTestContext context)
    {
        this.context = context;
    }

    [Given("the user is on the Insights page")]
    public void GivenUserIsOnInsightsPage()
    {
        insightsPage = context.Navbar.ClickInsightsLink();
    }

    [When("the user swipes the carousel")]
    public void WhenUserSwipesCarousel()
    {
        insightsPage.SwipeCarousel();
        slideTitle = insightsPage.GetActiveSlideTitle();

        context.Logger.Info($"Captured slide title: {slideTitle}");
    }

    [When("the user opens the article via Read More")]
    public void WhenUserOpensArticleViaReadMore()
    {
        insightsPage.ClickReadMoreLink();
        articleTitle = insightsPage.GetArticleTitle();

        context.Logger.Info($"Captured article title: {articleTitle}");
    }

    [Then("the article title should match the slide title")]
    public void ThenArticleTitleShouldMatchSlideTitle()
    {
        Assert.That(articleTitle, Is.EqualTo(slideTitle),
            $"Slide title '{slideTitle}' does not match article title '{articleTitle}'");
    }
}
