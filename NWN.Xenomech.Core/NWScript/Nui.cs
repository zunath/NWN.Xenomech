using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWScript.Enum;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        /// Create a NUI window from the given resref(.jui) for the given player.
        /// * The resref needs to be available on the client, not the server.
        /// * The token is an integer for ease of handling only. You are not supposed to do anything with it, except store/pass it.
        /// * The window ID needs to be alphanumeric and short. Only one window (per client) with the same ID can exist at a time.
        ///   Re-creating a window with the same id of one already open will immediately close the old one.
        /// * sEventScript is optional and overrides the NUI module event for this window only.
        /// * See nw_inc_nui.nss for full documentation.
        /// Returns the window token on success (>0), or 0 on error.
        /// </summary>
        public static int NuiCreateFromResRef(uint oPlayer, string sResRef, string sWindowId = "", string sEventScript = "")
        {
            NWNXPInvoke.StackPushString(sEventScript);
            NWNXPInvoke.StackPushString(sWindowId);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1010);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Create a NUI window inline for the given player.
        /// * The token is an integer for ease of handling only. You are not supposed to do anything with it, except store/pass it.
        /// * The window ID needs to be alphanumeric and short. Only one window (per client) with the same ID can exist at a time.
        ///   Re-creating a window with the same id of one already open will immediately close the old one.
        /// * sEventScript is optional and overrides the NUI module event for this window only.
        /// * See nw_inc_nui.nss for full documentation.
        /// Returns the window token on success (>0), or 0 on error.
        /// </summary>
        public static int NuiCreate(uint oPlayer, Json jNui, string sWindowId = "", string sEventScript = "")
        {
            NWNXPInvoke.StackPushString(sEventScript);
            NWNXPInvoke.StackPushString(sWindowId);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jNui);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1011);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// You can look up windows by ID, if you gave them one.
        /// * Windows with an ID present are singletons - attempting to open a second one with the same ID
        ///   will fail, even if the json definition is different.
        /// Returns the token if found, or 0.
        /// </summary>
        public static int NuiFindWindow(uint oPlayer, string sId)
        {
            NWNXPInvoke.StackPushString(sId);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1012);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Destroys the given window, by token, immediately closing it on the client.
        /// Does nothing if nUiToken does not exist on the client.
        /// Does not send a close event - this immediately destroys all serverside state.
        /// The client will close the window asynchronously.
        /// </summary>
        public static void NuiDestroy(uint oPlayer, int nUiToken)
        {
            NWNXPInvoke.StackPushInteger(nUiToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1013);
        }

        /// <summary>
        /// Returns the originating player of the current event.
        /// </summary>
        public static uint NuiGetEventPlayer()
        {
            NWNXPInvoke.CallBuiltIn(1014);

            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Gets the window token of the current event (or 0 if not in an event).
        /// </summary>
        public static int NuiGetEventWindow()
        {
            NWNXPInvoke.CallBuiltIn(1015);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the event type of the current event.
        /// * See nw_inc_nui.nss for full documentation of all events.
        /// </summary>
        public static string NuiGetEventType()
        {
            NWNXPInvoke.CallBuiltIn(1016);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Returns the ID of the widget that triggered the event.
        /// </summary>
        public static string NuiGetEventElement()
        {
            NWNXPInvoke.CallBuiltIn(1017);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Get the array index of the current event.
        /// This can be used to get the index into an array, for example when rendering lists of buttons.
        /// Returns -1 if the event is not originating from within an array.
        /// </summary>
        public static int NuiGetEventArrayIndex()
        {
            NWNXPInvoke.CallBuiltIn(1018);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the window ID of the window described by nUiToken.
        /// Returns "" on error, or if the window has no ID.
        /// </summary>
        public static string NuiGetWindowId(uint oPlayer, int nUiToken)
        {
            NWNXPInvoke.StackPushInteger(nUiToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1019);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Gets the JSON value for the given player, token, and bind.
        /// * JSON values can hold all kinds of values; but NUI widgets require specific bind types.
        ///   It is up to you to either handle this in NWScript, or just set compatible bind types.
        ///   No auto-conversion happens.
        /// Returns a JSON null value if the bind does not exist.
        /// </summary>
        public static Json NuiGetBind(uint oPlayer, int nUiToken, string sBindName)
        {
            NWNXPInvoke.StackPushString(sBindName);
            NWNXPInvoke.StackPushInteger(nUiToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1020);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Sets a JSON value for the given player, token, and bind.
        /// The value is synced down to the client and can be used in UI binding.
        /// When the UI changes the value, it is returned to the server and can be retrieved via NuiGetBind().
        /// * JSON values can hold all kinds of values; but NUI widgets require specific bind types.
        ///   It is up to you to either handle this in NWScript, or just set compatible bind types.
        ///   No auto-conversion happens.
        /// * If the bind is on the watch list, this will immediately invoke the event handler with the "watch"
        ///   event type; even before this function returns. Do not update watched binds from within the watch handler
        ///   unless you enjoy stack overflows.
        /// Does nothing if the given player+token is invalid.
        /// </summary>
        public static void NuiSetBind(uint oPlayer, int nUiToken, string sBindName, Json jValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushString(sBindName);
            NWNXPInvoke.StackPushInteger(nUiToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1021);
        }

        /// <summary>
        /// Swaps out the given element (by id) with the given NUI layout (partial).
        /// * This currently only works with the "group" element type, and the special "_window_" root group.
        /// </summary>
        public static void NuiSetGroupLayout(uint oPlayer, int nUiToken, string sElement, Json jNui)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jNui);
            NWNXPInvoke.StackPushString(sElement);
            NWNXPInvoke.StackPushInteger(nUiToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1022);
        }

        /// <summary>
        /// Mark the given bind name as watched.
        /// A watched bind will invoke the NUI script event every time its value changes.
        /// Be careful with binding NUI data inside a watch event handler: It's easy to accidentally recurse yourself into a stack overflow.
        /// </summary>
        public static int NuiSetBindWatch(uint oPlayer, int nUiToken, string sBind, bool bWatch)
        {
            NWNXPInvoke.StackPushInteger(bWatch ? 1 : 0);
            NWNXPInvoke.StackPushString(sBind);
            NWNXPInvoke.StackPushInteger(nUiToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1023);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the nNth window token of the player, or 0.
        /// nNth starts at 0.
        /// Iterator is not write-safe: Calling DestroyWindow() will invalidate moving following offsets by one.
        /// </summary>
        public static int NuiGetNthWindow(uint oPlayer, int nNth = 0)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1024);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Return the nNth bind name of the given window, or "".
        /// If bWatched is TRUE, iterates only watched binds.
        /// If FALSE, iterates all known binds on the window (either set locally or in UI).
        /// </summary>
        public static string NuiGetNthBind(uint oPlayer, int nToken, bool bWatched, int nNth = 0)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushInteger(bWatched ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1025);

            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Returns the event payload, specific to the event.
        /// Returns JsonNull if the event has no payload.
        /// </summary>
        public static Json NuiGetEventPayload()
        {
            NWNXPInvoke.CallBuiltIn(1026);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Get the userdata of the given window token.
        /// Returns JsonNull if the window does not exist on the given player, or has no userdata set.
        /// </summary>
        public static Json NuiGetUserData(uint oPlayer, int nToken)
        {
            NWNXPInvoke.StackPushInteger(nToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1027);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        /// Sets an arbitrary JSON value as userdata on the given window token.
        /// This userdata is not read or handled by the game engine and not sent to clients.
        /// This mechanism only exists as a convenience for the programmer to store data bound to a windows' lifecycle.
        /// Will do nothing if the window does not exist.
        /// </summary>
        public static void NuiSetUserData(uint oPlayer, int nToken, Json jUserData)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jUserData);
            NWNXPInvoke.StackPushInteger(nToken);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1028);
        }

    }
}
