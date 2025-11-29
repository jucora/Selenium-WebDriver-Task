using NUnit.Framework;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Tests;

namespace SearchAutomation.Tests
{
    [TestFixture]
    public class ArticleTitleConsistencyTests : BaseTest
    {
        [Test]
        public void ArticleTitleMatchesCarouselTitle()
        {
            InsightsPage insightsPage = navbar.ClickInsightsLink();
            insightsPage.SwipeCarousel();

            var slideTitle = insightsPage.GetActiveSlideTitle();
            insightsPage.ClickReadMoreLink();
            var articleTitle = insightsPage.GetArticleTitle();

            Assert.That(slideTitle.Equals(
                articleTitle), Is.True,
                $"The slide title: {slideTitle} does not match with the article title: {articleTitle}");
        }
    }
}
