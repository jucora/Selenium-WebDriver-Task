using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Reqnroll;
using TestAutomationFramework.Business.Pages.Services.AI;
using TestAutomationFramework.Tests.Context;

namespace TestAutomationFramework.Tests.Steps
{
    [Binding]
    public class ServicesPageSteps
    {
        private AIServiceBasePage CurrentAIPage = null!;
        private readonly UiTestContext context;

        private static readonly By ServicesMenu =
            By.XPath("//span[normalize-space()='Services']");

        public ServicesPageSteps(UiTestContext context)
        {
            this.context = context;
        }

        private static By DynamicServiceCategory(string category) =>
            By.XPath($"//a[@class='top-navigation__sub-link' and normalize-space(.) = '{category}']");

        #region Step Definitions

        [When(@"the user navigates to the ""(.*)"" section")]
        public void WhenUserNavigatesToServices(string sectionName)
        {
            if (!sectionName.Equals("Services", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Unknown section: {sectionName}");

            context.Logger.Info("Hovering over the Services menu...");

            var servicesElement = context.WaitHelper.WaitForElementVisible(ServicesMenu);

            Actions actions = new Actions(context.Driver);
            actions.MoveToElement(servicesElement).Perform();

            context.Logger.Info("Hover performed successfully on 'Services' menu.");
        }

        [When(@"the user selects the ""(.*)"" category")]
        public void WhenUserSelectsServiceCategory(string serviceCategory)
        {
            context.Logger.Info($"Selecting category: {serviceCategory}");

            var categoryLocator = DynamicServiceCategory(serviceCategory);
            var categoryElement = context.WaitHelper.WaitForElementClickable(categoryLocator);

            categoryElement.Click();

            // Assign the correct page object to the base type
            CurrentAIPage = serviceCategory switch
            {
                "Generative AI" => new GenerativeAIPage(context.Driver, context.Logger),
                "Responsible AI" => new ResponsibleAIPage(context.Driver, context.Logger),

                _ => throw new ArgumentException($"Unknown service category: {serviceCategory}")
            };
        }

        [Then(@"the page title should contain ""(.*)""")]
        public void ThenPageTitleShouldContain(string expectedTitle)
        {
            if (CurrentAIPage == null)
                throw new InvalidOperationException("AI Page is not initialized.");

            context.Logger.Info($"Validating page title contains: {expectedTitle}");

            string actualTitle = CurrentAIPage.GetServiceTitle();

            if (!actualTitle.Contains(expectedTitle, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Expected title to contain '{expectedTitle}' but was '{actualTitle}'");

            context.Logger.Info("Title validation successful.");
        }


        [Then(@"the ""(.*)"" section should be displayed")]
        public void ThenSectionShouldBeDisplayed(string sectionName)
        {
            if (CurrentAIPage == null)
                throw new InvalidOperationException("AI Page is not initialized.");

            context.Logger.Info($"Validating section '{sectionName}' is visible...");

            bool isDisplayed = CurrentAIPage.IsRelatedExpertiseDisplayed();

            if (!isDisplayed)
                throw new Exception($"Expected section '{sectionName}' to be displayed, but it was not.");

            context.Logger.Info($"Section '{sectionName}' is displayed.");
        }


        #endregion
    }
}
