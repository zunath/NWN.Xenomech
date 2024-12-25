using NWN.Xenomech.Core.Interop;

// ReSharper disable once CheckNamespace
namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {
        /// <summary>
        ///   Destroy oObject (irrevocably).
        ///   This will not work on modules and areas.
        /// </summary>
        public static void DestroyObject(uint oDestroy, float fDelay = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fDelay);
            NWNXPInvoke.StackPushObject(oDestroy);
            NWNXPInvoke.CallBuiltIn(241);
            
            ExecuteScript("object_destroyed", oDestroy);
        }
    }
}