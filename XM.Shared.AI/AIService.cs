using System;
using System.Collections.Generic;
using Anvil.Services;
using Microsoft.VisualBasic;
using XM.AI.AITrees;
using XM.AI.Enmity;
using XM.Progression.Stat;
using XM.Shared.Core.EventManagement;

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
        private const int BatchSize = 10; 
        private const double UpdateInterval = 2.0f;
        private readonly Dictionary<uint, DateTime> _lastUpdateTimestamps = new();

        private readonly Dictionary<uint, IAITree> _creatureAITrees = new();

        private readonly EnmityService _enmity;

        private readonly StatService _stat;

        public AIService(
            XMEventService @event, 
            EnmityService enmity,
            StatService stat)
        {
            _enmity = enmity;
            _stat = stat;

            @event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
            @event.Subscribe<CreatureEvent.OnDeath>(OnCreatureDeath);
        }


        private void OnSpawnCreated(uint creature)
        {
            SetAIFlags(creature, AIFlag.ReturnHome);

            _creatureAITrees[creature] = new StandardAITree(creature, this, _enmity, _stat);
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
