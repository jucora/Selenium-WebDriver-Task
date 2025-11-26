using NUnit.Framework;
using SearchAutomation.Utils;

namespace SearchAutomation.Validators
{
    public static class DownloadFunctionValidator
    {
        public static void ValidateFileDownloaded() 
        {
            Assert.That(
                FileUtil.WaitForFileToDownload("EPAM_Corporate_Overview_Sept_25.pdf"), Is.True,
                "The file EPAM_Systems_Company_Overview.pdf was NOT downloaded");
        }
    }
}
