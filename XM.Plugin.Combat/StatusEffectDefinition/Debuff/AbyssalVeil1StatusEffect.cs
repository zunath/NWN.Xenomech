using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(AbyssalVeil1StatusEffect))]
    public class AbyssalVeil1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.AbyssalVeilI;
        public override EffectIconType Icon => EffectIconType.AbyssalVeil1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        [Inject]
        public StatService Stat { get; set; }

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(AbyssalVeil2StatusEffect)
        ];

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }

        protected override void Tick(uint creature)
        {
            const int DrainAmount = 2;
            var amount = DrainAmount;
            var ep = Stat.GetCurrentEP(creature);
            if (ep < DrainAmount)
                amount = ep;

            if (amount <= 0)
                return;

            Stat.ReduceEP(creature, amount);
            Stat.RestoreEP(Source, amount);

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.BeamOdd), creature);
            });
        }
    }
}