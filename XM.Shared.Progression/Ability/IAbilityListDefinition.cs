using System.Collections.Generic;
using XM.Shared.API.Constants;

namespace XM.Progression.Ability
{
    public interface IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
