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
            Stats[StatType.Might] = Stat.GetAttribute(creature, AbilityType.Might);
            Stats[StatType.Perception] = Stat.GetAttribute(creature, AbilityType.Perception);
            Stats[StatType.Vitality] = Stat.GetAttribute(creature, AbilityType.Vitality);
            Stats[StatType.Willpower] = Stat.GetAttribute(creature, AbilityType.Willpower);
            Stats[StatType.Agility] = Stat.GetAttribute(creature, AbilityType.Agility);
            Stats[StatType.Social] = Stat.GetAttribute(creature, AbilityType.Social);
        }
    }
}
