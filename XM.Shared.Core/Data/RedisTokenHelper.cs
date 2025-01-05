namespace XM.Shared.Core.Data
{
    public static class RedisTokenHelper
    {
        /// <summary>
        /// Escapes tokens used in Redis queries.
        /// </summary>
        /// <param name="str">The string to escape</param>
        /// <returns>A string containing escaped tokens.</returns>
        public static string EscapeTokens(string str)
        {
            return str
                .Replace("@", "\\@")
                .Replace("!", "\\!")
                .Replace("{", "\\{")
                .Replace("}", "\\}")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace("|", "\\|")
                .Replace("-", "\\-")
                .Replace("=", "\\=")
                .Replace(">", "\\>")
                .Replace("'", "\\'")
                .Replace("\"", "\\\"");
        }
    }
}
