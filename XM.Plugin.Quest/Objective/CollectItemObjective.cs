using Anvil.Services;
using XM.Inventory;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Entity.Quest;
using XM.Shared.Core;
using XM.Shared.Core.Data;

namespace XM.Quest.Objective
{
    internal class CollectItemObjective : IQuestObjective
    {
        public string Resref { get; set; }
        public int Quantity { get; set; }

        private readonly DBService _db;
        private readonly ItemCacheService _itemCache;

        public CollectItemObjective(
            DBService db,
            ItemCacheService itemCache)
        {
            _db = db;
            _itemCache = itemCache;
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

            var itemName = _itemCache.GetItemNameByResref(Resref);

            var statusMessage = $"[{itemName} remaining: {quest.ItemProgresses[Resref]}";

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
