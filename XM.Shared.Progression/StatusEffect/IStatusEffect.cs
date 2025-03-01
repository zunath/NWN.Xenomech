using System;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect
{
    public interface IStatusEffect
    {
        string Id { get; }
        uint Source { get; }
        StatusEffectActivationType ActivationType { get; }
        StatusEffectSourceType SourceType { get; }
        LocaleString Name { get; }
        EffectIconType Icon { get; }
        StatusEffectStackType StackingType { get; }
        bool IsFlaggedForRemoval { get; }
        bool SendsApplicationMessage { get; }
        bool SendsWornOffMessage { get; }
        bool IsRemovedOnJobChange { get; }
        float Frequency { get; }
        public StatGroup StatGroup { get; }
        public List<Type> MorePowerfulEffectTypes { get; }
        public List<Type> LessPowerfulEffectTypes { get; }
        LocaleString CanApply(uint creature);
        void ApplyEffect(uint source, uint creature, int durationTicks);
        void RemoveEffect(uint creature);
        void TickEffect(uint creature);
        void OnHitEffect(uint creature, uint target, int damage);
    }
}
