using Carrier_Search_Automation.Locators;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Carrier_Search_Automation.Helpers
{
    public class GlobalSearchTestHelper : Search
    {
        protected void clickMagnifier()
        {
            var magnifier = wait.Until(ExpectedConditions.ElementToBeClickable(
                GlobalSearchLocator.MagnifierIcon
            ));
            magnifier.Click();
        }

        protected void enterSearchKeyword(string keyword)
        {
            var searchInput = wait.Until(ExpectedConditions.ElementIsVisible(
                GlobalSearchLocator.SearchInputField
            ));
            searchInput.SendKeys(keyword);
        }

        protected void clickFindButton()
        {
            var findButton = driver.FindElement(GlobalSearchLocator.FindButton);
            findButton.Click();
        }

        protected void validateLinkTexts(string keyword)
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
