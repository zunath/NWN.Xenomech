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
        LocaleString Name { get; }
        EffectIconType Icon { get; }
        bool IsStackable { get; }
        bool IsFlaggedForRemoval { get; }
        bool SendsApplicationMessage { get; }
        bool SendsWornOffMessage { get; }
        bool IsRemovedOnJobChange { get; }
        float Frequency { get; }
        public StatGroup Stats { get; }
        public List<Type> MorePowerfulEffectTypes { get; }
        public List<Type> LessPowerfulEffectTypes { get; }
        LocaleString CanApply(uint creature);
        void ApplyEffect(uint creature, int durationTicks);
        void RemoveEffect(uint creature);
        void TickEffect(uint creature);
    }
}
