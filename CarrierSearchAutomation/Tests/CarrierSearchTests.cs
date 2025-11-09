using Carrier_Search_Automation.Helpers;
using NUnit.Framework;

namespace CarrierSearch.Tests
{
    [TestFixture]
    public class CarrierSearchTests : CarrierSearchTestHelper
    {
        [TestCase("https://www.epam.com/", "Python", "All Locations")]
        [TestCase("https://www.epam.com/", "Java", "All Locations")]
        [TestCase("https://www.epam.com/", "C#", "All Locations")]
        public void ValidateUserCanSearchPositionBasedOnCriteria(string url, string keyword, string location)
        {
            navigateToUrl(url);
            clickCarriersLink();
            enterKeyword(keyword);
            selectLocation(location);
            selectRemoteOption();
            clickFindButton();
            selectViewAndApplyFromLastResult();
            validateKeywordIsPresent(keyword);
        }
    }
}
