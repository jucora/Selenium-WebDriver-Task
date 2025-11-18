using NUnit.Framework;

namespace SearchAutomation.Validators
{
    public static class CareerSearchValidator
    {
        public static void ValidateKeywordIsPresent(string keyword, string pageSource)
        {
            bool containsKeyword = pageSource.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            Assert.That(containsKeyword,
                $"Expected to find '{keyword}' in the job description, but it was not found.");
        }
    }
}
