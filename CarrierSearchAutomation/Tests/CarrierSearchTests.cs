using Carrier_Search_Automation.Helpers;
using NUnit.Framework;

namespace CarrierSearch.Tests
{
    [TestFixture]
    public class CarrierSearchTests : CarrierSearchTestHelper
    {
        [TestCase("Python")]
        [TestCase("Java")]
        [TestCase("C#")]
        public void ValidateUserCanSearchPositionBasedOnCriteria(string keyword)
        {
            NavigateToUrl();
            ClickCarriersLink();
            EnterKeyword(keyword);
            SelectLocation();
            SelectRemoteOption();
            ClickFindButton();
            SelectViewAndApplyFromLastResult();
            ValidateKeywordIsPresent(keyword);
        }
    }
}
