using NWN.Core.NWNX;
using XM.API.Constants;

namespace XM.API.NWNX.WeaponPlugin
{
    public static class WeaponPlugin
    {
        /// <summary>
        /// Sets the weapon focus feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponFocusFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponFocusFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the required creature size for a weapon base item to be finessable.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nSize">The creature size minimum to consider this weapon finessable.</param>
        public static void SetWeaponFinesseSize(BaseItemType nBaseItem, CreatureSizeType nSize)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponFinesseSize((int)nBaseItem, (int)nSize);
        }

        /// <summary>
        /// Gets the required creature size for a weapon base item to be finessable.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <returns>The required creature size.</returns>
        public static CreatureSizeType GetWeaponFinesseSize(BaseItemType nBaseItem)
        {
            return (CreatureSizeType)NWN.Core.NWNX.WeaponPlugin.GetWeaponFinesseSize((int)nBaseItem);
        }

        /// <summary>
        /// Sets the weapon base item to be considered as unarmed for weapon finesse feat.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        public static void SetWeaponUnarmed(BaseItemType nBaseItem)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponUnarmed((int)nBaseItem);
        }

        /// <summary>
        /// Sets the weapon improved critical feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponImprovedCriticalFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponImprovedCriticalFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the weapon specialization feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponSpecializationFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponSpecializationFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the epic weapon focus feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponFocusFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponFocusFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the epic weapon specialization feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponSpecializationFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponSpecializationFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the epic weapon overwhelming critical feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponOverwhelmingCriticalFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponOverwhelmingCriticalFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the epic weapon devastating critical feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponDevastatingCriticalFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponDevastatingCriticalFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the weapon of choice feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponOfChoiceFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponOfChoiceFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the greater weapon specialization feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetGreaterWeaponSpecializationFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetGreaterWeaponSpecializationFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the greater weapon focus feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetGreaterWeaponFocusFeat(BaseItemType nBaseItem, FeatType nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetGreaterWeaponFocusFeat((int)nBaseItem, (int)nFeat);
        }

        /// <summary>
        /// Sets the weapon plugin options.
        /// </summary>
        /// <param name="nOption">The option to change from WeaponOptionType.</param>
        /// <param name="nVal">The new value of the option.</param>
        public static void SetOption(WeaponOptionType nOption, int nVal)
        {
            NWN.Core.NWNX.WeaponPlugin.SetOption((int)nOption, nVal);
        }

        /// <summary>
        /// Sets the devastating critical event script.
        /// </summary>
        /// <param name="sScript">The script to call when a devastating critical occurs.</param>
        public static void SetDevastatingCriticalEventScript(string sScript)
        {
            NWN.Core.NWNX.WeaponPlugin.SetDevastatingCriticalEventScript(sScript);
        }

        /// <summary>
        /// Gets the devastating critical event data.
        /// </summary>
        /// <returns>The devastating critical event data.</returns>
        public static DevastatingCriticalEvent_Data GetDevastatingCriticalEventData()
        {
            return NWN.Core.NWNX.WeaponPlugin.GetDevastatingCriticalEventData();
        }

        /// <summary>
        /// Bypasses the devastating critical event.
        /// </summary>
        public static void BypassDevastatingCritical()
        {
            NWN.Core.NWNX.WeaponPlugin.BypassDevastatingCritical();
        }

        /// <summary>
        /// Sets the weapon to gain a .5 strength bonus.
        /// </summary>
        /// <param name="oWeapon">The melee weapon.</param>
        /// <param name="nEnable">TRUE for bonus, FALSE to turn off the bonus.</param>
        /// <param name="bPersist">Whether the two-hand state should persist to the GFF file.</param>
        public static void SetOneHalfStrength(uint oWeapon, int nEnable, bool bPersist = false)
        {
            NWN.Core.NWNX.WeaponPlugin.SetOneHalfStrength(oWeapon, nEnable, bPersist ? 1 : 0);
        }

        /// <summary>
        /// Gets if the weapon is set to gain the additional .5 strength bonus.
        /// </summary>
        /// <param name="oWeapon">The weapon.</param>
        /// <returns>TRUE if the weapon gains the bonus, FALSE otherwise.</returns>
        public static bool GetOneHalfStrength(uint oWeapon)
        {
            return NWN.Core.NWNX.WeaponPlugin.GetOneHalfStrength(oWeapon) == 1;
        }

        /// <summary>
        /// Overrides the max attack distance of ranged weapons.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="fMax">The maximum attack distance.</param>
        /// <param name="fMaxPassive">The maximum passive attack distance.</param>
        /// <param name="fPreferred">The preferred attack distance.</param>
        public static void SetMaxRangedAttackDistanceOverride(BaseItemType nBaseItem, float fMax, float fMaxPassive, float fPreferred)
        {
            NWN.Core.NWNX.WeaponPlugin.SetMaxRangedAttackDistanceOverride((int)nBaseItem, fMax, fMaxPassive, fPreferred);
        }
    }
}
