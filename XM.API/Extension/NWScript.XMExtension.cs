// ReSharper disable once CheckNamespace
namespace XM.API
{
    public partial class NWScript
    {
        /// <summary>
        /// Set oObject's local boolean variable sVarName to nValue
        /// </summary>
        public static void SetLocalBool(uint oObject, string sVarName, bool nValue)
        {
            SetLocalInt(oObject, sVarName, Convert.ToInt32(nValue));
        }

        /// <summary>
        /// Get oObject's local boolean variable sVarName
        /// * Return value on error: false
        /// </summary>
        public static bool GetLocalBool(uint oObject, string sVarName)
        {
            return Convert.ToBoolean(GetLocalInt(oObject, sVarName));
        }

        /// <summary>
        /// Delete oObject's local boolean variable sVarName
        /// </summary>
        public static void DeleteLocalBool(uint oObject, string sVarName)
        {
            DeleteLocalInt(oObject, sVarName);
        }

    }
}
