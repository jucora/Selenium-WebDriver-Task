using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages.Services.AI
{
    public class GenerativeAIPage : AIServiceBasePage
    {
        private static readonly By RelatedExpertiseLocator =
            By.XPath("(//span[@class='museo-sans-light'])[11]");

        public GenerativeAIPage(IWebDriver driver, ILogger logger)
            : base(driver, logger, RelatedExpertiseLocator)
        {
        }
    }
}
