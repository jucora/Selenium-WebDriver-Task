using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Reqnroll;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Business.Pages.Services.AI;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

namespace TestAutomationFramework.Tests.Steps
{
    [Binding]
    public class ServicesPageSteps : BasePage
    {
        // Pages
        private AIServiceBasePage? CurrentAIPage;

        public ServicesPageSteps(IWebDriver driver, ILogger logger) : base(driver, logger){}

        // Locators for top navigation
        private static readonly By ServicesMenu =
            By.XPath("//span[normalize-space()='Services']");

        private static By DynamicServiceCategory(string category) =>
            By.XPath($"//a[@class='top-navigation__sub-link' and normalize-space(.) = '{category}']");

        #region Step Definitions

        [Given(@"the user is on the EPAM homepage")]
        public void GivenUserIsOnHomepage()
        {
            Logger.Info("Navigating to EPAM homepage...");
            Driver.Navigate().GoToUrl("https://www.epam.com/");
        }

        [When(@"the user navigates to the ""(.*)"" section")]
        public void WhenUserNavigatesToServices(string sectionName)
        {
            if (!sectionName.Equals("Services", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Unknown section: {sectionName}");

            Logger.Info("Hovering over the Services menu...");

            var servicesElement = WaitHelper.WaitForElementVisible(ServicesMenu);

            Actions actions = new Actions(Driver);
            actions.MoveToElement(servicesElement).Perform();

            Logger.Info("Hover performed successfully on 'Services' menu.");
        }

        [When(@"the user selects the ""(.*)"" category")]
        public void WhenUserSelectsServiceCategory(string serviceCategory)
        {
            Logger.Info($"Selecting category: {serviceCategory}");

            var categoryLocator = DynamicServiceCategory(serviceCategory);
            var categoryElement = WaitHelper.WaitForElementClickable(categoryLocator);

            categoryElement.Click();
            WaitForPageLoad();

            // Assign the correct page object to the base type
            CurrentAIPage = serviceCategory switch
            {
                "Generative AI" => new GenerativeAIPage(Driver, Logger),
                "Responsible AI" => new ResponsibleAIPage(Driver, Logger),

                _ => throw new ArgumentException($"Unknown service category: {serviceCategory}")
            };
        }

        [Then(@"the page title should contain ""(.*)""")]
        public void ThenPageTitleShouldContain(string expectedTitle)
        {
            if (CurrentAIPage == null)
                throw new InvalidOperationException("AI Page is not initialized.");

            Logger.Info($"Validating page title contains: {expectedTitle}");

            string actualTitle = CurrentAIPage.GetServiceTitle();

            if (!actualTitle.Contains(expectedTitle, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Expected title to contain '{expectedTitle}' but was '{actualTitle}'");

            Logger.Info("Title validation successful.");
        }


        [Then(@"the ""(.*)"" section should be displayed")]
        public void ThenSectionShouldBeDisplayed(string sectionName)
        {
            if (CurrentAIPage == null)
                throw new InvalidOperationException("AI Page is not initialized.");

            Logger.Info($"Validating section '{sectionName}' is visible...");

            bool isDisplayed = CurrentAIPage.IsRelatedExpertiseDisplayed();

            if (!isDisplayed)
                throw new Exception($"Expected section '{sectionName}' to be displayed, but it was not.");

            Logger.Info($"Section '{sectionName}' is displayed.");
        }


        #endregion
    }
}
