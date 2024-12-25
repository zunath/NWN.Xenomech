using NWN.Xenomech.Core.NWScript.Enum;

namespace NWN.Xenomech.Core.Interop
{
    public partial class Effect
    {
        public nint Handle;
        public Effect(nint handle) => Handle = handle;
        ~Effect() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.Effect, Handle); }

        public static implicit operator nint(Effect effect) => effect.Handle;
        public static implicit operator Effect(nint intPtr) => new Effect(intPtr);
    }

    public partial class Event
    {
        public nint Handle;
        public Event(nint handle) => Handle = handle;
        ~Event() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.Event, Handle); }

        public static implicit operator nint(Event effect) => effect.Handle;
        public static implicit operator Event(nint intPtr) => new Event(intPtr);
    }

    public partial class Location
    {
        public nint Handle;
        public Location(nint handle) => Handle = handle;
        ~Location() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.Location, Handle); }

        public static implicit operator nint(Location effect) => effect.Handle;
        public static implicit operator Location(nint intPtr) => new Location(intPtr);
    }

    public partial class Talent
    {
        public nint Handle;
        public Talent(nint handle) => Handle = handle;
        ~Talent() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.Talent, Handle); }

        public static implicit operator nint(Talent effect) => effect.Handle;
        public static implicit operator Talent(nint intPtr) => new Talent(intPtr);
    }

    public partial class ItemProperty
    {
        public nint Handle;
        public ItemProperty(nint handle) => Handle = handle;
        ~ItemProperty() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.ItemProperty, Handle); }

        public static implicit operator nint(ItemProperty effect) => effect.Handle;
        public static implicit operator ItemProperty(nint intPtr) => new ItemProperty(intPtr);
    }

    public partial class SQLQuery
    {
        public nint Handle;
        public SQLQuery(nint handle) => Handle = handle;
        ~SQLQuery() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.SQLQuery, Handle); }

        public static implicit operator nint(SQLQuery sqlQuery) => sqlQuery.Handle;
        public static implicit operator SQLQuery(nint intPtr) => new SQLQuery(intPtr);
    }

    public partial class Cassowary
    {
        public nint Handle;
        public Cassowary(nint handle) => Handle = handle;
        ~Cassowary() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.Cassowary, Handle); }

        public static implicit operator nint(Cassowary cassowary) => cassowary.Handle;
        public static implicit operator Cassowary(nint intPtr) => new Cassowary(intPtr);
    }

    public partial class Json
    {
        public nint Handle;
        public Json(nint handle) => Handle = handle;
        ~Json() { NWNXPInvoke.FreeGameDefinedStructure((int)EngineStructure.Json, Handle); }

        public static implicit operator nint(Json json) => json.Handle;
        public static implicit operator Json(nint intPtr) => new Json(intPtr);
    }

    public delegate void ActionDelegate();
}