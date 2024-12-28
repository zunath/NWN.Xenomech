using Anvil.Services;

namespace NWN.Xenomech.Core
{
    [ServiceBinding(typeof(Localization))]
    public class Localization
    {
        /// <summary>
        /// Custom TLK files begin at 16777216.
        /// </summary>
        private const int CustomTlkIdStart = 16777216;

        /// <summary>
        /// Retrieves a localized string from the TLK file.
        /// Only strings from the module's custom TLK file can be used.
        /// </summary>
        /// <param name="tlkId">The tlk row ID number</param>
        /// <param name="args">The variables to replace in the formatting of the text.</param>
        /// <returns>A localized string.</returns>
        public string GetString(int tlkId, params object[] args)
        {
            if (tlkId < CustomTlkIdStart)
                tlkId += CustomTlkIdStart;

            var text = string.Format(GetStringByStrRef(tlkId), args);
            return text;
        }
    }
}
