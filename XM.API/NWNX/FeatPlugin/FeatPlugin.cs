using XM.API.Constants;

namespace XM.API.NWNX.FeatPlugin
{
    public static class FeatPlugin
    {
        /// <summary>
        /// Sets a feat modifier.
        /// </summary>
        /// <param name="featId">The Feat constant or value in feat.2da.</param>
        /// <param name="modifierType">The feat modifier to set.</param>
        /// <param name="param1">The first parameter for the feat modifier. Default is -559038737.</param>
        /// <param name="param2">The second parameter for the feat modifier. Default is -559038737.</param>
        /// <param name="param3">The third parameter for the feat modifier. Default is -559038737.</param>
        /// <param name="param4">The fourth parameter for the feat modifier. Default is -559038737.</param>
        public static void SetFeatModifier(FeatType featId, FeatModifierType modifierType, int param1 = -559038737, int param2 = -559038737, int param3 = -559038737, int param4 = -559038737)
        {
            NWN.Core.NWNX.FeatPlugin.SetFeatModifier((int)featId, (int)modifierType, param1, param2, param3, param4);
        }

    }
}
