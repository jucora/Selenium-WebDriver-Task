using NUnit.Framework;
using Reqnroll;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Utilities;
using TestAutomationFramework.Tests.Context;

namespace TestAutomationFramework.Tests.Steps
{

    [Binding]
    public class DownloadFunctionSteps
    {
        private AboutPage aboutPage = null!;
        private readonly UiTestContext context;

        public DownloadFunctionSteps(UiTestContext context)
        {
            this.context = context;
        }

        [Given("the user is on the About page")]
        public void GivenTheUserIsOnTheAboutPage()
        {
            aboutPage = context.Navbar.ClickAboutLink();
        }

        [When("the user clicks the download button")]
        public void WhenTheUserClicksTheDownloadButton()
        {
            aboutPage.ClickDownloadButton();
        }

        [Then("the file \"(.*)\" should be downloaded")]
        public async Task ThenTheFileShouldBeDownloaded(string fileName)
        {
            var fileUtil = new FileUtil(context.Logger);

            bool fileDownloaded = await fileUtil.WaitForFileToDownloadAsync(fileName);

            Assert.That(fileDownloaded, Is.True,
                $"The file '{fileName}' was NOT downloaded");
        }
    }
}
