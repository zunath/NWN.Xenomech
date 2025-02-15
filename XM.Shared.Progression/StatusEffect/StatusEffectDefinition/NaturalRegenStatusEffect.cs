using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(NaturalRegenStatusEffect))]
    public class NaturalRegenStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.NaturalRegen;
        public override EffectIconType Icon => EffectIconType.Invalid;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override bool SendsApplicationMessage => false;
        public override bool SendsWornOffMessage => false;
        public override float Frequency => 3f;

        [Inject]
        public StatService Stat { get; set; }

        protected override void Tick(uint creature)
        {
            var hpRegen = Stat.GetHPRegen(creature);
            var epRegen = Stat.GetEPRegen(creature);
            var doHPHeal = Stat.GetCurrentHP(creature) < Stat.GetMaxHP(creature);
            var doEPHeal = Stat.GetCurrentEP(creature) < Stat.GetMaxEP(creature);

            if (hpRegen > 0 && doHPHeal)
            {
                ApplyEffectToObject(DurationType.Instant, EffectHeal(hpRegen), creature);
            }

            if (epRegen > 0 && doEPHeal)
            {
                Stat.RestoreEP(creature, epRegen);
            }
        }
    }
}
