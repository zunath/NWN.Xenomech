using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Quest.Prerequisite;
using XM.Quest.Reward;

namespace XM.Quest
{
    [ServiceBinding(typeof(QuestDetail))]
    internal class QuestDetail
    {
        public string QuestId { get; set; }
        public string Name { get; set; }
        public bool IsRepeatable { get; set; }
        public int GuildRank { get; set; } = -1;
        public bool AllowRewardSelection { get; set; }

        public List<IQuestReward> Rewards { get; } = new();
        public List<IQuestPrerequisite> Prerequisites { get; } = new();

        public Dictionary<int, QuestStateDetail> States { get; } = new();
        public List<AcceptQuestDelegate> OnAcceptActions { get; } = new();
        public List<AbandonQuestDelegate> OnAbandonActions { get; } = new();
        public List<AdvanceQuestDelegate> OnAdvanceActions { get; } = new();
        public List<CompleteQuestDelegate> OnCompleteActions { get; } = new();


        /// <summary>
        /// Adds a quest state to this quest.
        /// </summary>
        /// <returns>The newly created quest state.</returns>
        protected QuestStateDetail AddState()
        {
            int index = States.Count;
            States[index] = new QuestStateDetail();
            return States[index];
        }

        /// <summary>
        /// Retrieves a state by its index.
        /// </summary>
        /// <param name="state">The index of the state.</param>
        /// <returns>The quest state at a specified index</returns>
        protected QuestStateDetail GetState(int state)
        {
            return States[state];
        }

        /// <summary>
        /// Retrieves the list of quest states ordered by their sequence.
        /// </summary>
        /// <returns>A list of quest states ordered by their sequence</returns>
        protected Dictionary<int, QuestStateDetail> GetStates()
        {
            return States.OrderBy(o => o.Key).ToDictionary(x => x.Key, y => y.Value);
        }


        /// <summary>
        /// Returns the rewards given for completing this quest.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IQuestReward> GetRewards()
        {
            return Rewards;
        }

        /// <summary>
        /// Gives all rewards for this quest to the player.
        /// </summary>
        /// <param name="player">The player receiving the rewards.</param>
        public void GiveRewards(uint player)
        {
            foreach (var reward in Rewards)
            {
                reward.GiveReward(player);
            }
        }

    }
}
