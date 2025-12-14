using NUnit.Framework;
using Reqnroll;
using TestAutomationFramework.Business.Pages;

[Binding]
public class CareerSearchSteps
{
    private CareersPage careersPage;
    private JobListingsPage jobListingsPage;
    private readonly UiTestContext context;

    public CareerSearchSteps(UiTestContext context)
    {
        this.context = context;
    }

    [Given("the user is on the Careers page")]
    public void GivenTheUserIsOnTheCareersPage()
    {
        careersPage = context.Navbar.ClickCareersLink();
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
        bool containsKeyword = context.Driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);

        Assert.That(containsKeyword, Is.True,
            $"Expected to find keyword '{keyword}' in the job description, but it was not found.");
    }
}
