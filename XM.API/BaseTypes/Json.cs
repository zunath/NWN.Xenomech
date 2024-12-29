using NWN.Core;

namespace XM.API.BaseTypes
{
    public partial class Json
    {
        public IntPtr Handle;
        public Json(IntPtr handle) => Handle = handle;
        ~Json() { VM.FreeGameDefinedStructure((int)EngineStructure.Json, Handle); }

        public static implicit operator IntPtr(Json json) => json.Handle;
        public static implicit operator Json(IntPtr intPtr) => new Json(intPtr);
    }
}
