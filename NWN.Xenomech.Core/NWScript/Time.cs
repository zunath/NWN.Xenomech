
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        ///   * Returns TRUE if it is currently day.
        /// </summary>
        public static bool GetIsDay()
        {
            NWNXPInvoke.CallBuiltIn(405);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   * Returns TRUE if it is currently night.
        /// </summary>
        public static bool GetIsNight()
        {
            NWNXPInvoke.CallBuiltIn(406);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   * Returns TRUE if it is currently dawn.
        /// </summary>
        public static bool GetIsDawn()
        {
            NWNXPInvoke.CallBuiltIn(407);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   * Returns TRUE if it is currently dusk.
        /// </summary>
        public static bool GetIsDusk()
        {
            NWNXPInvoke.CallBuiltIn(408);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Convert nRounds into a number of seconds
        ///   A round is always 6.0 seconds
        /// </summary>
        public static float RoundsToSeconds(int nRounds)
        {
            NWNXPInvoke.StackPushInteger(nRounds);
            NWNXPInvoke.CallBuiltIn(121);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Convert nHours into a number of seconds
        ///   The result will depend on how many minutes there are per hour (game-time)
        /// </summary>
        public static float HoursToSeconds(int nHours)
        {
            NWNXPInvoke.StackPushInteger(nHours);
            NWNXPInvoke.CallBuiltIn(122);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Convert nTurns into a number of seconds
        ///   A turn is always 60.0 seconds
        /// </summary>
        public static float TurnsToSeconds(int nTurns)
        {
            NWNXPInvoke.StackPushInteger(nTurns);
            NWNXPInvoke.CallBuiltIn(123);
            return NWNXPInvoke.StackPopFloat();
        }
    }
}
