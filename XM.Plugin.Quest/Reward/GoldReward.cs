using Anvil.Services;

namespace XM.Quest.Reward
{
    [ServiceBinding(typeof(GoldReward))]
    internal class GoldReward : IQuestReward
    {
        public int Amount { get; set;  }
        public bool IsSelectable { get; set; }
        public string MenuName => Amount + " Credits";

        private readonly QuestService _quest;

        public GoldReward(QuestService quest)
        {
            _quest = quest;
        }

        public void GiveReward(uint player)
        {
            var amount = _quest.CalculateQuestGoldReward(player, Amount);
            GiveGoldToCreature(player, amount);
        }
    }

}
