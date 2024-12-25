
using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWScript.Enum;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        ///   Instantly gives this creature the benefits of a rest (restored hitpoints, spells, feats, etc..)
        /// </summary>
        public static void ForceRest(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(775);
        }

        /// <summary>
        ///   * Returns TRUE if oCreature is resting.
        /// </summary>
        public static bool GetIsResting(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(505);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the last PC that has rested in the module.
        /// </summary>
        public static uint GetLastPCRested()
        {
            NWNXPInvoke.CallBuiltIn(506);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Determine the type (REST_EVENTTYPE_REST_*) of the last rest event (as
        ///   returned from the OnPCRested module event).
        /// </summary>
        public static RestEventType GetLastRestEventType()
        {
            NWNXPInvoke.CallBuiltIn(508);
            return (RestEventType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   The creature will rest if not in combat and no enemies are nearby.
        ///   - bCreatureToEnemyLineOfSightCheck: TRUE to allow the creature to rest if enemies
        ///   are nearby, but the creature can't see the enemy.
        ///   FALSE the creature will not rest if enemies are
        ///   nearby regardless of whether or not the creature
        ///   can see them, such as if an enemy is close by,
        ///   but is in a different room behind a closed door.
        /// </summary>
        public static void ActionRest(bool bCreatureToEnemyLineOfSightCheck = false)
        {
            NWNXPInvoke.StackPushInteger(bCreatureToEnemyLineOfSightCheck ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(402);
        }
    }
}
