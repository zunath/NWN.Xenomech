using Anvil.Services;
using XM.Data;

namespace XM.Quest.Reward
{
    [ServiceBinding(typeof(XPReward))]
    internal class XPReward : IQuestReward
    {
        public int Amount { get; set; }
        public bool IsSelectable { get; set; }
        public string MenuName => Amount + " XP";

        private readonly DBService _db;

        public XPReward(DBService db)
        {
            _db = db;
        }

        public void GiveReward(uint player)
        {
            var playerId = GetObjectUUID(player);


            // todo: Need to update with new class system
            //var dbPlayer = DB.Get<Player>(playerId);
            //dbPlayer.UnallocatedXP += Amount;

            //DB.Set(dbPlayer);
            //SendMessageToPC(player, $"You earned {Amount} XP!");
        }
    }

}
