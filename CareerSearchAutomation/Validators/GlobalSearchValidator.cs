using NUnit.Framework;

namespace SearchAutomation.Validators
{
    public static class GlobalSearchValidator
    {
        public static void ValidateLinkTexts(List<string> linkTexts, string keyword)
        {
            bool allContainKeyword = linkTexts.All(text =>
                text.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            );

            Assert.That(allContainKeyword, Is.False, // should be true?
                $"Not all links contain the word '{keyword}'.\n" +
                $"Texts: {string.Join(", ", linkTexts)}");
        }
    }
}
