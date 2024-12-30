using NWN.Core;

namespace XM.API.BaseTypes
{
    public partial class SQLQuery
    {
        public IntPtr Handle;
        public SQLQuery(IntPtr handle) => Handle = handle;
        ~SQLQuery() { VM.FreeGameDefinedStructure((int)EngineStructure.SQLQuery, Handle); }

        public static implicit operator IntPtr(SQLQuery sqlQuery) => sqlQuery.Handle;
        public static implicit operator SQLQuery(IntPtr intPtr) => new SQLQuery(intPtr);
    }
}
