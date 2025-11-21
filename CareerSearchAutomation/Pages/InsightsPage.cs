using OpenQA.Selenium;
using SearchAutomation.Locators.Pages;

namespace SearchAutomation.Pages
{
    public class InsightsPage : BasePage
    {
        private readonly int numberOfCarouselClicks = 2;
        public InsightsPage(IWebDriver driver) : base(driver){}

        public InsightsPage SwipeCarousel()
        {
            for (int i = 0; i < numberOfCarouselClicks; i++)
            {
                Click(InsightsPageLocators.CarouselRightArrow);
            }
            return this;
        }

        public string GetActiveSlideTitle()
        {
            // Capture previous text if it exists
            string previousTitle = TryGetActiveSlideRawText();

            // Wait for the active slide text to change
            wait.Until(d =>
            {
                string newTitle = TryGetActiveSlideRawText();
                return newTitle != previousTitle && !string.IsNullOrWhiteSpace(newTitle);
            });

            // When changed, return clean text
            return TryGetActiveSlideRawText();
        }


        private string TryGetActiveSlideRawText()
        {
            var activeSlide = driver.FindElement(InsightsPageLocators.ActiveSlide);

            var spans = activeSlide
                .FindElements(By.CssSelector("span"))
                .Where(s => s.Displayed)
                .Select(s => s.Text.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .ToList();

            if (!spans.Any())
                return string.Empty;

            // spans[0] → title
            return spans[0];
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
