using Anvil.API;
using NWN.Core;

namespace NWN.Xenomech.API.BaseTypes
{
    public partial class Effect
    {
        public IntPtr Handle;
        public Effect(IntPtr handle) => Handle = handle;
        ~Effect() { VM.FreeGameDefinedStructure((int)EngineStructure.Effect, Handle); }

        public static implicit operator IntPtr(Effect effect) => effect.Handle;
        public static implicit operator Effect(IntPtr intPtr) => new Effect(intPtr);
    }
}
