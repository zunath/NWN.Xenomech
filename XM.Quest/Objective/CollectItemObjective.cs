using Anvil.Services;
using XM.Core;
using XM.Core.Data;
using XM.Inventory;
using XM.Quest.Entity;

namespace XM.Quest.Objective
{
    [ServiceBinding(typeof(CollectItemObjective))]
    internal class CollectItemObjective : IQuestObjective
    {
        public string Resref { get; set; }
        public int Quantity { get; set; }

        private readonly DBService _db;
        private readonly ItemCacheService _itemCache;
        private readonly QuestService _quest;

        public CollectItemObjective(
            DBService db,
            ItemCacheService itemCache,
            QuestService quest)
        {
            _db = db;
            _itemCache = itemCache;
            _quest = quest;
        }

        public void Initialize(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : new PlayerQuestDetail();

            quest.ItemProgresses[Resref] = Quantity;
            dbPlayer.Quests[questId] = quest;
            _db.Set(dbPlayer);
        }

        public void Advance(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : null;

            if (quest == null) return;
            if (!quest.ItemProgresses.ContainsKey(Resref)) return;
            if (quest.ItemProgresses[Resref] <= 0) return;

            quest.ItemProgresses[Resref]--;
            _db.Set(dbPlayer);

            var questDetail = _quest.GetQuestById(questId);
            var itemName = _itemCache.GetItemNameByResref(Resref);

            var statusMessage = $"[{questDetail.Name}] {itemName} remaining: {quest.ItemProgresses[Resref]}";

            if (quest.ItemProgresses[Resref] <= 0)
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

            foreach (var progress in quest.ItemProgresses.Values)
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
            if (!dbPlayer.Quests[questId].ItemProgresses.ContainsKey(Resref))
                return "N/A";

            var numberRemaining = dbPlayer.Quests[questId].ItemProgresses[Resref];
            var itemName = _itemCache.GetItemNameByResref(Resref);
            return $"{Quantity - numberRemaining} / {Quantity} {itemName}";
        }
    }
}
