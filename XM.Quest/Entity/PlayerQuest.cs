using Anvil.Services;
using System.Collections.Generic;
using XM.Data.Shared;

namespace XM.Quest.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerQuest: EntityBase
    {
        public Dictionary<string, PlayerQuestDetail> Quests { get; set; }

        public PlayerQuest()
        {
            Quests = new Dictionary<string, PlayerQuestDetail>();
        }

        public PlayerQuest(string playerId)
        {
            Id = playerId;
            Quests = new Dictionary<string, PlayerQuestDetail>();
        }

    }
}
