using NWN.Core.NWNX;

namespace NWN.Xenomech.API.NWNX.Weapon
{
    public static class WeaponPlugin
    {
        /// <summary>
        /// Sets the weapon focus feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponFocusFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponFocusFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the required creature size for a weapon base item to be finessable.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nSize">The creature size minimum to consider this weapon finessable.</param>
        public static void SetWeaponFinesseSize(int nBaseItem, int nSize)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponFinesseSize(nBaseItem, nSize);
        }

        /// <summary>
        /// Gets the required creature size for a weapon base item to be finessable.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <returns>The required creature size.</returns>
        public static int GetWeaponFinesseSize(int nBaseItem)
        {
            return NWN.Core.NWNX.WeaponPlugin.GetWeaponFinesseSize(nBaseItem);
        }

        /// <summary>
        /// Sets the weapon base item to be considered as unarmed for weapon finesse feat.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        public static void SetWeaponUnarmed(int nBaseItem)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponUnarmed(nBaseItem);
        }

        /// <summary>
        /// Sets the weapon improved critical feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponImprovedCriticalFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponImprovedCriticalFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the weapon specialization feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponSpecializationFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponSpecializationFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the epic weapon focus feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponFocusFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponFocusFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the epic weapon specialization feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponSpecializationFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponSpecializationFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the epic weapon overwhelming critical feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponOverwhelmingCriticalFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponOverwhelmingCriticalFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the epic weapon devastating critical feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetEpicWeaponDevastatingCriticalFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetEpicWeaponDevastatingCriticalFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the weapon of choice feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetWeaponOfChoiceFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponOfChoiceFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the greater weapon specialization feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetGreaterWeaponSpecializationFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetGreaterWeaponSpecializationFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the greater weapon focus feat for a base item.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="nFeat">The feat to set.</param>
        public static void SetGreaterWeaponFocusFeat(int nBaseItem, int nFeat)
        {
            NWN.Core.NWNX.WeaponPlugin.SetGreaterWeaponFocusFeat(nBaseItem, nFeat);
        }

        /// <summary>
        /// Sets the weapon as a monk weapon.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        [Obsolete("Use baseitems.2da instead. This will be removed in future NWNX releases.")]
        public static void SetWeaponIsMonkWeapon(int nBaseItem)
        {
            NWN.Core.NWNX.WeaponPlugin.SetWeaponIsMonkWeapon(nBaseItem);
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
        public static void SetOneHalfStrength(uint oWeapon, int nEnable, int bPersist = 0)
        {
            NWN.Core.NWNX.WeaponPlugin.SetOneHalfStrength(oWeapon, nEnable, bPersist);
        }

        /// <summary>
        /// Gets if the weapon is set to gain the additional .5 strength bonus.
        /// </summary>
        /// <param name="oWeapon">The weapon.</param>
        /// <returns>TRUE if the weapon gains the bonus, FALSE otherwise.</returns>
        public static int GetOneHalfStrength(uint oWeapon)
        {
            return NWN.Core.NWNX.WeaponPlugin.GetOneHalfStrength(oWeapon);
        }

        /// <summary>
        /// Overrides the max attack distance of ranged weapons.
        /// </summary>
        /// <param name="nBaseItem">The base item id.</param>
        /// <param name="fMax">The maximum attack distance.</param>
        /// <param name="fMaxPassive">The maximum passive attack distance.</param>
        /// <param name="fPreferred">The preferred attack distance.</param>
        public static void SetMaxRangedAttackDistanceOverride(int nBaseItem, float fMax, float fMaxPassive, float fPreferred)
        {
            NWN.Core.NWNX.WeaponPlugin.SetMaxRangedAttackDistanceOverride(nBaseItem, fMax, fMaxPassive, fPreferred);
        }
    }
}
