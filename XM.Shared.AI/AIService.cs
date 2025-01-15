using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.AI.AITrees;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.AI
{
    [ServiceBinding(typeof(AIService))]
    [ServiceBinding(typeof(IUpdateable))]
    internal class AIService: IUpdateable
    {
        private const string AIFlagsVariable = "AI_FLAGS";

        private readonly Dictionary<uint, IAITree> _creatureAITrees = new();

        public AIService(XMEventService @event)
        {
            @event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
            @event.Subscribe<CreatureEvent.OnDeathBefore>(OnCreatureDeath);
        }
        private void OnSpawnCreated(uint creature)
        {
            SetAIFlag(creature, AIFlag.ReturnHome);

            _creatureAITrees[creature] = new TestAITree(creature);
        }

        private void OnCreatureDeath(uint creature)
        {
            if (_creatureAITrees.ContainsKey(creature))
                _creatureAITrees.Remove(creature);
        }

        private void SetAIFlag(uint creature, AIFlag flags)
        {
            var flagValue = (int)flags;
            SetLocalInt(creature, AIFlagsVariable, flagValue);
        }

        public void Update()
        {
            foreach (var (_, ai) in _creatureAITrees)
            {
                ai.Update(DateTime.UtcNow);
            }
        }

        [ScriptHandler("bread_test")]
        public void Test()
        {
            var npc = GetObjectByTag("goblintest");
            ApplyEffectToObject(DurationType.Instant, EffectDamage(1), npc);

            SendMessageToPC(GetLastUsedBy(), $"goblin: {GetCurrentHitPoints(npc)} / {GetMaxHitPoints(npc)}");
        }
    }
}
