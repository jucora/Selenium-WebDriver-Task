using Global_Search_Automation.Helpers;
using NUnit.Framework;

namespace GlobalSearch.Tests
{
    [TestFixture]
    public class GlobalSearchTests : GlobalSearchTestHelper
    {
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void ValidateUserCanSearchBasedOnCriteria(string keyword)
        {
            NavigateToUrl();
            ClickMagnifierIcon();
            EnterSearchKeyword(keyword);
            ClickFindButton();
            ValidateLinkTexts(keyword);
        }
    }
}
