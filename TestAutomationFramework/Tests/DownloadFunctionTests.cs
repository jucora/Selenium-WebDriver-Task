using NUnit.Framework;
using TestAutomationFramework.Business.Pages;
using TestAutomationFramework.Core.Utilities;
using TestAutomationFramework.Tests;

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

            FileUtil fileUtil = new FileUtil(Logger);
            Assert.That(
                fileUtil.WaitForFileToDownload("EPAM_Corporate_Overview_Sept_25.pdf"), Is.True,
                "The file EPAM_Systems_Company_Overview.pdf was NOT downloaded");
        }
    }
}
