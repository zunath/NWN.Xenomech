using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Event;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect
{
    [ServiceBinding(typeof(StatusEffectService))]
    public class StatusEffectService
    {
        private const string StatusEffectTag = "STATUS_EFFECT";
        private const float Interval = 1f;

        private readonly Dictionary<uint, CreatureStatusEffect> _creatureEffects = new();

        private readonly XMEventService _event;
        private readonly InjectionService _injection;

        public StatusEffectService(
            XMEventService @event,
            InjectionService injection)
        {
            _event = @event;
            _injection = injection;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<StatusEffectEvent.OnApplyStatusEffect>(ProgressionEventScript.OnApplyStatusEffectScript);
            _event.RegisterEvent<StatusEffectEvent.OnRemoveStatusEffect>(ProgressionEventScript.OnRemoveStatusEffectScript);
            _event.RegisterEvent<StatusEffectEvent.OnStatusEffectInterval>(ProgressionEventScript.OnStatusEffectIntervalScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<StatusEffectEvent.OnApplyStatusEffect>(OnApplyNWNStatusEffect);
            _event.Subscribe<StatusEffectEvent.OnRemoveStatusEffect>(OnRemoveNWNStatusEffect);
            _event.Subscribe<StatusEffectEvent.OnStatusEffectInterval>(OnNWNStatusEffectInterval);

            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnPlayerEnter);

            _event.Subscribe<JobEvent.PlayerChangedJobEvent>(OnChangeJobs);
        }


        private void OnPlayerEnter(uint module)
        {
            var player = GetEnteringObject();
            ApplyNWNEffect(player);
        }

        private void ApplyNWNEffect(uint creature)
        {
            if (HasEffectByTag(creature, StatusEffectTag))
                return;

            var effect = EffectRunScript(
                ProgressionEventScript.OnApplyStatusEffectScript,
                ProgressionEventScript.OnRemoveStatusEffectScript,
                ProgressionEventScript.OnStatusEffectIntervalScript,
                Interval);
            effect = TagEffect(effect, StatusEffectTag);
            effect = SupernaturalEffect(effect);

            ApplyEffectToObject(DurationType.Permanent, effect, creature);
        }

        private void OnApplyNWNStatusEffect(uint player)
        {
            _creatureEffects[player] = new CreatureStatusEffect();
        }

        private void OnRemoveNWNStatusEffect(uint player)
        {
            if (_creatureEffects.ContainsKey(player))
                _creatureEffects.Remove(player);
        }

        private void OnNWNStatusEffectInterval(uint creature)
        {
            if (!_creatureEffects.ContainsKey(creature))
            {
                RemoveEffectByTag(creature, StatusEffectTag);
                return;
            }

            var effects = _creatureEffects[creature];

            foreach (var effect in effects.GetAllTickEffects())
            {
                if (effect.ActivationType != StatusEffectActivationType.Tick)
                    continue;

                if (effect.IsFlaggedForRemoval)
                {
                    RemoveStatusEffect(effect.GetType(), creature);
                }
                else
                {
                    effect.TickEffect(creature);
                }
            }
        }

        public CreatureStatusEffect GetCreatureStatusEffects(uint creature)
        {
            return !_creatureEffects.ContainsKey(creature) 
                ? new CreatureStatusEffect() 
                : _creatureEffects[creature];
        }

        public void ApplyPermanentStatusEffect<T>(uint source, uint creature)
            where T: IStatusEffect
        {
            ApplyStatusEffect<T>(source, creature, -1);
        }

        public void ApplyPermanentStatusEffect(Type type, uint source, uint creature)
        {
            ApplyStatusEffect(type, source, creature, -1);
        }

        private void ApplyStatusEffect(Type type, uint source, uint creature, int durationTicks)
        {
            if (durationTicks <= 0)
            {
                SendMessageToPC(source, LocaleString.YourSpellWasResisted.ToLocalizedString());
                return;
            }

            ApplyNWNEffect(creature);

            var statusEffect = (IStatusEffect)Activator.CreateInstance(type);
            _injection.Inject(statusEffect);

            var canApply = statusEffect.CanApply(creature).ToLocalizedString();
            if (!string.IsNullOrWhiteSpace(canApply))
            {
                var message = LocaleString.EffectFailedToApplyX.ToLocalizedString(canApply);
                SendMessageToPC(creature, message);
                return;
            }

            foreach (var morePowerful in statusEffect.MorePowerfulEffectTypes)
            {
                if (HasEffect(morePowerful, creature))
                {
                    var message = LocaleString.AMorePowerfulEffectIsActive.ToLocalizedString();
                    SendMessageToPC(creature, message);
                    return;
                }
            }

            switch (statusEffect.StackingType)
            {
                case StatusEffectStackType.Disabled:
                case StatusEffectStackType.Invalid:
                    RemoveStatusEffect(type, creature);
                    break;
                case StatusEffectStackType.StackFromMultipleSources:
                    RemoveStatusEffect(type, creature, source);
                    break;
            }

            foreach (var lessPowerful in statusEffect.LessPowerfulEffectTypes)
            {
                RemoveStatusEffect(lessPowerful, creature);
            }

            _creatureEffects[creature].Add(statusEffect);
            statusEffect.ApplyEffect(source, creature, durationTicks);

            if (statusEffect.Icon != EffectIconType.Invalid)
            {
                var iconEffect = EffectIcon(statusEffect.Icon);
                iconEffect = TagEffect(iconEffect, statusEffect.Id);
                ApplyEffectToObject(DurationType.Permanent, iconEffect, creature);
            }

            if (statusEffect.SendsApplicationMessage)
            {
                var name = GetName(creature);
                var effectName = statusEffect.Name.ToLocalizedString();
                Messaging.SendMessageNearbyToPlayers(creature,
                    LocaleString.CreatureReceivesTheEffectOfX.ToLocalizedString(name, effectName));
            }
        }

        public void ApplyStatusEffect<T>(uint source, uint creature, int durationTicks)
            where T: IStatusEffect
        {
            var type = typeof(T);
            ApplyStatusEffect(type, source, creature, durationTicks);
        }

        private void RemoveStatusEffect(Type type, uint creature, uint source)
        {
            if (!_creatureEffects.ContainsKey(creature))
                return;

            var hasSentMessage = false;
            var statusEffects = _creatureEffects[creature].GetAllEffects();
            foreach (var statusEffect in statusEffects)
            {
                if (statusEffect.GetType() == type)
                {
                    if (source == OBJECT_INVALID || statusEffect.Source == source)
                    {
                        if (statusEffect.SendsWornOffMessage && !hasSentMessage)
                        {
                            var name = GetName(creature);
                            var effectName = statusEffect.Name.ToLocalizedString();
                            Messaging.SendMessageNearbyToPlayers(creature,
                                LocaleString.CreaturesXEffectHasWornOff.ToLocalizedString(name, effectName));

                            hasSentMessage = true;
                        }
                    }

                    RemoveEffectByTag(creature, statusEffect.Id);
                    statusEffect.RemoveEffect(creature);
                    _creatureEffects[creature].Remove(statusEffect);
                }
            }
        }

        public void RemoveStatusEffect<T>(uint creature)
            where T: IStatusEffect
        {
            var type = typeof(T);
            RemoveStatusEffect(type, creature);
        }

        public void RemoveStatusEffect(Type type, uint creature)
        {
            RemoveStatusEffect(type, creature, OBJECT_INVALID);
        }

        public void RemoveStatusEffectBySourceType(uint creature, StatusEffectSourceType sourceType)
        {
            var creatureEffects = GetCreatureStatusEffects(creature);
            var effects = creatureEffects.GetAllBySourceType(sourceType);
            foreach (var effect in effects)
            {
                RemoveStatusEffect(effect.GetType(), creature);
            }
        }

        public bool HasEffect(Type type, uint creature)
        {
            if (!_creatureEffects.ContainsKey(creature))
                return false;

            return _creatureEffects[creature].HasEffect(type);
        }

        public bool HasEffect<T>(uint creature)
            where T : IStatusEffect
        {
            return HasEffect(typeof(T), creature);
        }

        private void OnChangeJobs(uint player)
        {
            if (!_creatureEffects.ContainsKey(player))
                return;

            foreach (var effect in _creatureEffects[player].GetAllEffects())
            {
                if (effect.IsRemovedOnJobChange)
                {
                    RemoveStatusEffect(effect.GetType(), player);
                }
            }
        }

        public void RunOnHitEffects(uint attacker, uint defender)
        {
            var effects = GetCreatureStatusEffects(attacker);

            foreach (var effect in effects.GetAllOnHitEffects())
            {
                effect.OnHitEffect(attacker, defender);
            }
        }
    }
}
