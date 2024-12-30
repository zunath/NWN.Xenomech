using XM.API;

namespace XM.Localization
{
    public static class Locale
    {
        /// <summary>
        /// Custom TLK files begin at 16777216.
        /// </summary>
        private const int CustomTlkIdStart = 16777216;

        /// <summary>
        /// Retrieves a localized string from the TLK file.
        /// Only strings from the module's custom TLK file can be used.
        /// </summary>
        /// <param name="stringId">The translated string Id</param>
        /// <param name="args">The variables to replace in the formatting of the text.</param>
        /// <returns>A localized string.</returns>
        public static string GetString(LocaleString stringId, params object[] args)
        {
            var tlkId = (int)stringId;

            if (tlkId < CustomTlkIdStart)
                tlkId += CustomTlkIdStart;

            var text = string.Format(NWScript.GetStringByStrRef(tlkId), args);
            return text;
        }
    }
}
