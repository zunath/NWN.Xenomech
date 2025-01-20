using System;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Combat.StatusEffect
{
    internal interface IStatusEffect
    {
        string Id { get; }
        LocaleString Name { get; }
        EffectIconType Icon { get; }
        bool IsStackable { get; }
        bool IsFlaggedForRemoval { get; }
        bool SendsApplicationMessage { get; }
        bool SendsWornOffMessage { get; }
        float Frequency { get; }
        int HPRegen { get; }
        int EPRegen { get; }
        int Defense { get; }
        int Evasion { get; }
        int Accuracy { get; }
        int Attack { get; }
        int EtherAttack { get; }
        Dictionary<ResistType, int> Resists { get; }
        void ApplyEffect(uint creature, int durationTicks);
        void RemoveEffect(uint creature);
        void TickEffect(uint creature);
    }
}
