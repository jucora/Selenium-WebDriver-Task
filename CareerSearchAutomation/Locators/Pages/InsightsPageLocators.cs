using OpenQA.Selenium;

namespace SearchAutomation.Locators.Pages
{
    public static class InsightsPageLocators
    {
        public static By CarouselRightArrow = 
            By.XPath("(//button[contains(@role,'presentation')])[4]");

        public static By ActiveSlide = 
            By.CssSelector("[aria-hidden='false']");

        public static By ReadMoreLink = 
            By.CssSelector("div.owl-item[aria-hidden='false'] a.slider-cta-link");

        // Some articles have two spans for the title: one for the top part and another for the bottom part
        public static By ArticleTopTitle =
            By.CssSelector("span.font-size-80-33 span.museo-sans-500");

        public static By ArticleBottomTitle =
            By.CssSelector("span.font-size-80-33 span.museo-sans-light");
    }
}
