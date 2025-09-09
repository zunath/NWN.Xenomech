using XM.Shared.Core.Localization;

namespace XM.Plugin.Mech
{
    public enum MechPartType
    {
        [MechPartType(LocaleString.Invalid, false, "")]
        Invalid = 0,
        [MechPartType(LocaleString.Head, true, "mech_head")]
        Head = 1,
        [MechPartType(LocaleString.MechCore, true, "mech_core")]
        Core = 2,
        [MechPartType(LocaleString.MechLeftArm, true, "mech_larm")]
        LeftArm = 3,
        [MechPartType(LocaleString.MechRightArm, true, "mech_rarm")]
        RightArm = 4,
        [MechPartType(LocaleString.MechLegs, true, "mech_legs")]
        Legs = 5,
        [MechPartType(LocaleString.MechGenerator, true, "mech_generator")]
        Generator = 6,
        [MechPartType(LocaleString.MechLeftWeapon, true, "mech_lweapon")]
        LeftWeapon = 7,
        [MechPartType(LocaleString.MechRightWeapon, true, "mech_rweapon")]
        RightWeapon = 8
    }
}