using NUnit.Framework;
using Reqnroll;
using OpenQA.Selenium;
using TestAutomationFramework.Business.Components;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Logging;

[Binding]
public class CareerSearchSteps : BasePage
{
    private CareersPage careersPage;
    private JobListingsPage jobListingsPage;

    public CareerSearchSteps(IWebDriver driver, ILogger logger) : base(driver, logger){ }

    [Given("the user is on the Careers page")]
    public void GivenTheUserIsOnTheCareersPage()
    {
        var navbar = new NavbarComponent(Driver, Logger);
        careersPage = navbar.ClickCareersLink();
    }

    [When("the user enters the keyword \"(.*)\"")]
    public void WhenTheUserEntersTheKeyword(string keyword)
    {
        careersPage.EnterKeyword(keyword);
    }

    [When("the user selects remote option")]
    public void WhenTheUserSelectsRemoteOption()
    {
        careersPage.SelectRemoteOption();
    }

    [When("the user selects a location")]
    public void WhenTheUserSelectsLocation()
    {
        careersPage.SelectLocation();
    }

    [When("the user clicks Find Jobs")]
    public void WhenTheUserClicksFindJobs()
    {
        jobListingsPage = careersPage.ClickFindButton();
    }

    [When("the user opens the last job result")]
    public void WhenTheUserOpensTheLastJobResult()
    {
        jobListingsPage.SelectViewAndApplyFromLastResult();
    }

    [Then("the job description should contain the keyword \"(.*)\"")]
    public void ThenJobDescriptionShouldContainKeyword(string keyword)
    {
        bool containsKeyword = Driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);

        Assert.That(containsKeyword, Is.True,
            $"Expected to find keyword '{keyword}' in the job description, but it was not found.");
    }
}
