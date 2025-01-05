using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Inventory.KeyItem;
using XM.Quest.Objective;
using XM.Quest.Prerequisite;
using XM.Quest.Reward;

namespace XM.Quest
{
    [ServiceBinding(typeof(QuestBuilder))]
    internal class QuestBuilder
    {
        private readonly Dictionary<string, QuestDetail> _quests = new();
        private QuestDetail _activeQuest;
        private QuestStateDetail _activeState;

        private readonly QuestService _quest;
        private readonly IServiceManager _serviceManager;

        public QuestBuilder(QuestService quest, IServiceManager serviceManager)
        {
            _quest = quest;
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Creates a new quest with a given questId, name, and journalTag.
        /// All arguments are required. Exceptions will be thrown if any are null or whitespace.
        /// </summary>
        /// <param name="questId">The quest Id to assign this quest.</param>
        /// <param name="name">The name of the quest.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder Create(string questId, string name)
        {
            if (string.IsNullOrWhiteSpace(questId))
                throw new ArgumentException($"{nameof(questId)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} cannot be null or whitespace.");

            _activeQuest = _serviceManager.AnvilServiceContainer.Create(typeof(QuestDetail)) as QuestDetail;
            if (_activeQuest == null)
                throw new Exception($"Unable to locate {typeof(QuestDetail)}");

            _activeQuest.QuestId = questId;
            _activeQuest.Name = name;

            _quests[questId] = _activeQuest;
            _activeState = null;

            return this;
        }

        /// <summary>
        /// Marks the quest as repeatable.
        /// </summary>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder IsRepeatable()
        {
            _activeQuest.IsRepeatable = true;

            return this;
        }

        /// <summary>
        /// Marks that the quest allows the player to select a reward when completed.
        /// </summary>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder HasRewardSelection()
        {
            _activeQuest.AllowRewardSelection = true;

            return this;
        }

        /// <summary>
        /// Adds an item reward for completing this quest.
        /// </summary>
        /// <param name="itemResref">The resref of the item to create.</param>
        /// <param name="quantity">The number of items to create.</param>
        /// <param name="isSelectable">If true, player will have the option to select the item as a reward. If false, they will receive it no matter what.
        /// If IsRepeatable() has not been called, this argument is ignored and all items are given to the player.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddItemReward(string itemResref, int quantity, bool isSelectable = true)
        {
            var reward = _serviceManager.AnvilServiceContainer.Create(typeof(ItemReward)) as ItemReward;
            reward!.Resref = itemResref;
            reward!.Quantity = quantity;
            reward!.IsSelectable = isSelectable;

            _activeQuest.Rewards.Add(reward);

            return this;
        }

        /// <summary>
        /// Adds a gold reward for completing this quest.
        /// </summary>
        /// <param name="amount">The amount of gold to create.</param>
        /// <param name="isSelectable">If true, player will have the option to select the gold as a reward. If false, they will receive it no matter what.
        /// If IsRepeatable() has not been called, this argument is ignored and all gold is given to the player.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddGoldReward(int amount, bool isSelectable = true)
        {
            var reward = _serviceManager.AnvilServiceContainer.Create(typeof(GoldReward)) as GoldReward;
            reward!.Amount = amount;
            reward!.IsSelectable = isSelectable;

            _activeQuest.Rewards.Add(reward);

            return this;
        }

        /// <summary>
        /// Adds an XP reward for completing this quest.
        /// </summary>
        /// <param name="amount">The amount of XP to give.</param>
        /// <param name="isSelectable">If true, player will have the option to select XP as a reward. If false, they will receive it no matter what.
        /// If IsRepeatable() has not been called, this argument is ignored and all XP is given to the player</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddXPReward(int amount, bool isSelectable = true)
        {
            var reward = _serviceManager.AnvilServiceContainer.Create(typeof(XPReward)) as XPReward;
            reward!.Amount = amount;
            reward!.IsSelectable = isSelectable;

            _activeQuest.Rewards.Add(reward);

            return this;
        }

        /// <summary>
        /// Adds a key item reward for completing this quest.
        /// </summary>
        /// <param name="keyItemType">The type of key item to award.</param>
        /// <param name="isSelectable">If true, player will have the option to select the key item as a reward. If false, they will receive it no matter what. If IsRepeatable() has not been called, this argument is ignored and all gold is given to the player.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddKeyItemReward(KeyItemType keyItemType, bool isSelectable = true)
        {
            var reward = _serviceManager.AnvilServiceContainer.Create(typeof(KeyItemReward)) as KeyItemReward;
            reward!.KeyItemType = keyItemType;
            reward!.IsSelectable = isSelectable;

            _activeQuest.Rewards.Add(reward);

            return this;
        }

        /// <summary>
        /// Adds a prerequisite to the quest. If the player has not completed this prerequisite quest, they will be unable to accept it.
        /// </summary>
        /// <param name="prerequisiteQuestId">The unique Id of the prerequisite quest. If this Id has not been registered, an exception will be thrown.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder PrerequisiteQuest(string prerequisiteQuestId)
        {
            var prereq = _serviceManager.AnvilServiceContainer.Create(typeof(RequiredQuestPrerequisite)) as RequiredQuestPrerequisite;
            prereq!.QuestId = prerequisiteQuestId;
            _activeQuest.Prerequisites.Add(prereq);

            return this;
        }

        /// <summary>
        /// Adds a prerequisite to the quest. If the player has not acquired this key item, they will be unable to accept it.
        /// </summary>
        /// <param name="keyItemType">The type of key item to require.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder PrerequisiteKeyItem(KeyItemType keyItemType)
        {
            var prereq = _serviceManager.AnvilServiceContainer.Create(typeof(RequiredKeyItemPrerequisite)) as RequiredKeyItemPrerequisite;
            prereq.KeyItemType = keyItemType;
            _activeQuest.Prerequisites.Add(prereq);

            return this;
        }

        /// <summary>
        /// Adds an action to run when a player accepts a quest.
        /// </summary>
        /// <param name="action">The action to run when a player accepts a quest.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder OnAcceptAction(AcceptQuestDelegate action)
        {
            _activeQuest.OnAcceptActions.Add(action);

            return this;
        }

        /// <summary>
        /// Adds an action to run when a player abandons a quest.
        /// </summary>
        /// <param name="action">The action to run when a player abandons a quest.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder OnAbandonAction(AbandonQuestDelegate action)
        {
            _activeQuest.OnAbandonActions.Add(action);

            return this;
        }

        /// <summary>
        /// Adds an action to run when a player advances to a new quest state.
        /// </summary>
        /// <param name="action">The action to run when a player advances to a new quest state.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder OnAdvanceAction(AdvanceQuestDelegate action)
        {
            _activeQuest.OnAdvanceActions.Add(action);

            return this;
        }

        /// <summary>
        /// Adds an action to run when a player completes a quest.
        /// </summary>
        /// <param name="action">The action to run when a player completes the quest.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder OnCompleteAction(CompleteQuestDelegate action)
        {
            _activeQuest.OnCompleteActions.Add(action);

            return this;
        }

        /// <summary>
        /// Adds a new quest state to the quest.
        /// </summary>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddState()
        {
            _activeState = new QuestStateDetail();
            var index = _activeQuest.States.Count + 1;
            _activeQuest.States[index] = _activeState;

            return this;
        }

        /// <summary>
        /// Sets the journal text of this quest state.
        /// </summary>
        /// <param name="journalText">The journal text to set.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder SetStateJournalText(string journalText)
        {
            _activeState.JournalText = journalText;
            return this;
        }

        /// <summary>
        /// Adds a kill objective to this quest state.
        /// </summary>
        /// <param name="group">The NPC group Id</param>
        /// <param name="quantity">The number of NPCs in this group required to kill to complete the objective.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddKillObjective(QuestNPCGroupType group, int quantity)
        {
            var killObjective = _serviceManager.AnvilServiceContainer.Create(typeof(KillTargetObjective)) as KillTargetObjective;
            killObjective!.Group = group;
            killObjective!.Quantity = quantity;

            _activeState.AddObjective(killObjective);

            return this;
        }

        /// <summary>
        /// Adds a collect item objective to this quest state.
        /// </summary>
        /// <param name="resref">The resref of the required item.</param>
        /// <param name="quantity">The number of items needed to complete the objective.</param>
        /// <returns>A QuestBuilder with the configured options.</returns>
        public QuestBuilder AddCollectItemObjective(string resref, int quantity)
        {
            var collectItemObjective = _serviceManager.AnvilServiceContainer.Create(typeof(CollectItemObjective)) as CollectItemObjective;
            collectItemObjective!.Resref = resref;
            collectItemObjective!.Quantity = quantity;

            _activeState.AddObjective(collectItemObjective);

            return this;
        }

        /// <summary>
        /// Builds all of the configured quests.
        /// </summary>
        /// <returns>A dictionary containing all of the new quests.</returns>
        public Dictionary<string, QuestDetail> Build()
        {
            return _quests;
        }
    }
}
