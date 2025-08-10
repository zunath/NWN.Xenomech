using System.Collections.Generic;
using Anvil.Services;

namespace XM.Shared.Core.Entity
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
        // SkillType codes represented as ints to avoid dependency on XM.Shared.Progression
        public int PrimaryCraftSkillCode { get; set; }
        public int SecondaryCraftSkillCode { get; set; }
    }
}


