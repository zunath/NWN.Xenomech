using NWN.Xenomech.Core.Interop;

// ReSharper disable once CheckNamespace
namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {
        /// <summary>
        /// Make oTarget run sScript and then return execution to the calling script.
        /// If sScript does not specify a compiled script, nothing happens.
        ///
        /// This command will make a round-trip back to the NWN context so it should only be used
        /// in situations where you are guaranteed to have an NWScript file in the module, for performance reasons.
        /// </summary>
        public static void ExecuteScriptNWScript(string sScript, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushString(sScript);
            NWNXPInvoke.CallBuiltIn(8);
        }

        /// <summary>
        /// Make oTarget run sScript and then return execution to the calling script.
        /// If sScript does not specify a compiled script, nothing happens.
        ///
        /// This command will bypass the NWN context. For this reason it can only execute C# event scripts.
        /// Use ExecuteScriptNWScript if you actually need to run something in the module.
        /// </summary>
        public static void ExecuteScript(string sScript, uint oTarget)
        {
            // Note: Bypass the NWScript round-trip and directly call the script execution.
            Internal.DirectRunScript(sScript, oTarget);
        }
    }
}
