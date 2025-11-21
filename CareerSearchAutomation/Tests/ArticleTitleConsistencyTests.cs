using Carrier_Search_Automation.Validators;
using NUnit.Framework;
using SearchAutomation.Base;
using SearchAutomation.Pages;

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

            InsightsValidator.ValidateArticleNameConsistency(slideTitle, articleTitle);
        }
    }
}
