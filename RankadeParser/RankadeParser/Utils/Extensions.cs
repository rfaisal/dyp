using System;

namespace RankadeParser.Utils
{
    public static class Extensions
    {
        /// <summary>
        /// Allows case insensitive checks
        /// </summary>
        public static bool Contains(this string source, string check, StringComparison comp)
        {
            return source.IndexOf(check, comp) >= 0;
        }
    }
}