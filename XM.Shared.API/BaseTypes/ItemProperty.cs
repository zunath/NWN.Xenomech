using NWN.Core;
using NWNX.NET;

namespace XM.Shared.API.BaseTypes
{
    public partial class ItemProperty
    {
        public nint Handle;
        public ItemProperty(nint handle) => Handle = handle;
        ~ItemProperty() { NWNXAPI.FreeGameDefinedStructure((int)EngineStructure.ItemProperty, Handle); }

        public static implicit operator nint(ItemProperty effect) => effect.Handle;
        public static implicit operator ItemProperty(nint intPtr) => new ItemProperty(intPtr);
    }
}
