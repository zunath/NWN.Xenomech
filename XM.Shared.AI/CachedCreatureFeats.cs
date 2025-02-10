using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI
{
    internal class CachedCreatureFeats: Dictionary<AbilityClassificationType, Dictionary<AITargetType, HashSet<FeatType>>>
    {
    }
}
