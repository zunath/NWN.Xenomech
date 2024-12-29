namespace XM.API.NWNX.VisibilityPlugin
{
    public static class VisibilityPlugin
    {
        /// <summary>
        /// Queries the existing visibility override for the given (oPlayer, oTarget) pair.
        /// If oPlayer is OBJECT_INVALID, the global visibility override will be returned.
        /// </summary>
        /// <param name="oPlayer">The PC Object or OBJECT_INVALID.</param>
        /// <param name="oTarget">The object for which we're querying the visibility override.</param>
        /// <returns>The VisibilityType value.</returns>
        public static VisibilityType GetVisibilityOverride(uint oPlayer, uint oTarget)
        {
            return (VisibilityType)NWN.Core.NWNX.VisibilityPlugin.GetVisibilityOverride(oPlayer, oTarget);
        }

        /// <summary>
        /// Overrides the default visibility rules about how oPlayer perceives oTarget.
        /// If oPlayer is OBJECT_INVALID, the global visibility override will be set.
        /// </summary>
        /// <param name="oPlayer">The PC Object or OBJECT_INVALID.</param>
        /// <param name="oTarget">The object for which we're altering the visibility.</param>
        /// <param name="nOverride">The VisibilityType value.</param>
        public static void SetVisibilityOverride(uint oPlayer, uint oTarget, VisibilityType nOverride)
        {
            NWN.Core.NWNX.VisibilityPlugin.SetVisibilityOverride(oPlayer, oTarget, (int)nOverride);
        }
    }
}