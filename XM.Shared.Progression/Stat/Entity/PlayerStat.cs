using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Data;

namespace XM.Progression.Stat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerStat : EntityBase
    {
        public PlayerStat()
        {
            Init();
        }

        public PlayerStat(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {
            BaseAttributes = new Dictionary<AbilityType, int>
            {
                {AbilityType.Might, 0},
                {AbilityType.Perception, 0},
                {AbilityType.Vitality, 0},
                {AbilityType.Agility, 0},
                {AbilityType.Willpower, 0},
                {AbilityType.Social, 0},
            };

            Attributes = new Dictionary<AbilityType, int>
            {
                {AbilityType.Might, 0},
                {AbilityType.Perception, 0},
                {AbilityType.Vitality, 0},
                {AbilityType.Agility, 0},
                {AbilityType.Willpower, 0},
                {AbilityType.Social, 0},
            };

            Resists = new ResistCollection();
        }

        public int MaxHP { get; set; }
        public int MaxEP { get; set; }
        public int HP { get; set; }
        public int HPRegen { get; set; }
        public int EP { get; set; }
        public int EPRegen { get; set; }
        public int AbilityRecastReduction { get; set; }
        public int Defense { get; set; }
        public int Evasion { get; set; }
        public int Accuracy { get; set; }
        public int Attack { get; set; }
        public int EtherAttack { get; set; }
        public Dictionary<AbilityType, int> BaseAttributes { get; set; }
        public Dictionary<AbilityType, int> Attributes { get; set; }
        public Dictionary<ResistType, int> Resists { get; set; }

    }
}
