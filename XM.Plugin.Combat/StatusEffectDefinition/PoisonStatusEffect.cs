using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
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
