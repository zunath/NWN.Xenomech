using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI
{
    internal class CachedCreatureFeats: Dictionary<AbilityCategoryType, Dictionary<AITargetType, HashSet<FeatType>>>
    {
    }
}
