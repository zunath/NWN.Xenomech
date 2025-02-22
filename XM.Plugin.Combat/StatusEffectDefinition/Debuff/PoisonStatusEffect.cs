using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(PoisonStatusEffect))]
    public class PoisonStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Poison;
        public override EffectIconType Icon => EffectIconType.Poison;
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
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Acid), creature);
            });

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPoisonSmall), creature);
            });
        }

        public override LocaleString CanApply(uint creature)
        {
            var resist = Stat.GetResist(creature, ResistType.Poison);

            if (XMRandom.D100(1) <= resist)
            {
                return LocaleString.RESISTED;
            }

            return LocaleString.Empty;
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
