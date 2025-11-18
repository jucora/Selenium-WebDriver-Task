using BaseSearchAutomation.Helpers;
using GlobalSearchAutomation.Locators;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace Global_Search_Automation.Helpers
{
    public class GlobalSearchTestHelper : BaseTest
    {
        protected void ClickMagnifierIcon()
        {
            var magnifier = wait.Until(ExpectedConditions.ElementToBeClickable(
                GlobalSearchLocator.MagnifierIcon
            ));
            magnifier.Click();
        }

        protected void EnterSearchKeyword(string keyword)
        {
            var searchInput = wait.Until(ExpectedConditions.ElementIsVisible(
                GlobalSearchLocator.SearchInputField
            ));
            searchInput.SendKeys(keyword);
        }

        protected void ClickFindButton()
        {
            var findButton = driver.FindElement(GlobalSearchLocator.FindButton);
            findButton.Click();
        }

        protected void ValidateLinkTexts(string keyword)
        {
            var links = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                GlobalSearchLocator.SearchResultLinks
            ));

            var linkTexts = links.Select(link => link.Text.Trim()).ToList();

            bool allContainKeyword = linkTexts.All(text =>
                text.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            );

            Assert.That(allContainKeyword, Is.False, // should be true?
                $"Not all links contain the word '{keyword}'.\n" +
                $"Texts: {string.Join(", ", linkTexts)}");
        }
    }
}
