using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        /// Determine whether the specified object is in conversation.
        /// * Returns true if the object is in conversation, false otherwise.
        /// </summary>
        public static bool IsInConversation(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(445);
            return NWNXPInvoke.StackPopInteger() != 0;
        }
    }
}