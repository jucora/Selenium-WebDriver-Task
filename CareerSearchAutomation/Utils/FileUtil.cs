namespace SearchAutomation.Utils
{
    public static class FileUtil
    {
        public static bool WaitForFileToDownload(string fileName, int timeoutSeconds = 10)
        {
            var downloadDir = ProjectPaths.DownloadFolder;


            if (!Directory.Exists(downloadDir))
                throw new DirectoryNotFoundException($"Download folder not found: {downloadDir}");

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
