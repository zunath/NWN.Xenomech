using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(ScourgeDebuffStatusEffect))]
    public class ScourgeDebuffStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Scourge;
        public override EffectIconType Icon => EffectIconType.Scourge;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        [Inject]
        public StatService Stat { get; set; }

        private void ApplyDamage(uint creature)
        {
            var sourceHasDamage = GetCurrentHitPoints(Source) < GetMaxHitPoints(Source);
            var maxHP = Stat.GetMaxHP(creature);
            var damage = (int)(maxHP * 0.005f);

            if (damage < 1)
                damage = 1;

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Darkness), creature);
            });

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadEvil), creature);
            });

            if (sourceHasDamage)
            {
                ApplyEffectToObject(DurationType.Instant, EffectHeal(damage), Source);
            }
        }

        protected override void Tick(uint creature)
        {
            ApplyDamage(creature);
        }
    }
}
