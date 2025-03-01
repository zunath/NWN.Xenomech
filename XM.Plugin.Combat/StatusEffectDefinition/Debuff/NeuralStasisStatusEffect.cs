using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(NeuralStasisStatusEffect))]
    public class NeuralStasisStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.NeuralStasis;
        public override EffectIconType Icon => EffectIconType.NeuralStasis;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        public NeuralStasisStatusEffect()
        {
            StatGroup.Stats[StatType.Slow] = 20;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpSlow), creature);
        }
    }
}
