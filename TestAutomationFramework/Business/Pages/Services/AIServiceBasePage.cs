using OpenQA.Selenium;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

namespace TestAutomationFramework.Business.Pages.Services.AI
{
    public abstract class AIServiceBasePage : BasePage
    {
        // Locator común del título
        private static readonly By TitleLocator =
            By.CssSelector(".museo-sans-500.gradient-text");

        protected readonly By RelatedExpertiseSectionLocator;

        protected AIServiceBasePage(
            IWebDriver driver,
            ILogger logger,
            By relatedExpertiseLocator)
            : base(driver, logger)
        {
            RelatedExpertiseSectionLocator = relatedExpertiseLocator;
        }

        public string GetServiceTitle()
        {
            Logger.Info("Retrieving service title...");

            var titleElement = WaitHelper.WaitForElementVisible(TitleLocator);
            return titleElement.Text.Trim();
        }

        public bool IsRelatedExpertiseDisplayed()
        {
            Logger.Info("Checking if the Related Expertise section is visible...");

            try
            {
                var section = WaitHelper.WaitForElementVisible(RelatedExpertiseSectionLocator);
                return section.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
