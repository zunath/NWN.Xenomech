using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(ScourgeStatusEffect))]
    public class ScourgeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Scourge;
        public override EffectIconType Icon => EffectIconType.Scourge;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.OnHit;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        [Inject]
        public StatusEffectService Status { get; set; }

        protected override void OnHit(uint creature, uint target, int damage)
        {
            if (XMRandom.D100(1) <= 20)
            {
                Status.ApplyStatusEffect<ScourgeDebuffStatusEffect>(creature, target, 3);
            }
        }
    }
}
