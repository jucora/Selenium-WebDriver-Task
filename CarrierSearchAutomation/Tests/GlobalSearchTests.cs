using Carrier_Search_Automation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace GlobalSearch.Tests
{
    [TestFixture]
    public class GlobalSearchTests : GlobalSearchTestHelper
    {
        [TestCase("https://www.epam.com/", "BLOCKCHAIN")]
        [TestCase("https://www.epam.com/", "Cloud")]
        [TestCase("https://www.epam.com/", "Automation")]
        public void ValidateUserCanSearchBasedOnCriteria(string url, string keyword)
        {
            navigateToUrl(url);
            clickMagnifier();
            enterSearchKeyword(keyword);
            clickFindButton();
            validateLinkTexts(keyword);
        }
    }
}
