using Anvil.Services;
using NLog;
using System.Collections.Generic;
using XM.Quest.Objective;
using XM.API.NWNX.PlayerPlugin;
using XM.Quest.Entity;
using JournalEntry = XM.API.NWNX.PlayerPlugin.JournalEntry;
using System.Linq;
using XM.API.Constants;
using XM.Core.Activity;
using XM.Inventory;
using CreatureType = XM.API.Constants.CreatureType;
using InventoryDisturbType = XM.API.Constants.InventoryDisturbType;
using System;
using XM.Core.EventManagement;
using XM.Core.Extension;
using XM.Quest.Event;
using XM.Core;
using XM.Core.Data;

namespace XM.Quest
{
    [ServiceBinding(typeof(QuestService))]
    internal class QuestService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, QuestDetail> _quests = new();
        private readonly Dictionary<QuestNPCGroupType, List<string>> _npcsWithKillQuests = new();
        private static readonly Dictionary<QuestNPCGroupType, QuestNPCGroupAttribute> _npcGroups = new();

        public IList<IQuestListDefinition> Definitions { get; set; }

        private readonly DBService _db;
        private readonly ItemCacheService _itemCache;
        private readonly InventoryService _inventory;
        private readonly ActivityService _activity;
        private readonly IServiceManager _serviceManager;
        private readonly XMEventService _event;

        public QuestService(
            DBService db, 
            ItemCacheService itemCache,
            InventoryService inventory,
            ActivityService activity,
            IServiceManager serviceManager,
            XMEventService @event)
        {
            _db = db;
            _itemCache = itemCache;
            _inventory = inventory;
            _activity = activity;
            _serviceManager = serviceManager;
            _event = @event;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<QuestCompletedEvent>(QuestEventScript.OnQuestCompletedScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnPlayerEnter);
            _event.Subscribe<XMEvent.OnCacheDataBefore>(OnCacheDataBefore);
            _event.Subscribe<CreatureEvent.OnDeathBefore>(CreatureOnDeathBefore);

        }

        private void OnCacheDataBefore()
        {
            LoadQuests();
            LoadQuestNPCGroups();
        }

        private void LoadQuests()
        {
            // Organize quests to make later reads quicker.
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(p => typeof(IQuestListDefinition).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .ToArray();

            foreach (var type in types)
            {
                var instance = _serviceManager.AnvilServiceContainer.GetInstance(type) as IQuestListDefinition;
                if (instance == null)
                    continue;

                var quests = instance.BuildQuests();

                foreach (var (questId, quest) in quests)
                {
                    _quests[questId] = quest;

                    // If any state has a Kill Target objective, add the NPC Group ID to the cache
                    foreach (var state in quest.States)
                    {
                        foreach (var objective in state.Value.GetObjectives())
                        {
                            if (objective is KillTargetObjective killObjective)
                            {
                                if (!_npcsWithKillQuests.ContainsKey(killObjective.Group))
                                    _npcsWithKillQuests[killObjective.Group] = new List<string>();

                                if (!_npcsWithKillQuests[killObjective.Group].Contains(questId))
                                    _npcsWithKillQuests[killObjective.Group].Add(questId);
                            }
                        }
                    }
                }
            }
        }

        private void LoadQuestNPCGroups()
        {
            var npcGroups = Enum.GetValues(typeof(QuestNPCGroupType)).Cast<QuestNPCGroupType>();
            foreach (var npcGroupType in npcGroups)
            {
                var npcGroupDetail = npcGroupType.GetAttribute<QuestNPCGroupType, QuestNPCGroupAttribute>();
                _npcGroups[npcGroupType] = npcGroupDetail;
            }

            _logger.Info($"Loaded {_npcGroups.Count} NPC groups.");
        }

        private void OnPlayerEnter()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

            // Reapply quest journal entries on log-in.
            // An NWN quirk requires this to be on a short delay because journal entries are wiped on login.
            DelayCommand(0.5f, () =>
            {
                foreach (var (questId, playerQuest) in dbPlayer.Quests)
                {
                    var quest = _quests[questId];
                    var state = quest.States[playerQuest.CurrentState];

                    PlayerPlugin.AddCustomJournalEntry(player, new JournalEntry()
                    {
                        Name = quest.Name,
                        Text = state.JournalText,
                        Tag = questId,
                        State = playerQuest.CurrentState,
                        Priority = 1,
                        IsQuestCompleted = false,
                        IsQuestDisplayed = true,
                        Updated = 0,
                        CalendarDay = GetCalendarDay(),
                        TimeOfDay = GetTimeHour()
                    }, true);
                }
            });
        }

