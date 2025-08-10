using NLog;
using System.Collections.Generic;
using XM.Quest.Objective;
using XM.Shared.Core.Entity;
using XM.Shared.Core.Entity.Quest;
using JournalEntry = XM.Shared.API.NWNX.PlayerPlugin.JournalEntry;
using System.Linq;
using XM.Inventory;
using CreatureType = XM.Shared.API.Constants.CreatureType;
using InventoryDisturbType = XM.Shared.API.Constants.InventoryDisturbType;
using System;
using XM.Quest.Event;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.PlayerPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Activity;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using Anvil.Services;
using XM.Quest.Reward;
using XM.Quest.Conversation;
using DialogService = XM.Shared.Core.Dialog.DialogService;
using Anvil.API;
using XM.Progression.Event;

namespace XM.Quest
{
    [ServiceBinding(typeof(QuestService))]
    [ServiceBinding(typeof(IInitializable))]
    [ServiceBinding(typeof(IDisposable))]
    internal class QuestService: 
        IInitializable,
        IDisposable
    {
        private readonly Dictionary<string, QuestDetail> _quests = new();
        private readonly Dictionary<QuestNPCGroupType, List<string>> _npcsWithKillQuests = new();

        [Inject]
        public IList<IQuestListDefinition> Definitions { get; set; }

        private readonly DBService _db;
        private readonly ItemCacheService _itemCache;
        private readonly InventoryService _inventory;
        private readonly ActivityService _activity;
        private readonly XMEventService _event;
        private readonly DialogService _dialog;

        public QuestService(
            DBService db, 
            ItemCacheService itemCache,
            InventoryService inventory,
            ActivityService activity,
            XMEventService @event,
            DialogService dialog)
        {
            _db = db;
            _itemCache = itemCache;
            _inventory = inventory;
            _activity = activity;
            _event = @event;
            _dialog = dialog;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnPlayerEnter);
            _event.Subscribe<CreatureEvent.OnDeath>(CreatureOnDeathBefore);
        }

