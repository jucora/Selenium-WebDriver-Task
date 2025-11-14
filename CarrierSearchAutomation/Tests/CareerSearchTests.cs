using CareerSearchAutomation.Helpers;
using NUnit.Framework;

namespace CareerSearch.Tests
{
    [TestFixture]
    public class CareerSearchTests : CareerSearchTestHelper
    {
        [TestCase("Python")]
        [TestCase("Java")]
        [TestCase("C#")]
        public void ValidateUserCanSearchPositionBasedOnCriteria(string keyword)
        {
            NavigateToUrl();
            ClickCareersLink();
            EnterKeyword(keyword);
            SelectLocation();
            SelectRemoteOption();
            ClickFindButton();
            SelectViewAndApplyFromLastResult();
            ValidateKeywordIsPresent(keyword);
        }
    }
}
