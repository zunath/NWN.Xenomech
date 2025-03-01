using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(BurnStatusEffect))]
    public class BurnStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Burn;
        public override EffectIconType Icon => EffectIconType.Burn;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        [Inject]
        public StatService Stat { get; set; }

        private void ApplyDamage(uint creature)
        {
            var maxHP = Stat.GetMaxHP(creature);
            var damage = (int)(maxHP * 0.02f);

            if (damage < 1)
                damage = 1;

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Fire), creature);
            });

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpFlameSmall), creature);
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
