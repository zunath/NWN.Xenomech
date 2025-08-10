using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;
using XM.Shared.Core.Data;

namespace XM.Plugin.Craft.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerCraft: EntityBase
    {
        public PlayerCraft()
        {
            LearnedRecipes = new HashSet<RecipeType>();
        }

        public PlayerCraft(string playerId)
        {
            Id = playerId;
            LearnedRecipes = new HashSet<RecipeType>();
        }

        public HashSet<RecipeType> LearnedRecipes { get; set; }
        public SkillType PrimaryCraftSkill { get; set; }
        public SkillType SecondaryCraftSkill { get; set; }
    }
}
