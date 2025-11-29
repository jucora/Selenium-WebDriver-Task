using TestAutomationFramework.Core.Configuration;
using TestAutomationFramework.Core.Logging;

namespace TestAutomationFramework.Core.Utilities
{
    public class FileUtil
    {
        private readonly ILogger logger;

        public FileUtil(ILogger logger)
        {
            this.logger = logger;
        }

        public bool WaitForFileToDownload(string fileName, int timeoutSeconds = 10)
        {
            logger.Info($"Waiting for the file '{fileName}' to be downloaded within {timeoutSeconds} seconds...");
            var downloadDir = DownloadsPath.DownloadFolder;


            if (!Directory.Exists(downloadDir))
                Directory.CreateDirectory(downloadDir);

            var timeout = DateTime.Now.AddSeconds(timeoutSeconds);

            while (DateTime.Now < timeout)
            {
                var files = Directory.GetFiles(downloadDir);

                bool exists = files.Any(f =>
                    Path.GetFileName(f).Equals(fileName, StringComparison.OrdinalIgnoreCase) &&
                    !f.EndsWith(".crdownload")
                );

                if (exists)
                    return true;

                Thread.Sleep(500); // checks every 0.5 seconds
            }

            return false; // if it never appeared
        }
    }
}
