using NUnit.Framework;
using SearchAutomation.Pages;
using SearchAutomation.Validators;

namespace SearchAutomation.Tests
{
    [TestFixture]
    public class ArticleTitleConsistencyTests : BaseTest
    {
        [Test]
        public void ArticleTitleMatchesCarouselTitle()
        {
            Log.Info("Hello World of Logging :) ...");
            InsightsPage insightsPage = navbar.ClickInsightsLink();
            insightsPage.SwipeCarousel();

            var slideTitle = insightsPage.GetActiveSlideTitle();
            insightsPage.ClickReadMoreLink();
            var articleTitle = insightsPage.GetArticleTitle();

            InsightsValidator.ValidateArticleNameConsistency(slideTitle, articleTitle);
        }
    }
}
