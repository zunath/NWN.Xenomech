using XM.Quest.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Data;

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
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var timesCompleted = dbPlayer.Quests.ContainsKey(QuestId) ? dbPlayer.Quests[QuestId].TimesCompleted : 0;
            return timesCompleted > 0;
        }
    }
}
