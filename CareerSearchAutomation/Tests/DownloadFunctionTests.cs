using Carrier_Search_Automation.Pages;
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
            IndexPage indexPage = new IndexPage(driver);
            indexPage.AcceptCookies();

            AboutPage aboutPage = indexPage.ClickAboutLink();
            aboutPage.ClickDownloadButton();

            DownloadFunctionValidator.ValidateFileDownloaded();
        }
    }
}
