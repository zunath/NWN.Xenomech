using System;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect
{
    public abstract class StatusEffectBase: IStatusEffect
    {
        private bool _isPermanent;
        private int _durationTicks;
        private DateTime _lastRun;

        public string Id { get; }
        public abstract LocaleString Name { get; }
        public abstract EffectIconType Icon { get; }
        public abstract bool IsStackable { get; }
        public bool IsFlaggedForRemoval { get; protected set; }
        public virtual bool SendsApplicationMessage => true;
        public virtual bool SendsWornOffMessage => true;
        public abstract float Frequency { get; }
        public virtual bool IsRemovedOnJobChange => true;
        public StatGroup Stats { get; }
        public virtual List<Type> MorePowerfulEffectTypes { get; }
        public virtual List<Type> LessPowerfulEffectTypes { get; }

        protected StatusEffectBase()
        {
            Id = Guid.NewGuid().ToString();
            Stats = new StatGroup();
            MorePowerfulEffectTypes = new List<Type>();
            LessPowerfulEffectTypes = new List<Type>();
        }

        public virtual LocaleString CanApply(uint creature) { return LocaleString.Empty; }

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
