using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.AI.AITrees;
using XM.AI.Enmity;
using XM.Shared.Core.EventManagement;

namespace XM.AI
{
    [ServiceBinding(typeof(AIService))]
    [ServiceBinding(typeof(IDisposable))]
    internal class AIService: 
        IDisposable
    {
        private const string AIFlagsVariable = "AI_FLAGS";

        private readonly Dictionary<uint, IAITree> _creatureAITrees = new();

        private readonly EnmityService _enmity;

        public AIService(
            XMEventService @event, 
            EnmityService enmity,
            SchedulerService scheduler)
        {
            _enmity = enmity;

            @event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
            @event.Subscribe<CreatureEvent.OnDeath>(OnCreatureDeath);

            scheduler.ScheduleRepeating(ProcessBehaviorTrees, TimeSpan.FromSeconds(2));
        }

        private void ProcessBehaviorTrees()
        {
            foreach (var (creature, ai) in _creatureAITrees)
            {
                _enmity.TickVolatileEnmity(creature);
                ai.Update(DateTime.UtcNow);
            }
        }

        private void OnSpawnCreated(uint creature)
        {
            SetAIFlags(creature, AIFlag.ReturnHome);

            _creatureAITrees[creature] = new TestAITree(creature, this, _enmity);
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

        public void Dispose()
        {
            _creatureAITrees.Clear();
        }






        [ScriptHandler("bread_test")]
        public void Test()
        {
            var npc = GetObjectByTag("goblintest");
            var boy = GetObjectByTag("boy");

            _enmity.ModifyEnmity(boy, npc, EnmityType.Volatile, 500);

            var table = _enmity.GetEnmityTable(npc);
            SendMessageToPC(GetLastUsedBy(), $"Boy enmity = {table[boy].TotalEnmity}");
        }
        [ScriptHandler("bread_test2")]
        public void Test2()
        {
            var npc = GetObjectByTag("goblintest");
            var girl = GetObjectByTag("girl");

            _enmity.ModifyEnmity(girl, npc, EnmityType.Volatile, 500);

            var table = _enmity.GetEnmityTable(npc);
            SendMessageToPC(GetLastUsedBy(), $"Girl enmity = {table[girl].TotalEnmity}");
        }

    }
}
