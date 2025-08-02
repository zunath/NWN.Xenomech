using XM.Progression.Job;
using XM.Shared.Core.Data;

namespace XM.Quest.Reward
{
    internal class XPReward : IQuestReward
    {
        public int Amount { get; set; }
        public bool IsSelectable { get; set; }
        public string MenuName => Amount + " XP";

        private readonly DBService _db;
        private readonly JobService _job;

        public XPReward(DBService db, JobService job)
        {
            _db = db;
            _job = job;
        }

        public void GiveReward(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                return;

            _job.GiveXP(player, Amount);
        }
    }
}
