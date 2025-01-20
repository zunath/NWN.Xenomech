using System;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Combat.StatusEffect
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


        protected StatusEffectBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        protected abstract void Apply(uint creature);
        public void ApplyEffect(uint creature, int durationTicks)
        {
            if (durationTicks < 0)
                _isPermanent = true;

            _lastRun = DateTime.UtcNow;
            _durationTicks = durationTicks;
            Apply(creature);
        }

        protected abstract void Remove(uint creature);
        public void RemoveEffect(uint creature)
        {
            Remove(creature);
        }

        protected abstract void Tick(uint creature);
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
