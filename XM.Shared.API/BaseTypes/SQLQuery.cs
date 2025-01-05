using NWN.Core;
using XM.Shared.API;

namespace XM.Shared.API.BaseTypes
{
    public partial class SQLQuery
    {
        public nint Handle;
        public SQLQuery(nint handle) => Handle = handle;
        ~SQLQuery() { VM.FreeGameDefinedStructure((int)EngineStructure.SQLQuery, Handle); }

        public static implicit operator nint(SQLQuery sqlQuery) => sqlQuery.Handle;
        public static implicit operator SQLQuery(nint intPtr) => new SQLQuery(intPtr);
    }
}
