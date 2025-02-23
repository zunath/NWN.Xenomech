using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(RuptureStatusEffect))]
    public class RuptureStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Rupture;
        public override EffectIconType Icon => EffectIconType.Rupture;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        [Inject]
        public StatService Stat { get; set; }

        private void ApplyDamage(uint creature)
        {
            var maxHP = Stat.GetMaxHP(creature);
            var damage = (int)(maxHP * 0.01f);

            if (damage < 1)
                damage = 1;

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Mind), creature);
            });

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDestruction), creature);
            });
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyDamage(creature);
        }

        protected override void Tick(uint creature)
        {
            ApplyDamage(creature);
        }
    }
}
