using System;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffect
{
    internal abstract class StatusEffectBase: IStatusEffect
    {
        private bool _isPermanent;
        private int _durationTicks;
        private DateTime _lastRun;

        public string Id { get; }
        public abstract LocaleString Name { get; }
        public abstract EffectIconType Icon { get; }
        public abstract bool IsStackable { get; }
        public abstract bool IsFlaggedForRemoval { get; protected set; }
        public abstract bool SendsApplicationMessage { get; }
        public abstract bool SendsWornOffMessage { get; }
        public abstract float Frequency { get; }
        public virtual bool IsRemovedOnJobChange => true;
        public virtual int HPRegen => 0;
        public virtual int EPRegen => 0;
        public virtual int Defense => 0;
        public virtual int Evasion => 0;
        public virtual int Accuracy => 0;
        public virtual int Attack => 0;
        public virtual int EtherAttack => 0;
        public Dictionary<ResistType, int> Resists => new ResistCollection();


        protected StatusEffectBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        protected virtual void Apply(uint creature) { }
        public void ApplyEffect(uint creature, int durationTicks)
        {
            if (durationTicks < 0)
                _isPermanent = true;

            _lastRun = DateTime.UtcNow;
            _durationTicks = durationTicks;
            Apply(creature);
        }

        protected virtual void Remove(uint creature) { }
        public void RemoveEffect(uint creature)
        {
            Remove(creature);
        }

        protected virtual void Tick(uint creature) { }
        public void TickEffect(uint creature)
        {
            var currentTime = DateTime.UtcNow;
            if ((currentTime - _lastRun).TotalSeconds < Frequency)
            {
                return;
            }

            _lastRun = currentTime;

            // Reduce duration ticks and flag for removal if expired
            if (!_isPermanent && --_durationTicks <= 0)
            {
                IsFlaggedForRemoval = true;
            }

            Tick(creature);
        }

    }
}
