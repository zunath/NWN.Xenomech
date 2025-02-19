using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(PoisonStatusEffect))]
    public class PoisonStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Poison;
        public override EffectIconType Icon => EffectIconType.Poison;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        private readonly StatService _stat;
        public PoisonStatusEffect(StatService stat)
        {
            _stat = stat;
        }

        public override LocaleString CanApply(uint creature)
        {
            var resist = _stat.GetResist(creature, ResistType.Poison);

            if (XMRandom.D100(1) <= resist)
            {
                return LocaleString.RESISTED;
            }

            return LocaleString.Empty;
        }

        protected override void Tick(uint creature)
        {
            var maxHP = _stat.GetMaxHP(creature);
            var damage = (int)(maxHP * 0.01f);

            if (damage < 1)
                damage = 1;

            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Acid), creature);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPoisonSmall), creature);
        }
    }
}
