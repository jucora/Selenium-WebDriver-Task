namespace SearchAutomation.Utils
{
    public static class ProjectPaths
    {
        public static string Root =>
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                     .Parent.Parent.Parent.FullName;

        public static string DownloadFolder =>
            Path.Combine(Root, "Downloads");
    }
}