        public void Init()
        {
            foreach (var definition in Definitions)
            {
                var quests = definition.BuildQuests();

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


        private void OnPlayerEnter(uint objectSelf)
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

        private void CreatureOnDeathBefore(uint objectSelf)
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
                        AdvanceQuest(member, questId, creature);
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
            AdvanceQuest(player, questId, owner);

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

            AdvanceQuest(player, questId, triggerOrPlaceable);
        }




        /// <summary>
        /// Returns true if player can accept this quest. Returns false otherwise.
        /// </summary>
        /// <param name="player">The player to check</param>
        /// <returns>true if player can accept, false otherwise</returns>
        private bool CanAcceptQuest(uint player, string questId)
        {
            // Retrieve the player's current quest status for this quest.
            // If they haven't accepted it yet, this will be null.
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : null;
            var questDetail = _quests[questId];

            // If the status is null, it's assumed that the player hasn't accepted it yet.
            if (quest != null)
            {
                // If the quest isn't repeatable, prevent the player from accepting it after it's already been completed.
                if (quest.TimesCompleted > 0)
                {
                    // If it's repeatable, then we don't care if they've already completed it.
                    if (!questDetail.IsRepeatable)
                    {
                        SendMessageToPC(player, "You have already completed this quest.");
                        return false;
                    }
                }
                // If the player already accepted the quest, prevent them from accepting it again.
                else
                {
                    SendMessageToPC(player, "You have already accepted this quest.");
                    return false;
                }
            }

            // Check whether the player meets all necessary prerequisites.
            foreach (var prereq in questDetail.Prerequisites)
            {
                if (!prereq.MeetsPrerequisite(player))
                {
                    SendMessageToPC(player, "You do not meet the prerequisites necessary to accept this quest.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if player can complete this quest. Returns false otherwise.
        /// </summary>
        /// <param name="player">The player to check</param>
        /// <returns>true if player can complete, false otherwise</returns>
        public bool CanCompleteQuest(uint player, string questId)
        {
            // Has the player even accepted this quest?
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : null;

            if (quest == null) return false;
            var questDetail = _quests[questId];

            // Is the player on the final state of this quest?
            if (quest.CurrentState != questDetail.States.Count) return false;

            var state = questDetail.States[quest.CurrentState];
            // Are all objectives complete?
            foreach (var objective in state.GetObjectives())
            {
                if (!objective.IsComplete(player, questId))
                {
                    return false;
                }
            }

            // Met all requirements. We can complete this quest.
            return true;
        }

        /// <summary>
        /// Opens the reward selection menu wherein players can select the reward they want.
        /// If quest is not configured to allow reward selection, quest will be marked complete instead
        /// and all rewards will be given to the player.
        /// </summary>
        /// <param name="player">The player to request a reward from</param>
        /// <param name="questSource">The source of the quest reward giver</param>
        private void RequestRewardSelectionFromPC(uint player, string questId, uint questSource)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var questDetail = _quests[questId];
            if (questDetail.AllowRewardSelection)
            {
                SetLocalString(player, "QST_REWARD_SELECTION_QUEST_ID", questId);
                _dialog.StartConversation<QuestRewardSelectionDialog>(player, player);
            }
            else
            {
                CompleteQuest(player, questId, questSource, null);
            }
        }


        /// <summary>
        /// Abandons a quest.
        /// </summary>
        /// <param name="player">The player abandoning a quest.</param>
        public void AbandonQuest(uint player, string questId)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            if (!dbPlayer.Quests.ContainsKey(questId))
                return;
            var questDetail = _quests[questId];

            // This is a repeatable quest. Mark the completion date to now
            // so that we don't lose how many times it's been completed.
            if (dbPlayer.Quests[questId].TimesCompleted > 0)
            {
                dbPlayer.Quests[questId].DateLastCompleted = DateTime.UtcNow;
            }
            // This quest hasn't been completed yet. It's safe to remove it completely.
            else
            {
                dbPlayer.Quests.Remove(questId);
            }

            _db.Set(dbPlayer);
            SendMessageToPC(player, $"Quest '{questDetail.Name}' has been abandoned!");

            foreach (var action in questDetail.OnAbandonActions)
            {
                action.Invoke(player);
            }
        }

        /// <summary>
        /// Accepts a quest using the configured settings.
        /// </summary>
        /// <param name="player">The player accepting the quest.</param>
        /// <param name="questSource">The source of the quest giver</param>
        public void AcceptQuest(uint player, string questId, uint questSource)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            if (!CanAcceptQuest(player, questId))
            {
                return;
            }

            // By this point, it's assumed the player will accept the quest.
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var questDetail = _quests[questId];
            var playerQuest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : new PlayerQuestDetail();

            // Retrieve the first quest state for this quest.
            playerQuest.CurrentState = 1;
            playerQuest.DateLastCompleted = null;
            dbPlayer.Quests[questId] = playerQuest;
            _db.Set(dbPlayer);

            var state = questDetail.States[1];
            foreach (var objective in state.GetObjectives())
            {
                objective.Initialize(player, questId);
            }

            // Add the journal entry to the player.
            PlayerPlugin.AddCustomJournalEntry(player, new JournalEntry()
            {
                Name = questDetail.Name,
                Text = state.JournalText,
                Tag = questId,
                State = playerQuest.CurrentState,
                Priority = 1,
                IsQuestCompleted = false,
                IsQuestDisplayed = true,
                Updated = 0,
                CalendarDay = GetCalendarDay(),
                TimeOfDay = GetTimeHour()
            });

            // Notify them that they've accepted a quest.
            SendMessageToPC(player, "Quest '" + questDetail.Name + "' accepted. Refer to your journal for more information on this quest.");

            // Run any quest-specific code.
            foreach (var action in questDetail.OnAcceptActions)
            {
                action.Invoke(player, questSource);
            }

            //Gui.PublishRefreshEvent(player, new QuestAcquiredRefreshEvent(QuestId));
        }

        /// <summary>
        /// Advances the player to the next quest state.
        /// </summary>
        /// <param name="player">The player advancing to the next quest state</param>
        /// <param name="questSource">The source of quest advancement</param>
        public void AdvanceQuest(uint player, string questId, uint questSource)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;

            // Retrieve the player's current quest state.
            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var questDetail = _quests[questId];
            var playerQuest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : new PlayerQuestDetail();

            // Can't find a state? Notify the player they haven't accepted the quest.
            if (playerQuest.CurrentState <= 0)
            {
                SendMessageToPC(player, "You have not accepted this quest yet.");
                return;
            }

            // If this quest has already been completed, exit early.
            // This is used in case a module builder incorrectly configures a quest.
            // We don't want to risk giving duplicate rewards.
            if (playerQuest.TimesCompleted > 0 && !questDetail.IsRepeatable) return;

            var currentState = questDetail.States[playerQuest.CurrentState];

            // Check quest objectives. If not complete, exit early.
            foreach (var objective in currentState.GetObjectives())
            {
                if (!objective.IsComplete(player, questId))
                    return;
            }

            var lastState = questDetail.States.Last();

            // If this is the last state, the assumption is that it's time to complete the quest.
            if (playerQuest.CurrentState == lastState.Key)
            {
                RequestRewardSelectionFromPC(player, questId, questSource);
            }
            else
            {
                // Progress player's quest status to the next state.
                playerQuest.CurrentState++;
                var nextState = questDetail.States[playerQuest.CurrentState];

                // Update the player's journal
                PlayerPlugin.AddCustomJournalEntry(player, new JournalEntry
                {
                    Name = questDetail.Name,
                    Text = currentState.JournalText,
                    Tag = questId,
                    State = playerQuest.CurrentState,
                    Priority = 1,
                    IsQuestCompleted = false,
                    IsQuestDisplayed = true,
                    Updated = 0,
                    CalendarDay = GetCalendarDay(),
                    TimeOfDay = GetTimeHour()
                });

                // Notify the player they've progressed.
                SendMessageToPC(player, "Objective for quest '" + questDetail.Name + "' complete! Check your journal for information on the next objective.");

                // Save changes
                dbPlayer.Quests[questId] = playerQuest;
                _db.Set(dbPlayer);

                // Create any extended data entries for the next state of the quest.
                foreach (var objective in nextState.GetObjectives())
                {
                    objective.Initialize(player, questId);
                }

                // Run any quest-specific code.
                foreach (var action in questDetail.OnAdvanceActions)
                {
                    action.Invoke(player, questSource, playerQuest.CurrentState);
                }

                //Gui.PublishRefreshEvent(player, new QuestProgressedRefreshEvent(QuestId));
            }

        }

        /// <summary>
        /// Completes a quest for a player. If a reward is selected, that reward will be given to the player.
        /// Otherwise, all rewards configured for this quest will be given to the player.
        /// </summary>
        /// <param name="player">The player completing the quest.</param>
        /// <param name="questSource">The source of the quest completion</param>
        /// <param name="selectedReward">The reward selected by the player</param>
        public void CompleteQuest(uint player, string questId, uint questSource, IQuestReward selectedReward)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;
            if (!CanCompleteQuest(player, questId)) return;

            var playerId = PlayerId.Get(player);
            var dbPlayer = _db.Get<PlayerQuest>(playerId) ?? new PlayerQuest(playerId);
            var quest = dbPlayer.Quests.ContainsKey(questId) ? dbPlayer.Quests[questId] : new PlayerQuestDetail();
            var questDetail = _quests[questId];

            // Mark player as being on the last state of the quest.
            quest.CurrentState = questDetail.States.Count;
            quest.TimesCompleted++;

            // Note - we must update the database before we give rewards.  Otherwise rewards that update the
            // database (e.g. key items) will be discarded when we commit this change.
            quest.ItemProgresses.Clear();
            quest.KillProgresses.Clear();
            quest.DateLastCompleted = DateTime.UtcNow;
            dbPlayer.Quests[questId] = quest;
            _db.Set(dbPlayer);

            // No selected reward, simply give all available rewards to the player.
            if (selectedReward == null)
            {
                foreach (var reward in questDetail.Rewards)
                {
                    reward.GiveReward(player);
                }
            }
            // There is a selected reward. Give that reward and any rewards which are not selectable to the player.
            else
            {
                // Non-selectable rewards (gold, GP, etc) are granted to the player.
                foreach (var reward in questDetail.Rewards.Where(x => !x.IsSelectable))
                {
                    reward.GiveReward(player);
                }

                selectedReward.GiveReward(player);
            }

            foreach (var action in questDetail.OnCompleteActions)
            {
                action.Invoke(player, questSource);
            }

            SendMessageToPC(player, "Quest '" + questDetail.Name + "' complete!");
            RemoveJournalQuestEntry(questId, player, false);

            _event.PublishEvent<QuestEvent.QuestCompletedEvent>(player);
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing {nameof(QuestService)}");
            Definitions.Clear();
            _quests.Clear();
            _npcsWithKillQuests.Clear();
        }
    }
}
