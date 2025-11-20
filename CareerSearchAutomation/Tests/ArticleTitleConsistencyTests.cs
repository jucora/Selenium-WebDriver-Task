using Carrier_Search_Automation.Pages;
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
            IndexPage indexPage = new IndexPage(driver);
            indexPage.AcceptCookies();

            InsightsPage insightsPage = indexPage.ClickInsightsLink();
            insightsPage.SwipeCarousel();

            var slideTitle = insightsPage.GetActiveSlideTitle();
            insightsPage.ClickReadMoreLink();
            var articleTitle = insightsPage.GetArticleTitle();

            InsightsValidator.ValidateArticleNameConsistency(slideTitle, articleTitle);
        }
    }
}
