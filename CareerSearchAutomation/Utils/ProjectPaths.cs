namespace SearchAutomation.Utils
{
    public static class ProjectPaths
    {
        public static string Root
        {
            get
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var parent1 = Directory.GetParent(baseDir);
                if (parent1 == null)
                    throw new InvalidOperationException("Cannot determine project root: base directory has no parent.");
                var parent2 = parent1.Parent;
                if (parent2 == null)
                    throw new InvalidOperationException("Cannot determine project root: base directory's parent has no parent.");
                var parent3 = parent2.Parent;
                if (parent3 == null)
                    throw new InvalidOperationException("Cannot determine project root: base directory's grandparent has no parent.");
                return parent3.FullName;
            }
        }

        public static string DownloadFolder =>
            Path.Combine(Root, "Downloads");
    }
}
