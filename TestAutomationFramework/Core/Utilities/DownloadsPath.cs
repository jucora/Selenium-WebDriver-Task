namespace TestAutomationFramework.Core.Utilities
{
    public static class DownloadsPath
    {
        public static string Root { get; } = GetProjectRoot();

        private static string GetProjectRoot()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            // we search for the .csproj file upwards
            var dir = new DirectoryInfo(baseDir);
            while (dir != null && !dir.GetFiles("*.csproj").Any())
            {
                dir = dir.Parent;
            }

            if (dir == null)
                throw new Exception("Cannot find project root. No .csproj found.");

            return dir.FullName;
        }

        public static string DownloadFolder =>
            Path.Combine(Root + "\\bin\\Debug\\net8.0", "Downloads");
    }
}
