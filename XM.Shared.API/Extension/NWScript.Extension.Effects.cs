// ReSharper disable once CheckNamespace
namespace XM.Shared.API
{
    public partial class NWScript
    {
        /// <summary>
        /// Removes all effects with the specified tag(s) from a creature.
        /// </summary>
        /// <param name="creature">The creature to remove effects from.</param>
        /// <param name="tags">The tags to look for.</param>
        public static void RemoveEffectByTag(uint creature, params string[] tags)
        {
            for (var effect = GetFirstEffect(creature); GetIsEffectValid(effect); effect = GetNextEffect(creature))
            {
                var effectTag = GetEffectTag(effect);
                if (tags.Contains(effectTag))
                {
                    RemoveEffect(creature, effect);
                }
            }
        }

        /// <summary>
        /// Determines if creature has at least one effect with the specified tags.
        /// </summary>
        /// <param name="creature">The creature to check</param>
        /// <param name="tags">The effect tags to check for</param>
        /// <returns>true if at least one effect was found, false otherwise</returns>
        public static bool HasEffectByTag(uint creature, params string[] tags)
        {
            for (var effect = GetFirstEffect(creature); GetIsEffectValid(effect); effect = GetNextEffect(creature))
            {
                var effectTag = GetEffectTag(effect);
                if (tags.Contains(effectTag))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
