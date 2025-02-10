using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Anvil;
using Anvil.API;
using Anvil.Services;
using NLog;
using NWN.Core.NWNX;
using XM.AI.Enmity;
using XM.AI.Event;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Json;
using XM.Shared.Core.Localization;

namespace XM.AI
{
    [ServiceBinding(typeof(AIService))]
    [ServiceBinding(typeof(IInitializable))]
    [ServiceBinding(typeof(IDisposable))]
    [ServiceBinding(typeof(IUpdateable))]
    internal class AIService :
        IInitializable,
        IUpdateable,
        IDisposable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private const string AIFlagsVariable = "AI_FLAGS";
        private const string AggroAOETag = "AGGRO_AOE";

        private const int BatchSize = 10;
        private const double UpdateInterval = 2.0f;
        private readonly Dictionary<uint, DateTime> _lastUpdateTimestamps = new();
        private readonly Dictionary<uint, IAIContext> _creatureAITrees = new();
        private readonly Dictionary<string, CachedCreatureFeats> _creatureFeats = new();

        private readonly AbilityService _ability;
        private readonly XMEventService _event;
        private readonly EnmityService _enmity;
        private readonly AIServiceCollection _services;
        private readonly XMSettingsService _settings;

        public AIService(
            AbilityService ability,
            XMEventService @event,
            EnmityService enmity,
            AIServiceCollection services,
            XMSettingsService settings)
        {
            _ability = ability;
            _event = @event;
            _enmity = enmity;
            _services = services;
            _settings = settings;

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
        public void Init()
        {
            LoadCachedCreatureFeats();
        }

        private void LoadCachedCreatureFeats()
        {
            var fileName = _settings.ResourcesDirectory + "CachedCreatures.json";
            if (!File.Exists(fileName))
            {
                _logger.Error($"WARNING: Could not locate '{Path.GetFullPath(fileName)}'. AI system will not function.");
                return;
            }

            var json = File.ReadAllText(fileName);
            var cachedCreatures = XMJsonUtility.Deserialize<List<CreatureFeatsFile>>(json);

            foreach (var cachedCreature in cachedCreatures)
            {
                var resref = cachedCreature.Resref;
                _creatureFeats[resref] = new CachedCreatureFeats();

                foreach (var feat in cachedCreature.Feats.OrderByDescending(f => _ability.GetLevelAcquired(f)))
                {
                    if (!_ability.IsFeatRegistered(feat))
                        continue;
                    
                    var ability = _ability.GetAbilityDetail(feat);
                    var type = ability.FeatType;
                    var spellIdStr = Get2DAString("feat", "SPELLID", (int)type);
                    var spellId = Convert.ToInt32(spellIdStr);
                    var targetTypeStr = Get2DAString("spells", "TargetType", spellId);
                    var targetTypes = (SpellTargetTypes)Convert.ToInt32(targetTypeStr, 16);

                    if (!_creatureFeats[resref].ContainsKey(ability.Classification))
                        _creatureFeats[resref][ability.Classification] = new Dictionary<AITargetType, HashSet<FeatType>>();

                    if (targetTypes.HasFlag(SpellTargetTypes.Self))
                    {
                        if (!_creatureFeats[resref][ability.Classification].ContainsKey(AITargetType.Self))
                            _creatureFeats[resref][ability.Classification][AITargetType.Self] = new HashSet<FeatType>();

                        _creatureFeats[resref][ability.Classification][AITargetType.Self].Add(feat);
                    }

                    if (targetTypes.HasFlag(SpellTargetTypes.Creature))
                    {
                        if (!_creatureFeats[resref][ability.Classification].ContainsKey(AITargetType.Others))
                            _creatureFeats[resref][ability.Classification][AITargetType.Others] = new HashSet<FeatType>();

                        _creatureFeats[resref][ability.Classification][AITargetType.Others].Add(feat);
                    }
                }
            }
        }

        private void OnSpawnCreated(uint creature)
        {
            var resref = GetResRef(creature);
            SetAIFlags(creature, AIFlag.ReturnHome);
            var feats = _creatureFeats.ContainsKey(resref)
                ? _creatureFeats[resref]
                : new CachedCreatureFeats();
            _creatureAITrees[creature] = new AIContext(creature, GetAIFlags(creature), feats, _services);
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

        private AIFlag GetAIFlags(uint creature)
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
            _lastUpdateTimestamps.Clear();
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
