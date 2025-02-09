using System;
using System.Collections.Generic;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.AI.Enmity;
using XM.AI.Event;
using XM.AI.Scorer;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.AI
{
    [ServiceBinding(typeof(AIService))]
    [ServiceBinding(typeof(IDisposable))]
    [ServiceBinding(typeof(IUpdateable))]
    internal class AIService: 
        IUpdateable,
        IDisposable
    {
        private const string AIFlagsVariable = "AI_FLAGS";
        private const string AggroAOETag = "AGGRO_AOE";

        private const int BatchSize = 10; 
        private const double UpdateInterval = 2.0f;
        private readonly Dictionary<uint, DateTime> _lastUpdateTimestamps = new();

        private readonly Dictionary<uint, IAIContext> _creatureAITrees = new();

        private readonly XMEventService _event;
        private readonly EnmityService _enmity;
        private readonly AIServiceCollection _services;

        public AIService(
            XMEventService @event, 
            EnmityService enmity,
            AIServiceCollection services)
        {
            _event = @event;
            _enmity = enmity;
            _services = services;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
            _event.Subscribe<CreatureEvent.OnDeath>(OnCreatureDeath);

            _event.Subscribe<AIEvent.OnEnterAggroAOE>(OnEnterAggroAOE);
            _event.Subscribe<AIEvent.OnExitAggroAOE>(OnExitAggroAOE);

            _event.Subscribe<NWNXEvent.OnDmToggleAiAfter>(OnDMToggleAI);
        }


        private void OnSpawnCreated(uint creature)
        {
            SetAIFlags(creature, AIFlag.ReturnHome);
            _creatureAITrees[creature] = new AIContext(creature, _services);
            LoadAggroEffect(creature);
        }

        private void OnCreatureDeath(uint creature)
        {
            if (_creatureAITrees.ContainsKey(creature))
                _creatureAITrees.Remove(creature);
        }

        private void SetAIFlags(uint creature, AIFlag flags)
        {
            var flagValue = (int)flags;
            SetLocalInt(creature, AIFlagsVariable, flagValue);
        }

        public AIFlag GetAIFlags(uint creature)
        {
            var flagValue = GetLocalInt(creature, AIFlagsVariable);
            return (AIFlag)flagValue;
        }

        private void ProcessBehaviorTrees()
        {
            var processedCount = 0;
            var now = DateTime.UtcNow;

            foreach (var (creature, ai) in _creatureAITrees)
            {
                if (processedCount >= BatchSize)
                {
                    break;
                }

                if (!_lastUpdateTimestamps.TryGetValue(creature, out var lastUpdate) ||
                    (now - lastUpdate).TotalSeconds >= UpdateInterval)
                {
                    _enmity.TickVolatileEnmity(creature);
                    ai.Update(now);
                    _lastUpdateTimestamps[creature] = now;
                    processedCount++;
                }
            }
        }

        public void Update()
        {
            ProcessBehaviorTrees();
        }

        private void OnDMToggleAI(uint dm)
        {
            var count = Convert.ToInt32(EventsPlugin.GetEventData("NUM_TARGETS"));

            for (var x = 1; x <= count; x++)
            {
                var target = StringToObject(EventsPlugin.GetEventData($"TARGET_{x}"));

                if (!_creatureAITrees.ContainsKey(target))
                    continue;

                var isEnabled = _creatureAITrees[target].ToggleAI();

                var targetName = GetName(target);
                var toggleText = isEnabled
                    ? ColorToken.Green(LocaleString.ENABLED.ToLocalizedString())
                    : ColorToken.Red(LocaleString.DISABLED.ToLocalizedString());
                SendMessageToPC(dm, LocaleString.AIForCreatureXHasBeenY.ToLocalizedString(targetName, toggleText));
            }
        }

        private void LoadAggroEffect(uint creature)
        {
            var effect = SupernaturalEffect(EffectAreaOfEffect(AreaOfEffectType.AOEPerCustomAOE));
            effect = TagEffect(effect, AggroAOETag);
            ApplyEffectToObject(DurationType.Permanent, effect, creature);
        }

        private void OnEnterAggroAOE(uint creature)
        {
            var context = _creatureAITrees[creature];
            var entering = GetEnteringObject();
            context.AddFriendly(entering);
        }
        private void OnExitAggroAOE(uint creature)
        {
            var context = _creatureAITrees[creature];
            var exiting = GetExitingObject();
            context.AddFriendly(exiting);
        }

        public void Dispose()
        {
            _creatureAITrees.Clear();
        }



        [ScriptHandler("bread_test3")]
        public void Test3()
        {
            var npc = GetObjectByTag("goblintest");
            ApplyEffectToObject(DurationType.Instant, EffectDamage(1), npc);

            SendMessageToPC(GetLastUsedBy(), $"Goblin HP: {GetCurrentHitPoints(npc)} / {GetMaxHitPoints(npc)}");
        }
    }
}
