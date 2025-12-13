using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Business.Pages.Services.AI
{
    public class ResponsibleAIPage : AIServiceBasePage
    {
        private static readonly By RelatedExpertiseLocator =
            By.XPath("(//span[@class='museo-sans-light'])[11]");

        public ResponsibleAIPage(IWebDriver driver, ILogger logger)
            : base(driver, logger, RelatedExpertiseLocator)
        {
        }
    }
}
