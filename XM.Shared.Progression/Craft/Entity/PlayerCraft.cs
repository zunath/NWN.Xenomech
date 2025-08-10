using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;
using XM.Shared.Core.Data;

namespace XM.Progression.Craft.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerCraft : EntityBase
    {
        public PlayerCraft()
        {
            LearnedRecipes = new HashSet<int>();
        }

        public PlayerCraft(string playerId)
        {
            Id = playerId;
            LearnedRecipes = new HashSet<int>();
        }

        // Store recipe identifiers as ints to avoid plugin dependency
        public HashSet<int> LearnedRecipes { get; set; }
        public SkillType PrimaryCraftSkill { get; set; }
        public SkillType SecondaryCraftSkill { get; set; }
    }
}


