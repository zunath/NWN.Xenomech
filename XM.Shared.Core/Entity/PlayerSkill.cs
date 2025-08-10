using System.Collections.Generic;
using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerSkill : EntityBase
    {
        public PlayerSkill()
        {
            Init();
        }

        public PlayerSkill(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {
            Skills = new Dictionary<int, int>();
        }

        // Key: SkillType code (int), Value: skill level
        public Dictionary<int, int> Skills { get; set; }
    }
}