using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(GraspingDeathStatusEffect))]
    public class GraspingDeathStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.GraspingDeath;
        public override EffectIconType Icon => EffectIconType.GraspingDeath;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 6f;

        [Inject]
        public StatService Stat { get; set; }

        private const string EffectTag = "GRASPING_DEATH_EFFECT";

        private void ApplyDamage(uint creature)
        {
            var maxHP = Stat.GetMaxHP(creature);
            var damage = (int)(maxHP * 0.02f);

            if (damage < 1)
                damage = 1;

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Darkness), creature);
            });

            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDoom), creature);
            });
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            var effect = EffectMovementSpeedDecrease(20);
            effect = TagEffect(effect, EffectTag);
            ApplyEffectToObject(DurationType.Temporary, effect, creature, Frequency * durationTicks);

            ApplyDamage(creature);
        }

        protected override void Tick(uint creature)
        {
            ApplyDamage(creature);
        }

        protected override void Remove(uint creature)
        {
            RemoveEffectByTag(creature, EffectTag);
        }
    }
}
