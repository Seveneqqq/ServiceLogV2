using System.Security.Cryptography;
using System.Text;

namespace ServiceLog.Helpers
{
    public static class ShortCodeGenerator
    {
        private static readonly char[] _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string GenerateShortId(int length = 10)
        {
            var randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var result = new StringBuilder(length);
            foreach (var b in randomBytes)
            {
                result.Append(_chars[b % _chars.Length]);
            }

            return result.ToString();
        }
    }
}
