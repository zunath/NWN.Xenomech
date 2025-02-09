using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.Event;
using XM.Progression.Event;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffect
{
    [ServiceBinding(typeof(StatusEffectService))]
    internal class StatusEffectService
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
            _event.RegisterEvent<StatusEffectEvent.OnApplyStatusEffect>(CombatEventScript.OnApplyStatusEffectScript);
            _event.RegisterEvent<StatusEffectEvent.OnRemoveStatusEffect>(CombatEventScript.OnRemoveStatusEffectScript);
            _event.RegisterEvent<StatusEffectEvent.OnStatusEffectInterval>(CombatEventScript.OnStatusEffectIntervalScript);
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
                CombatEventScript.OnApplyStatusEffectScript,
                CombatEventScript.OnRemoveStatusEffectScript,
                CombatEventScript.OnStatusEffectIntervalScript,
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

            foreach (var effect in effects.GetAllEffects())
            {
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

        public void ApplyPermanentStatusEffect<T>(uint creature)
            where T: IStatusEffect
        {
            ApplyStatusEffect<T>(creature, -1);
        }

        public void ApplyStatusEffect<T>(uint creature, int durationTicks)
            where T: IStatusEffect
        {
            ApplyNWNEffect(creature);

            var statusEffect = (IStatusEffect)Activator.CreateInstance<T>();
            _injection.Inject(statusEffect);

            if (!statusEffect.IsStackable)
            {
                RemoveStatusEffect<T>(creature);
            }

            _creatureEffects[creature].Add(statusEffect);
            statusEffect.ApplyEffect(creature, durationTicks);

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

        private void RemoveStatusEffect(Type type, uint creature)
        {
            var hasSentMessage = false;
            var statusEffects = _creatureEffects[creature].GetAllEffects();
            foreach (var statusEffect in statusEffects)
            {
                if (statusEffect.GetType() == type)
                {
                    if (statusEffect.SendsWornOffMessage && !hasSentMessage)
                    {
                        var name = GetName(creature);
                        var effectName = statusEffect.Name.ToLocalizedString();
                        Messaging.SendMessageNearbyToPlayers(creature,
                            LocaleString.CreaturesXEffectHasWornOff.ToLocalizedString(name, effectName));

                        hasSentMessage = true;
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
    }
}
