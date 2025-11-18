using SearchAutomation.Base;
using SearchAutomation.Pages;
using SearchAutomation.Validators;
using NUnit.Framework;

namespace CareerSearch.Tests
{
    [TestFixture]
    public class CareerSearchTests : BaseTest
    {
        [TestCase("Python")]
        [TestCase("Java")]
        [TestCase("C#")]
        public void ValidateUserCanSearchPositionBasedOnCriteria(string keyword)
        {
            IndexPage indexPage = new IndexPage(driver);
            
            CareersPage careersPage = indexPage.ClickCareersLink();
            careersPage
                .EnterKeyword(keyword)
                .SelectLocation()
                .SelectRemoteOption();

            JobListingsPage jobListingsPage = careersPage.ClickFindButton();
            jobListingsPage
                .SelectViewAndApplyFromLastResult();

            CareerSearchValidator
                .ValidateKeywordIsPresent(keyword, driver.PageSource);
        }
    }
}
