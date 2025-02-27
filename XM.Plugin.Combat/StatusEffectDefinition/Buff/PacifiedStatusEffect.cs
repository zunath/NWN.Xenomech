using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(FamiliarStatusEffect))]
    public class FamiliarStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Familiar;
        public override EffectIconType Icon => EffectIconType.Familiar;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f * 10f;

        [Inject]
        public StatService Stat { get; set; }

        protected override void Apply(uint creature, int durationTicks)
        {
            StatGroup.Stats[StatType.Might] = Stat.GetAttribute(creature, AbilityType.Might);
            StatGroup.Stats[StatType.Perception] = Stat.GetAttribute(creature, AbilityType.Perception);
            StatGroup.Stats[StatType.Vitality] = Stat.GetAttribute(creature, AbilityType.Vitality);
            StatGroup.Stats[StatType.Willpower] = Stat.GetAttribute(creature, AbilityType.Willpower);
            StatGroup.Stats[StatType.Agility] = Stat.GetAttribute(creature, AbilityType.Agility);
            StatGroup.Stats[StatType.Social] = Stat.GetAttribute(creature, AbilityType.Social);
        }
    }
}
