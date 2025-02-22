﻿using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(FireWardStatusEffect))]
    public class FireWardStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.FireWard;
        public override EffectIconType Icon => EffectIconType.FireWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public FireWardStatusEffect()
        {
            Stats.Resists[ResistType.Fire] = 50;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraPulseRedBlack), creature, 2f);
        }
    }
}