        /// <summary>
        /// Retrieves an NPC group detail by the type.
        /// </summary>
        /// <param name="type">The type of NPC group to retrieve.</param>
        /// <returns>An NPC group detail</returns>
        public QuestNPCGroupAttribute GetQuestNPCGroup(QuestNPCGroupType type)
        {
            return _npcGroups[type];
        }

        /// <summary>
        /// Retrieves a quest by its Id. If the quest has not been registered, a KeyNotFoundException will be thrown.
        /// </summary>
        /// <param name="questId">The quest Id to search for.</param>
        /// <returns>The quest detail matching this Id.</returns>
        public QuestDetail GetQuestById(string questId)
        {
            if (!_quests.ContainsKey(questId))
                throw new KeyNotFoundException($"Quest '{questId}' was not registered. Did you set the right Id?");

            return _quests[questId];
        }

        /// <summary>
        /// Retrieves the quests associated with an NPC group.
        /// If no quests are associated with this NPC group, an empty list will be returned.
        /// </summary>
        /// <param name="npcGroupType">The NPC group to search for</param>
        /// <returns>A list of quests associated with an NPC group.</returns>
        public List<string> GetQuestsAssociatedWithNPCGroup(QuestNPCGroupType npcGroupType)
        {
            if (!_npcsWithKillQuests.ContainsKey(npcGroupType))
                return new List<string>();

            return _npcsWithKillQuests[npcGroupType];
        }

        public void AbandonQuest(uint player, string questId)
        {
            _quests[questId].Abandon(player);
        }

        /// <summary>
        /// Makes a player accept a quest by the specified Id.
        /// If the quest Id is invalid, an exception will be thrown.
        /// </summary>
        /// <param name="player">The player who is accepting the quest</param>
        /// <param name="questId">The Id of the quest to accept.</param>
        public void AcceptQuest(uint player, string questId)
        {
            _quests[questId].Accept(player, OBJECT_SELF);
        }

        /// <summary>
        /// Makes a player advance to the next state of the quest.
        /// If there are no additional states, the quest will be treated as completed.
        /// </summary>
        /// <param name="player">The player who is advancing to the next state of the quest.</param>
        /// <param name="questSource">The source of the quest. Typically an NPC or object.</param>
        /// <param name="questId">The Id of the quest to advance.</param>
        public void AdvanceQuest(uint player, uint questSource, string questId)
        {
            _quests[questId].Advance(player, questSource);
        }

        /// <summary>
        /// Forces a player to open a collection placeable in which they will put items needed for the quest.
        /// </summary>
        /// <param name="player">The player who will open the collection placeable.</param>
        /// <param name="questId">The quest to collect items for.</param>
        public void RequestItemsFromPlayer(uint player, string questId)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

            if (!dbPlayer.Quests.ContainsKey(questId))
            {
                SendMessageToPC(player, "You have not accepted this quest yet.");
                return;
            }

            var quest = dbPlayer.Quests[questId];
            var questDetail = GetQuestById(questId);
            var questState = questDetail.States[quest.CurrentState];

            // Ensure there's at least one "Collect Item" objective on this quest state.
            var hasCollectItemObjective = questState.GetObjectives().OfType<CollectItemObjective>().Any();

            // The only time this should happen is if the quest is misconfigured.
            if (!hasCollectItemObjective)
            {
                SendMessageToPC(player, "There are no items to turn in for this quest. This is likely a bug. Please let the staff know.");
                return;
            }

            var collector = CreateObject(ObjectType.Placeable, "qst_item_collect", GetLocation(player));
            SetLocalObject(collector, "QUEST_OWNER", OBJECT_SELF);
            SetLocalString(collector, "QUEST_ID", questId);

