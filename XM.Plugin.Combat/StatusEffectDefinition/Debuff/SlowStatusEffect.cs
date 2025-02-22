﻿using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(SlowStatusEffect))]
    public class SlowStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Slow;
        public override EffectIconType Icon => EffectIconType.XMSlow;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public SlowStatusEffect()
        {
            Stats[StatType.Slow] = 15;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpSlow), creature);
        }
    }
}
