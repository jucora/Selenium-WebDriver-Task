using NUnit.Framework;
using Reqnroll;
using OpenQA.Selenium;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;
using TestAutomationFramework.Core.Utilities;

[Binding]
public class DownloadFunctionSteps : BasePage
{
    private AboutPage aboutPage;

    public DownloadFunctionSteps(IWebDriver driver, ILogger logger) : base(driver, logger) {}

    [Given("the user is on the About page")]
    public void GivenTheUserIsOnTheAboutPage()
    {
        var navbar = new NavbarComponent(Driver, Logger);
        aboutPage = navbar.ClickAboutLink();
    }

    [When("the user clicks the download button")]
    public void WhenTheUserClicksTheDownloadButton()
    {
        aboutPage.ClickDownloadButton();
    }

    [Then("the file \"(.*)\" should be downloaded")]
    public void ThenTheFileShouldBeDownloaded(string fileName)
    {
        var fileUtil = new FileUtil(Logger);

        bool fileDownloaded = fileUtil.WaitForFileToDownload(fileName);

        Assert.That(fileDownloaded, Is.True,
            $"The file '{fileName}' was NOT downloaded");
    }
}
