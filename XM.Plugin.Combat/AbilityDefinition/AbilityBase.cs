using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Party;
using CreatureType = XM.Shared.API.Constants.CreatureType;

namespace XM.Plugin.Combat.AbilityDefinition
{
    internal abstract class AbilityBase: IAbilityListDefinition
    {
        private readonly PartyService _party;
        private readonly StatusEffectService _status;

        protected AbilityBase(
            PartyService party,
            StatusEffectService status)
        {
            _party = party;
            _status = status;
        }

        protected void ApplyPartyAOE<T>(
            uint source,
            uint target,
            float distance,
            int durationTicks)
            where T : IStatusEffect
        {
            _status.ApplyStatusEffect<T>(source, target, durationTicks);

            var nth = 1;
            var nearby = GetNearestCreature(CreatureType.IsAlive, 1, target, nth);
            while (GetIsObjectValid(nearby) && GetDistanceBetween(target, nearby) <= distance)
            {
                if (target != nearby && _party.IsInParty(target, nearby))
                {
                    _status.ApplyStatusEffect<T>(source, nearby, durationTicks);
                }

                nth++;
                nearby = GetNearestCreature(CreatureType.IsAlive, 1, target, nth);
            }
        }

        public abstract Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
