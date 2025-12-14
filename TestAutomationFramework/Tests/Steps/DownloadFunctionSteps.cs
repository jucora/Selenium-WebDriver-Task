using NUnit.Framework;
using Reqnroll;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Utilities;

[Binding]
public class DownloadFunctionSteps
{
    private AboutPage aboutPage;
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
    public void ThenTheFileShouldBeDownloaded(string fileName)
    {
        var fileUtil = new FileUtil(context.Logger);

        bool fileDownloaded = fileUtil.WaitForFileToDownload(fileName);

        Assert.That(fileDownloaded, Is.True,
            $"The file '{fileName}' was NOT downloaded");
    }
}
