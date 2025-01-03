﻿using XM.Data;
using XM.Quest.Entity;

namespace XM.Quest.Prerequisite
{
    internal class RequiredQuestPrerequisite : IQuestPrerequisite
    {
        public string QuestId { get; set; }

        private readonly DBService _db;

        public RequiredQuestPrerequisite(DBService db)
        {
            _db = db;
        }

        public bool MeetsPrerequisite(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var timesCompleted = dbPlayer.Quests.ContainsKey(QuestId) ? dbPlayer.Quests[QuestId].TimesCompleted : 0;
            return timesCompleted > 0;
        }
    }
}
