using Carrier_Search_Automation.Validators;
using NUnit.Framework;
using SearchAutomation.Base;
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
