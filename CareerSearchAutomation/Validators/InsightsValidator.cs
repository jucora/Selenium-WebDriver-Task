using NUnit.Framework;

namespace Carrier_Search_Automation.Validators
{
    public static class InsightsValidator
    {
        public static void ValidateArticleNameConsistency(string slideTitle, string articleTitle) 
        {
            Assert.That(slideTitle.Equals(
                articleTitle), Is.True, 
                $"The slide title: {slideTitle} does not match with the article title: {articleTitle}");
        }
    }
}
