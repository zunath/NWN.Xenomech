using Anvil.Services;
using XM.Shared.API.Constants;

namespace XM.Quest.Reward
{
    internal class GoldReward : IQuestReward
    {
        public int Amount { get; set;  }
        public bool IsSelectable { get; set; }
        public string MenuName => Amount + " Credits";

        public void GiveReward(uint player)
        {
            var amount = CalculateQuestGoldReward(player, Amount);
            GiveGoldToCreature(player, amount);
        }


        private int CalculateQuestGoldReward(uint player, int baseAmount)
        {
            // 5% credit bonus per social modifier.
            var social = GetAbilityModifier(AbilityType.Social, player) * 0.05f;
            var amount = baseAmount + (int)(baseAmount * social);
            return amount;
        }
    }

}