            AssignCommand(collector, () => SetFacingPoint(GetPosition(player)));
            AssignCommand(player, () => ActionInteractObject(collector));
        }

        private void CreatureOnDeathBefore()
        {
            ProgressKillTargetObjectives();
        }

        /// <summary>
        /// When an NPC is killed, any objectives for quests a player currently has active will be updated.
        /// </summary>
        private void ProgressKillTargetObjectives()
        {
            var creature = OBJECT_SELF;
            var npcGroupType = (QuestNPCGroupType)GetLocalInt(creature, "QUEST_NPC_GROUP_ID");
            if (npcGroupType == QuestNPCGroupType.Invalid) return;
            var possibleQuests = GetQuestsAssociatedWithNPCGroup(npcGroupType);
            if (possibleQuests.Count <= 0) return;

            var killer = GetLastKiller();
            if (killer == OBJECT_INVALID) killer = GetNearestCreature(CreatureType.PlayerCharacter, 1, creature);

            // Iterate over every player in the killer's party.
            // Every player who needs this NPCGroupType for a quest will have their objective advanced if they are within range and in the same area.
            for (var member = GetFirstFactionMember(killer); GetIsObjectValid(member); member = GetNextFactionMember(killer))
            {
                if (!GetIsPC(member) || GetIsDM(member))
                    continue;

                if (GetArea(member) != GetArea(killer))
                    continue;

                if (GetDistanceBetween(member, creature) > 50f)
                    continue;

                var playerId = PlayerId.Get(member);
                var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

                // Need to iterate over every possible quest this creature is a part of.
                foreach (var questId in possibleQuests)
                {
                    // Players who don't have the quest are skipped.
                    if (!dbPlayer.Quests.ContainsKey(questId)) continue;

                    var quest = dbPlayer.Quests[questId];
                    var questDetail = GetQuestById(questId);
                    var questState = questDetail.States[quest.CurrentState];
                    var killRequiredForQuestAndState = false;

                    // Iterate over all of the quest states which call for killing this enemy.
                    foreach (var objective in questState.GetObjectives())
                    {
                        // Only kill target objectives matching this NPC group ID are processed.
                        if (objective is KillTargetObjective killTargetObjective)
                        {
                            if (killTargetObjective.Group != npcGroupType) continue;

                            killRequiredForQuestAndState = true;
                            killTargetObjective.Advance(member, questId);
                        }
                    }

                    // Attempt to advance the quest detail. It's possible this will fail because objectives aren't all done. This is OK.
                    if (killRequiredForQuestAndState)
                    {
                        questDetail.Advance(member, creature);
                    }
                }
            }
        }

        /// <summary>
        /// When an item collector placeable is opened, 
        /// </summary>
        [ScriptHandler(QuestEventScript.OnQuestItemCollectorOpened)]
        public void OpenItemCollector()
        {
            var container = OBJECT_SELF;
            SetUseableFlag(container, false);

            var questId = GetLocalString(container, "QUEST_ID");
            var player = GetLastOpenedBy();
            var playerId = PlayerId.Get(player);

            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

            if (!dbPlayer.Quests.ContainsKey(questId))
            {
                SendMessageToPC(player, "You have not accepted this quest.");
                return;
            }

            FloatingTextStringOnCreature("Please place the items you would like to turn in for this quest into the container. If you want to cancel this process, move away from the container.", player, false);
            var quest = dbPlayer.Quests[questId];

            string text = "Required Items: \n\n";

            foreach (var itemProgress in quest.ItemProgresses)
            {
                var itemName = _itemCache.GetItemNameByResref(itemProgress.Key);
                text += $"{itemProgress.Value}x {itemName}\n";
            }

            SendMessageToPC(player, text);

            _activity.SetBusy(player, ActivityStatusType.Quest);
        }

        /// <summary>
        /// When an item collector placeable is closed, clear its inventory and destroy it.
        /// </summary>
        [ScriptHandler(QuestEventScript.OnQuestItemCollectorClosed)]
        public void CloseItemCollector()
        {
            var player = GetLastClosedBy();
            DelayCommand(0.02f, () =>
            {
                for (var item = GetFirstItemInInventory(OBJECT_SELF); GetIsObjectValid(item); item = GetNextItemInInventory(OBJECT_SELF))
                {
                    DestroyObject(item);
                }

                DestroyObject(OBJECT_SELF);
            });

            _activity.ClearBusy(player);
        }

        /// <summary>
        /// When an item collector placeable is disturbed, 
        /// </summary>
        [ScriptHandler(QuestEventScript.OnQuestItemCollectorDisturbed)]
        public void DisturbItemCollector()
        {
            var type = GetInventoryDisturbType();
            if (type != InventoryDisturbType.Added) return;

            var container = OBJECT_SELF;
            var owner = GetLocalObject(container, "QUEST_OWNER");
            var player = GetLastDisturbed();
            var playerId = PlayerId.Get(player);
            var dbPlayerQuest = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var item = GetInventoryDisturbItem();
            var resref = GetResRef(item);
            var questId = GetLocalString(container, "QUEST_ID");
            var quest = dbPlayerQuest.Quests[questId];

            // Item not required, or all items have been turned in.
            if (!quest.ItemProgresses.ContainsKey(resref) ||
                quest.ItemProgresses[resref] <= 0)
            {
                _inventory.ReturnItem(player, item);
                SendMessageToPC(player, "That item is not required for this quest.");
                return;
            }

            var requiredAmount = dbPlayerQuest.Quests[questId].ItemProgresses[resref];
            var stackSize = GetItemStackSize(item);

            // Decrement the required items and update the DB.
            if (stackSize > requiredAmount)
            {
                dbPlayerQuest.Quests[questId].ItemProgresses[resref] = 0;
                _inventory.ReduceItemStack(item, requiredAmount);
                _inventory.ReturnItem(player, item);
            }
            else
            {
                dbPlayerQuest.Quests[questId].ItemProgresses[resref] -= stackSize;
                _inventory.ReduceItemStack(item, stackSize);
            }

            _db.Set(dbPlayerQuest);

            // Give the player an update and reduce the item stack.
            var itemName = _itemCache.GetItemNameByResref(resref);
            SendMessageToPC(player, $"You need {dbPlayerQuest.Quests[questId].ItemProgresses[resref]}x {itemName} to complete this quest.");

            // Attempt to advance the quest.
            // If player hasn't completed the other objectives, nothing will happen when this is called.
            AdvanceQuest(player, owner, questId);

            // If no more items are necessary for this quest, force the player to speak with the NPC again.
            var itemsRequired = dbPlayerQuest.Quests[questId].ItemProgresses.Sum(x => x.Value);

            if (itemsRequired <= 0)
            {
                AssignCommand(player, () => ActionStartConversation(owner, string.Empty, true, false));
            }
        }

        /// <summary>
        /// When a player uses a quest placeable, handle the progression.
        /// </summary>
        [ScriptHandler(QuestEventScript.OnUseQuestPlaceableScript)]
        public void UseQuestPlaceable()
        {
            var player = GetLastUsedBy();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            TriggerAndPlaceableProgression(player, OBJECT_SELF);
        }

        /// <summary>
        /// When a player enters a quest trigger, handle the progression.
        /// </summary>
        [ScriptHandler(QuestEventScript.OnEnterQuestTriggerScript)]
        public void EnterQuestTrigger()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            TriggerAndPlaceableProgression(player, OBJECT_SELF);
        }


        /// <summary>
        /// Handles advancing a player's quest when they enter a trigger or click a quest placeable.
        /// Trigger or placeable must have both QUEST_ID (string) and QUEST_STATE (int) set in order for this to work, otherwise an error will be raised.
        /// </summary>
        /// <param name="player">The player who entered the trigger or clicked a placeable.</param>
        /// <param name="triggerOrPlaceable">The trigger or placeable</param>
        public void TriggerAndPlaceableProgression(uint player, uint triggerOrPlaceable)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;
            var questMessage = GetLocalString(triggerOrPlaceable, "QUEST_MESSAGE");
            var questId = GetLocalString(triggerOrPlaceable, "QUEST_ID");
            var questState = GetLocalInt(triggerOrPlaceable, "QUEST_STATE");

            if (string.IsNullOrWhiteSpace(questId))
            {
                SendMessageToPC(player, "QUEST_ID variable not set on object. Please inform admin this quest is bugged. (QuestID: " + questId + ")");
                return;
            }

            if (questState <= 0)
            {
                SendMessageToPC(player, "QUEST_STATE variable not set on object. Please inform admin this quest is bugged. (QuestID: " + questId + ")");
                return;
            }

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);

            if (!dbPlayer.Quests.ContainsKey(questId)) return;

            var dbQuest = dbPlayer.Quests[questId];

            if (dbQuest.CurrentState != questState)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(questMessage))
            {
                DelayCommand(1.0f, () =>
                {
                    SendMessageToPC(player, questMessage);
                });
            }

            var quest = GetQuestById(questId);
            quest.Advance(player, triggerOrPlaceable);
        }

        public int CalculateQuestGoldReward(uint player, int baseAmount)
        {
            // 5% credit bonus per social modifier.
            var social = GetAbilityModifier(AbilityType.Social, player) * 0.05f;
            var amount = baseAmount + (int)(baseAmount * social);
            return amount;
        }
    }
}
