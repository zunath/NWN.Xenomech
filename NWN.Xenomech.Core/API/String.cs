using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {
        /// <summary>
        ///   Get the length of sString
        ///   * Return value on error: -1
        /// </summary>
        public static int GetStringLength(string sString)
        {
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(59);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Convert sString into upper case
        ///   * Return value on error: ""
        /// </summary>
        public static string GetStringUpperCase(string sString)
        {
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(60);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Convert sString into lower case
        ///   * Return value on error: ""
        /// </summary>
        public static string GetStringLowerCase(string sString)
        {
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(61);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get nCount characters from the right end of sString
        ///   * Return value on error: ""
        /// </summary>
        public static string GetStringRight(string sString, int nCount)
        {
            NWNXPInvoke.StackPushInteger(nCount);
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(62);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get nCounter characters from the left end of sString
        ///   * Return value on error: ""
        /// </summary>
        public static string GetStringLeft(string sString, int nCount)
        {
            NWNXPInvoke.StackPushInteger(nCount);
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(63);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Insert sString into sDestination at nPosition
        ///   * Return value on error: ""
        /// </summary>
        public static string InsertString(string sDestination, string sString, int nPosition)
        {
            NWNXPInvoke.StackPushInteger(nPosition);
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.StackPushString(sDestination);
            NWNXPInvoke.CallBuiltIn(64);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get nCount characters from sString, starting at nStart
        ///   * Return value on error: ""
        /// </summary>
        public static string GetSubString(string sString, int nStart, int nCount)
        {
            NWNXPInvoke.StackPushInteger(nCount);
            NWNXPInvoke.StackPushInteger(nStart);
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(65);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Find the position of sSubstring inside sString
        ///   - nStart: The character position to start searching at (from the left end of the string).
        ///   * Return value on error: -1
        /// </summary>
        public static int FindSubString(string sString, string sSubString, int nStart = 0)
        {
            NWNXPInvoke.StackPushInteger(nStart);
            NWNXPInvoke.StackPushString(sSubString);
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(66);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   * Returns TRUE if sStringToTest matches sPattern.
        /// </summary>
        public static int TestStringAgainstPattern(string sPattern, string sStringToTest)
        {
            NWNXPInvoke.StackPushString(sStringToTest);
            NWNXPInvoke.StackPushString(sPattern);
            NWNXPInvoke.CallBuiltIn(177);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the appropriate matched string (this should only be used in
        ///   OnConversation scripts).
        ///   * Returns the appropriate matched string, otherwise returns ""
        /// </summary>
        public static string GetMatchedSubstring(int nString)
        {
            NWNXPInvoke.StackPushInteger(nString);
            NWNXPInvoke.CallBuiltIn(178);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get the number of string parameters available.
        ///   * Returns -1 if no string matched (this could be because of a dialogue event)
        /// </summary>
        public static int GetMatchedSubstringsCount()
        {
            NWNXPInvoke.CallBuiltIn(179);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Replaces all matching sRegExp in sValue with sReplacement.
        /// * Returns a empty string on error.
        /// * Please see the format documentation for replacement patterns.
        /// * nSyntaxFlags is a mask of REGEXP_*
        /// * nMatchFlags is a mask of REGEXP_MATCH_* and REGEXP_FORMAT_*.
        /// * FORMAT_DEFAULT replacement patterns:
        ///    $$    $
        ///    $&    The matched substring.
        ///    $`    The portion of string that precedes the matched substring.
        ///    $'    The portion of string that follows the matched substring.
        ///    $n    The nth capture, where n is a single digit in the range 1 to 9 and $n is not followed by a decimal digit.
        ///    $nn   The nnth capture, where nn is a two-digit decimal number in the range 01 to 99.
        /// Example: RegExpReplace("a+", "vaaalue", "[$&]")    => "v[aaa]lue"
        /// </summary>
        public static string RegExpReplace(
            string sRegExp,
            string sValue,
            string sReplacement,
            RegularExpressionType nSyntaxFlags = RegularExpressionType.Ecmascript,
            RegularExpressionFormatType nMatchFlags = RegularExpressionFormatType.Default)
        {
            NWNXPInvoke.StackPushInteger((int)nMatchFlags);
            NWNXPInvoke.StackPushInteger((int)nSyntaxFlags);
            NWNXPInvoke.StackPushString(sReplacement);
            NWNXPInvoke.StackPushString(sValue);
            NWNXPInvoke.StackPushString(sRegExp);
            NWNXPInvoke.CallBuiltIn(1070);
            return NWNXPInvoke.StackPopString();
        }
    }
}
