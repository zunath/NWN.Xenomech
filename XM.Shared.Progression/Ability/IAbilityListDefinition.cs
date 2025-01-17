using System.Collections.Generic;
using XM.Shared.API.Constants;

namespace XM.Progression.Ability
{
    internal interface IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
