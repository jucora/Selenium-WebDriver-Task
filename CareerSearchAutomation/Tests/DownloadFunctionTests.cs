using SearchAutomation.Validators;
using NUnit.Framework;
using SearchAutomation.Pages;

namespace SearchAutomation.Tests
{
    [TestFixture]
    public class DownloadFunctionTests : BaseTest
    {
        [Test]
        public void ValidateDownloadFunction() 
        {
            AboutPage aboutPage = navbar.ClickAboutLink();
            aboutPage.ClickDownloadButton();

            DownloadFunctionValidator.ValidateFileDownloaded();
        }
    }
}
