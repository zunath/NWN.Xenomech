using NWN.Core;

namespace NWN.Xenomech.API.BaseTypes
{
    public partial class ItemProperty
    {
        public IntPtr Handle;
        public ItemProperty(IntPtr handle) => Handle = handle;
        ~ItemProperty() { VM.FreeGameDefinedStructure((int)EngineStructure.ItemProperty, Handle); }

        public static implicit operator IntPtr(ItemProperty effect) => effect.Handle;
        public static implicit operator ItemProperty(IntPtr intPtr) => new ItemProperty(intPtr);
    }
}
