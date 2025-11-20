using Carrier_Search_Automation.Locators;
using OpenQA.Selenium;
using SearchAutomation.Pages;

namespace Carrier_Search_Automation.Pages
{
    public class InsightsPage : BasePage
    {
        private readonly int numberOfCarouselClicks = 2;
        public InsightsPage(IWebDriver driver) : base(driver)
        {
        }

        public InsightsPage SwipeCarousel() 
        {
            for (int i = 0; i < numberOfCarouselClicks; i++)
            {
                Click(InsightsPageLocators.CarouselRightArrow);
                Thread.Sleep(2000); // Wait 2 seconds between each click to allow the transition
            }
            return this;
        }

        public string GetActiveSlideTitle()
        {
            // 1. Active slide
            var activeSlide = driver.FindElement(InsightsPageLocators.ActiveSlide);

            // 2. Capture all visible SPANs on the slide
            var spans = activeSlide.FindElements(By.CssSelector("span"))
                .Where(s => s.Displayed)
                .Select(s => s.Text.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();

            // 3. Join all text on a single line
            string fullTitle = string.Join(" ", spans[0]);

            return fullTitle.Trim();
        }

        public void ClickReadMoreLink()
        {
            Click(InsightsPageLocators.ReadMoreLink);
        }

        public string GetArticleTitle()
        {
            // 1. Get both possible title spans
            var articleTopTitle = driver.FindElements(
                InsightsPageLocators.ArticleTopTitle)
                .Where(e => e.Displayed)
                .Select(e => e.Text.Trim())
                .FirstOrDefault(); // null if it does not exist

            var articleBottomTitle = driver.FindElements(
                InsightsPageLocators.ArticleBottomTitle)
                .Where(e => e.Displayed)
                .Select(e => e.Text.Trim())
                .FirstOrDefault();

            // 2. If both null → no title exists
            if (articleTopTitle == null && articleBottomTitle == null)
                throw new Exception("No article title spans were found.");

            // 3. Combine only what exists
            return string.Join(" ", new[] { articleTopTitle, articleBottomTitle }
                .Where(t => !string.IsNullOrWhiteSpace(t)))
                .Trim();
        }
    }
}
