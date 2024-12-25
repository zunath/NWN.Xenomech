using NWN.Xenomech.Core.NWScript.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        /// Returns true if the specified object (a placeable or a door) is currently open.
        /// </summary>
        public static bool GetIsOpen(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(443);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        /// The action subject will unlock the specified target, which can be a door or a placeable object.
        /// </summary>
        public static void ActionUnlockObject(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(483);
        }

        /// <summary>
        /// The action subject will lock the specified target, which can be a door or a placeable object.
        /// </summary>
        public static void ActionLockObject(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(484);
        }

        /// <summary>
        /// Causes the action subject to open the specified door.
        /// </summary>
        public static void ActionOpenDoor(uint oDoor)
        {
            NWNXPInvoke.StackPushObject(oDoor);
            NWNXPInvoke.CallBuiltIn(43);
        }

        /// <summary>
        /// Causes the action subject to close the specified door.
        /// </summary>
        public static void ActionCloseDoor(uint oDoor)
        {
            NWNXPInvoke.StackPushObject(oDoor);
            NWNXPInvoke.CallBuiltIn(44);
        }

        /// <summary>
        /// Get the last blocking door encountered by the caller of this function.
        /// * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetBlockingDoor()
        {
            NWNXPInvoke.CallBuiltIn(336);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Checks if the specified door action can be performed on the target door.
        /// </summary>
        /// <param name="oTargetDoor">The target door to check.</param>
        /// <param name="nDoorAction">The door action to check (DOOR_ACTION_*).</param>
        /// <returns>True if the door action is possible; otherwise, false.</returns>
        public static bool GetIsDoorActionPossible(uint oTargetDoor, DoorAction nDoorAction)
        {
            NWNXPInvoke.StackPushInteger((int)nDoorAction);
            NWNXPInvoke.StackPushObject(oTargetDoor);
            NWNXPInvoke.CallBuiltIn(337);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        /// Performs the specified door action on the target door.
        /// </summary>
        /// <param name="oTargetDoor">The target door to act upon.</param>
        /// <param name="nDoorAction">The door action to perform (DOOR_ACTION_*).</param>
        public static void DoDoorAction(uint oTargetDoor, DoorAction nDoorAction)
        {
            NWNXPInvoke.StackPushInteger((int)nDoorAction);
            NWNXPInvoke.StackPushObject(oTargetDoor);
            NWNXPInvoke.CallBuiltIn(338);
        }
    }
}
