using NWN.Core;

namespace XM.API.BaseTypes
{
    public partial class Talent
    {
        public IntPtr Handle;
        public Talent(IntPtr handle) => Handle = handle;
        ~Talent() { VM.FreeGameDefinedStructure((int)EngineStructure.Talent, Handle); }

        public static implicit operator IntPtr(Talent effect) => effect.Handle;
        public static implicit operator Talent(IntPtr intPtr) => new Talent(intPtr);
    }
}
