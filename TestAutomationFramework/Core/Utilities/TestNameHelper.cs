using NUnit.Framework;
using System.Security.Cryptography;
using System.Text;

namespace TestAutomationFramework.Core.Utilities
{
    public static class TestNameHelper
    {
        public static string GetSafeTestName()
        {
            var test = TestContext.CurrentContext.Test;

            var baseName = test.MethodName;

            var parameters = test.Arguments?.Any() == true
                ? string.Join("_", test.Arguments.Select(a => a?.ToString()))
                : string.Empty;

            var rawName = $"{baseName}_{parameters}";

            var hash = GetShortHash(test.ID);

            var safeName = $"{rawName}_{hash}";

            return FileNameHelper.Sanitize(safeName);
        }

        private static string GetShortHash(string value)
        {
            using var sha1 = SHA1.Create();
            var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(bytes)[..6]; // 6 chars
        }
    }
}
