using System;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Activity;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(RestStatusEffect))]
    public class RestStatusEffect : StatusEffectBase
    {
        private const string RestPositionX = "REST_POSITION_X";
        private const string RestPositionY = "REST_POSITION_Y";
        private const string RestPositionZ = "REST_POSITION_Z";

        public override LocaleString Name => LocaleString.Rest;
        public override EffectIconType Icon => EffectIconType.Fatigue;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 6f;

        [Inject]
        public ActivityService Activity { get; set; }

        [Inject]
        public StatService Stat { get; set; }


        protected override void Apply(uint creature, int durationTicks)
        {
            AssignCommand(creature, () =>
            {
                ActionPlayAnimation(AnimationType.LoopingSitCross, 1f, 9999f);
            });

            var position = GetPosition(creature);

            SetLocalFloat(creature, RestPositionX, position.X);
            SetLocalFloat(creature, RestPositionY, position.Y);
            SetLocalFloat(creature, RestPositionZ, position.Z);

            Activity.SetBusy(creature, ActivityStatusType.Resting);

            DelayCommand(0.5f, () => CheckMovement(creature));

        }

        protected override void Remove(uint creature)
        {
            DeleteLocalFloat(creature, RestPositionX);
            DeleteLocalFloat(creature, RestPositionY);
            DeleteLocalFloat(creature, RestPositionZ);

            Activity.ClearBusy(creature);
        }

        protected override void Tick(uint creature)
        {
            var vitalityBonus = GetAbilityModifier(AbilityType.Vitality, creature);
            if (vitalityBonus < 0)
                vitalityBonus = 0;

            var hpAmount = 1 + vitalityBonus * 7;
            var epAmount = 1 + vitalityBonus * 3;

            if (hpAmount < 1)
                hpAmount = 1;
            if (epAmount < 1)
                epAmount = 1;

            if (GetHasFeat(FeatType.ClearMind, creature))
                epAmount *= 2;

            var currentHP = Stat.GetCurrentHP(creature);
            var maxHP = Stat.GetMaxHP(creature);

            if (currentHP < maxHP)
                ApplyEffectToObject(DurationType.Instant, EffectHeal(hpAmount), creature);

            Stat.RestoreEP(creature, epAmount);
        }

        private void CheckMovement(uint target)
        {
            if (!GetIsObjectValid(target) || GetIsDead(target))
                return;

            var position = GetPosition(target);

            var originalPosition = Vector(
                GetLocalFloat(target, RestPositionX),
                GetLocalFloat(target, RestPositionY),
                GetLocalFloat(target, RestPositionZ));

            // Player has moved since the effect started. Remove it.
            if (Math.Abs(position.X - originalPosition.X) > 0.1f ||
                Math.Abs(position.Y - originalPosition.Y) > 0.1f ||
                Math.Abs(position.Z - originalPosition.Z) > 0.1f)
            {
                IsFlaggedForRemoval = true;
            }

            DelayCommand(0.5f, () => CheckMovement(target));
        }
    }
}
