using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.AI.AITrees;
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
            Console.WriteLine($"OBJECT_SELF = {OBJECT_SELF}, creature = {creature}, name = {GetName(creature)}");
            SetAIFlag(creature, AIFlag.ReturnHome);

            _creatureAITrees[creature] = new TestAI(creature);
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
            foreach (var (_, tree) in _creatureAITrees)
            {
                tree.Update((float)Time.DeltaTime.TotalSeconds);
            }
        }
    }
}
