using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Skill;
using XM.Shared.Core.Data;

namespace XM.Progression.Job.Entity
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
            Skills = new SkillCollection();
        }

        public Dictionary<SkillType, int> Skills { get; set; }
    }
}