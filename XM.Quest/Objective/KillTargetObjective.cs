 using Anvil.Services;
 using XM.Core;
 using XM.Quest.Entity;

 namespace XM.Quest.Objective
{
    [ServiceBinding(typeof(KillTargetObjective))]
    internal class KillTargetObjective : IQuestObjective
    {
        public QuestNPCGroupType Group { get; set; }
        public int Quantity { get; set; }


        private readonly DBService _db;

        private readonly QuestService _quest;

        public KillTargetObjective(
            DBService db,
            QuestService quest)
        {
            _db = db;
            _quest = quest;
        }

        public void Initialize(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : new PlayerQuestDetail();

            quest.KillProgresses[Group] = Quantity;
            dbPlayer.Quests[questId] = quest;
            _db.Set(dbPlayer);
        }

        public void Advance(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : null;

            if (quest == null) return;
            if (!quest.KillProgresses.ContainsKey(Group)) return;
            if (quest.KillProgresses[Group] <= 0) return;

            quest.KillProgresses[Group]--;
            _db.Set(dbPlayer);

            var npcGroup = _quest.GetQuestNPCGroup(Group);
            var questDetail = _quest.GetQuestById(questId);

            var statusMessage = $"[{questDetail.Name}] {npcGroup.Name} remaining: {quest.KillProgresses[Group]}";

            if (quest.KillProgresses[Group] <= 0)
            {
                statusMessage += $" {ColorToken.Green("{COMPLETE}")}";
            }

            SendMessageToPC(player, statusMessage);
        }

        public bool IsComplete(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : null;

            if (quest == null) return false;

            foreach (var progress in quest.KillProgresses.Values)
            {
                if (progress > 0)
                    return false;
            }

            return true;
        }

        public string GetCurrentStateText(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            if (!dbPlayer.Quests.ContainsKey(questId))
                return "N/A";

            var npcGroup = _quest.GetQuestNPCGroup(Group);
            var numberRemaining = dbPlayer.Quests[questId].KillProgresses[Group];

            return $"{Quantity - numberRemaining} / {Quantity} {npcGroup.Name}";
        }
    }
}
