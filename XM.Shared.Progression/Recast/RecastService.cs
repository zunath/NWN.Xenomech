using System;
using System.Globalization;
using Anvil.Services;
using XM.Progression.Recast.Entity;
using XM.Progression.Stat;
using XM.Shared.Core;
using XM.Shared.Core.Data;

namespace XM.Progression.Recast
{
    [ServiceBinding(typeof(RecastService))]
    public class RecastService
    {
        private readonly DBService _db;
        private readonly TimeService _time;
        private readonly StatService _stat;

        public RecastService(
            DBService db, 
            TimeService time,
            StatService stat)
        {
            _db = db;
            _time = time;
            _stat = stat;
        }

        /// <summary>
        /// Returns true if a recast delay has not expired yet.
        /// Returns false if there is no recast delay or the time has already passed.
        /// </summary>
        /// <param name="creature">The creature to check</param>
        /// <param name="recastGroup">The recast group to check</param>
        /// <returns>true if recast delay hasn't passed. false otherwise. If true, also returns a string containing a user-readable amount of time they need to wait. Otherwise it will be an empty string.</returns>
        public (bool, string) IsOnRecastDelay(uint creature, RecastGroup recastGroup)
        {
            if (GetIsDM(creature)) return (false, string.Empty);
            var now = DateTime.UtcNow;

            // Players
            if (GetIsPC(creature) && !GetIsDMPossessed(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayer = _db.Get<PlayerRecast>(playerId) ?? new PlayerRecast(playerId);

                if (!dbPlayer.RecastTimes.ContainsKey(recastGroup)) return (false, string.Empty);

                var timeToWait = _time.GetTimeToWaitLongIntervals(now, dbPlayer.RecastTimes[recastGroup], false);
                return (now < dbPlayer.RecastTimes[recastGroup], timeToWait);
            }
            // NPCs and DM-possessed NPCs
            else
            {
                var unlockDate = GetLocalString(creature, $"ABILITY_RECAST_ID_{(int)recastGroup}");
                if (string.IsNullOrWhiteSpace(unlockDate))
                {
                    return (false, string.Empty);
                }
                else
                {
                    var dateTime = DateTime.ParseExact(unlockDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    var timeToWait = _time.GetTimeToWaitLongIntervals(now, dateTime, false);
                    return (now < dateTime, timeToWait);
                }
            }
        }

        /// <summary>
        /// Applies a recast delay on a specific recast group.
        /// If group is invalid or delay amount is less than or equal to zero, nothing will happen.
        /// </summary>
        /// <param name="activator">The activator of the ability.</param>
        /// <param name="group">The recast group to put this delay under.</param>
        /// <param name="delaySeconds">The number of seconds to delay.</param>
        /// <param name="ignoreRecastReduction">If true, recast reduction bonuses are ignored.</param>
        public void ApplyRecastDelay(uint activator, RecastGroup group, float delaySeconds, bool ignoreRecastReduction)
        {
            if (!GetIsObjectValid(activator) || group == RecastGroup.Invalid || delaySeconds <= 0.0f) return;

            // NPCs and DM-possessed NPCs
            if (!GetIsPC(activator) || GetIsDMPossessed(activator))
            {
                var recastDate = DateTime.UtcNow.AddSeconds(delaySeconds);
                var recastDateString = recastDate.ToString("yyyy-MM-dd HH:mm:ss");
                SetLocalString(activator, $"ABILITY_RECAST_ID_{(int)group}", recastDateString);
            }
            // Players
            else if (GetIsPC(activator) && !GetIsDM(activator))
            {
                var playerId = PlayerId.Get(activator);
                var dbPlayerCombat = _db.Get<PlayerRecast>(playerId) ?? new PlayerRecast(playerId);

                if (!ignoreRecastReduction)
                {
                    var recastReduction = _stat.GetAbilityRecastReduction(activator);

                    var recastPercentage = recastReduction * 0.01f;
                    if (recastPercentage > 0.5f)
                        recastPercentage = 0.5f;

                    delaySeconds -= delaySeconds * recastPercentage;
                }

                var recastDate = DateTime.UtcNow.AddSeconds(delaySeconds);
                dbPlayerCombat.RecastTimes[group] = recastDate;

                _db.Set(dbPlayerCombat);
            }

        }

    }
}
