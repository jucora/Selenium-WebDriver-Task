using NUnit.Framework;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Tests;

namespace SearchAutomation.Tests
{
    [TestFixture]
    public class CareerSearchTests : BaseTest
    {
        [TestCase("Python")]
        [TestCase("Java")]
        [TestCase("C#")]
        public void ValidateUserCanSearchPositionBasedOnCriteria(string keyword)
        {
            CareersPage careersPage = navbar.ClickCareersLink();
            
            careersPage
                .ClickStartSearchLink();

            cookies.AcceptCookiesIfPresent();

            careersPage.EnterKeyword(keyword)
                .SelectRemoteOption()
                .cleanLocationFilter();

            JobListingsPage jobListingsPage = careersPage.ClickFindButton();
            jobListingsPage
                .SelectViewAndApplyFromLastResult();

            bool containsKeyword = Driver.PageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            Assert.That(containsKeyword,
                $"Expected to find '{keyword}' in the job description, but it was not found.");
        }
    }
}
