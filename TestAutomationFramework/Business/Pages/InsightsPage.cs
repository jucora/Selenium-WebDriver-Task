using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages
{
    public class InsightsPage : BasePage
    {
        private static By CarouselRightArrow =
            By.XPath("(//button[contains(@role,'presentation')])[4]");

        private static By ActiveSlide =
            By.CssSelector("[aria-hidden='false']");

        private static By ReadMoreLink =
            By.CssSelector("div.owl-item[aria-hidden='false'] a.slider-cta-link");

        // Some articles have two spans for the title: one for the top part and another for the bottom part
        private static By ArticleTopTitle =
            By.CssSelector("span.font-size-80-33 span.museo-sans-500");

        private static By ArticleBottomTitle =
            By.CssSelector("span.font-size-80-33 span.museo-sans-light");

        private readonly int numberOfCarouselClicks = 2;

        /// <summary>
        /// Constructor that calls the base constructor
        /// </summary>
        public InsightsPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Logger.Info("InsightsPage initialized");
        }

        public InsightsPage SwipeCarousel()
        {
            for (int i = 0; i < numberOfCarouselClicks; i++)
            {
                Click(CarouselRightArrow);
            }
            return this;
        }

        public string GetActiveSlideTitle()
        {
            // Capture previous text if it exists
            string previousTitle = TryGetActiveSlideRawText();

            // Wait for the active slide text to change
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Configuration.GetExplicitWait()));
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
            var activeSlide = Driver.FindElement(ActiveSlide);

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
            Click(ReadMoreLink);
        }

        public string GetArticleTitle()
        {
            // 1. Get both possible title spans
            var articleTopTitle = Driver.FindElements(
                ArticleTopTitle)
                .Where(e => e.Displayed)
                .Select(e => e.Text.Trim())
                .FirstOrDefault(); // null if it does not exist

            var articleBottomTitle = Driver.FindElements(
                ArticleBottomTitle)
                .Where(e => e.Displayed)
                .Select(e => e.Text.Trim())
                .FirstOrDefault();

            // 2. If both null → no title exists
            if (articleTopTitle == null && articleBottomTitle == null)
                throw new InvalidOperationException("No article title spans were found.");

            // 3. Combine only what exists
            return string.Join(" ", new[] { articleTopTitle, articleBottomTitle }
                .Where(t => !string.IsNullOrWhiteSpace(t)))
                .Trim();
        }
    }
}
