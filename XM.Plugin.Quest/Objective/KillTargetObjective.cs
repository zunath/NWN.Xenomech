using Anvil.Services;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Entity.Quest;
using XM.Shared.Core;
using XM.Shared.Core.Data;

namespace XM.Quest.Objective
{
    internal class KillTargetObjective : IQuestObjective
    {
        public QuestNPCGroupType Group { get; set; }
        public int Quantity { get; set; }


        private readonly DBService _db;
        private readonly QuestNPCService _questNPC;

        public KillTargetObjective(
            DBService db,
            QuestNPCService questNPC)
        {
            _db = db;
            _questNPC = questNPC;
        }

        public void Initialize(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : new PlayerQuestDetail();

            quest.KillProgresses[(int)Group] = Quantity;
            dbPlayer.Quests[questId] = quest;
            _db.Set(dbPlayer);
        }

        public void Advance(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : null;

            if (quest == null) return;
            if (!quest.KillProgresses.ContainsKey((int)Group)) return;
            if (quest.KillProgresses[(int)Group] <= 0) return;

            quest.KillProgresses[(int)Group]--;
            _db.Set(dbPlayer);

            var npcGroup = _questNPC.GetQuestNPCGroup(Group);

            var statusMessage = $"[{npcGroup.Name} remaining: {quest.KillProgresses[(int)Group]}";

            if (quest.KillProgresses[(int)Group] <= 0)
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

            var npcGroup = _questNPC.GetQuestNPCGroup(Group);
            var numberRemaining = dbPlayer.Quests[questId].KillProgresses[(int)Group];

            return $"{Quantity - numberRemaining} / {Quantity} {npcGroup.Name}";
        }
    }
}
