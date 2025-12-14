using System.Diagnostics;
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

        public async Task<bool> WaitForFileToDownloadAsync(
            string fileName,
            int timeoutSeconds = 10,
            int pollingMilliseconds = 500)
        {
            logger.Info($"Waiting for the file '{fileName}' to be downloaded within {timeoutSeconds} seconds...");

            var downloadDir = DownloadsPath.DownloadFolder;

            if (!Directory.Exists(downloadDir))
                Directory.CreateDirectory(downloadDir);

            var timeout = TimeSpan.FromSeconds(timeoutSeconds);
            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.Elapsed < timeout)
            {
                var exists = Directory
                    .GetFiles(downloadDir)
                    .Any(f =>
                        Path.GetFileName(f)
                            .Equals(fileName, StringComparison.OrdinalIgnoreCase) &&
                        !f.EndsWith(".crdownload"));

                if (exists)
                    return true;

                await Task.Delay(pollingMilliseconds);
            }

            return false;
        }
    }
}
