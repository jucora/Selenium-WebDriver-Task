using System.Text.RegularExpressions;

namespace TestAutomationFramework.Core.Utilities
{
    public static class FileNameHelper
    {
        private static readonly string InvalidCharsPattern =
            "[\"<>|:*?\\r\\n]";

        public static string Sanitize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "unknown string value";

            return Regex.Replace(value, InvalidCharsPattern, "_");
        }
    }
}
