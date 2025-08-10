using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Inventory;
using XM.Progression.Event;
using XM.Shared.Core.Entity;
using XM.Progression.Job.JobDefinition;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;
using XM.UI.Event;

namespace XM.Progression.Job
{
    [ServiceBinding(typeof(JobService))]
    public class JobService
    {
        public const int LevelCap = 50;
        public const int ResonanceNodeLevelAcquisitionRate = 5; // One every 5 levels

        private readonly IList<IJobDefinition> _jobDefinitionsList;
        private readonly Dictionary<JobType, IJobDefinition> _jobDefinitions = new();

        private readonly Dictionary<JobType, ClassType> _jobsToClasses = new();
        private readonly Dictionary<ClassType, JobType> _classesToJobs = new();

        private readonly InventoryService _inventory;
        private readonly DBService _db;
        private readonly XMEventService _event;
        private readonly XPChart _xp;
        private readonly DeltaXPChart _deltaXP;
        private readonly StatService _stat;
        private readonly PartyService _party;

        public JobService(
            InventoryService inventory, 
            DBService db,
            XMEventService @event,
            XPChart xp,
            DeltaXPChart deltaXP,
            StatService stat,
            PartyService party,
            IList<IJobDefinition> jobDefinitionsList)
        {
            _inventory = inventory;
            _db = db;
            _event = @event;
            _xp = xp;
            _deltaXP = deltaXP;
            _stat = stat;
            _party = party;
            _jobDefinitionsList = jobDefinitionsList;

            CacheData();
            RegisterEvents();
            SubscribeEvents();
        }

        private void CacheData()
        {
            foreach (var definition in _jobDefinitionsList)
            {
                _jobDefinitions[definition.Type] = definition;
                _jobsToClasses[definition.Type] = definition.NWNClass;
                _classesToJobs[definition.NWNClass] = definition.Type;
            }
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<JobEvent.PlayerChangedJobEvent>(ProgressionEventScript.PlayerChangedJobScript);
            _event.RegisterEvent<JobEvent.PlayerLeveledUpEvent>(ProgressionEventScript.PlayerLeveledUpScript);
            _event.RegisterEvent<JobEvent.JobFeatAddedEvent>(ProgressionEventScript.JobFeatAddScript);
            _event.RegisterEvent<JobEvent.JobFeatRemovedEvent>(ProgressionEventScript.JobFeatRemovedScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnPCInitialized>(InitializeJob);
            _event.Subscribe<CreatureEvent.OnDeath>(CreatureDeathXP);
        }


        private void InitializeJob(uint player)
        {
            var @class = GetClassByPosition(1, player);
            var job = _classesToJobs[@class];

            ChangeJob(player, job, []);
        }

        internal Dictionary<JobType, IJobDefinition> GetAllJobDefinitions()
        {
            return _jobDefinitions.ToDictionary(x => x.Key, y => y.Value);
        }

        internal IJobDefinition GetJobDefinition(JobType job)
        {
            return _jobDefinitions[job];
        }

        public IJobDefinition GetActiveJob(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only PCs can have jobs.");

            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            var activeJob = JobType.FromValue(dbPlayerJob.ActiveJobCode);
            return _jobDefinitions[activeJob];
        }

        public int GetJobLevel(uint player, JobType job)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only PCs can have jobs.");

            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            var level = dbPlayerJob.JobLevels.ContainsKey((int)job)
                ? dbPlayerJob.JobLevels[(int)job]
                : 0;
            return level;
        }

        public Dictionary<JobType, int> GetJobLevels(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only PCs can have jobs.");

            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);

            return dbPlayerJob.JobLevels.ToDictionary(x => JobType.FromValue(x.Key), y => y.Value);
        }
        public void GiveXP(uint player, int xp, bool includeModifiers = true)
        {
            if (!GetIsPC(player) ||
                GetIsDM(player) ||
                GetIsDMPossessed(player))
                return;

            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId);
            var job = JobType.FromValue(dbPlayerJob.ActiveJobCode);
            var level = dbPlayerJob.JobLevels[(int)job];
            var xpRequired = _xp[level];
            var levelsGained = new List<int>();

            if (includeModifiers)
            {
                var modifier = _stat.GetXPModifier(player);
                xp = (int)(xp * modifier);
            }

            dbPlayerJob.JobXP[(int)job] += xp;

            while (dbPlayerJob.JobXP[(int)job] >= xpRequired)
            {
                if (level >= LevelCap)
                {
                    dbPlayerJob.JobXP[(int)job] = xpRequired - 1;
                    break;
                }

                level++;
                dbPlayerJob.JobLevels[(int)job] = level;
                dbPlayerJob.JobXP[(int)job] -= xpRequired;
                xpRequired = _xp[level];

                levelsGained.Add(level);
            }

            _db.Set(dbPlayerJob);

            var definition = GetJobDefinition(job);
            foreach (var gainedLevel in levelsGained)
            {
                _event.PublishEvent(player, new JobEvent.PlayerLeveledUpEvent(definition, gainedLevel));

                var name = GetName(player);
                var levelUpMessage = LocaleString.XAttainsLevelY.ToLocalizedString(name, gainedLevel);
                Messaging.SendMessageNearbyToPlayers(player, levelUpMessage);
            }

            var message = LocaleString.YouEarnedExperience.ToLocalizedString();
            SendMessageToPC(player, message);

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }

        public void ChangeJob(
            uint player, 
            JobType job,
            List<FeatType> resonanceFeats)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only players may change jobs.");

            _inventory.UnequipAllItems(player);

            var definition = GetJobDefinition(job);
            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId);
            var level = dbPlayerJob.JobLevels[(int)job];
            var currentJob = GetActiveJob(player);
            var featsToAdd = new List<FeatType>();
            var featsToRemove = new List<FeatType>();

            for(var index = dbPlayerJob.ResonanceFeats.Count-1; index >= 0; index--)
            {
                var feat = dbPlayerJob.ResonanceFeats[index];
                CreaturePlugin.RemoveFeat(player, feat);
                dbPlayerJob.ResonanceFeats.Remove(feat);
                featsToRemove.Add(feat);
            }

            foreach (var (_, feat) in currentJob.FeatAcquisitionLevels)
            {
                CreaturePlugin.RemoveFeat(player, feat);
                featsToRemove.Add(feat);
            }

            var jobFeats = definition
                .FeatAcquisitionLevels
                .Where(x => x.Key <= level)
                .Select(s => s.Value)
                .ToList();

            foreach (var feat in jobFeats)
            {
                CreaturePlugin.AddFeatByLevel(player, feat, 1);
                featsToRemove.Add(feat);
            }

            foreach (var feat in resonanceFeats)
            {
                CreaturePlugin.AddFeatByLevel(player, feat, 1);
                dbPlayerJob.ResonanceFeats.Add(feat);
                featsToAdd.Add(feat);
            }

            dbPlayerJob.ActiveJobCode = (int)job;
            _db.Set(dbPlayerJob);

            var @class = _jobsToClasses[job];
            CreaturePlugin.SetClassByPosition(player, 0, @class);

            foreach (var feat in featsToRemove)
            {
                _event.PublishEvent(player, new JobEvent.JobFeatRemovedEvent(feat));
            }

            foreach (var feat in featsToAdd)
            {
                _event.PublishEvent(player, new JobEvent.JobFeatAddedEvent(feat));
            }

            _event.PublishEvent(player, new JobEvent.PlayerChangedJobEvent((JobDefinitionBase)definition, level));

            var name = definition.Name.ToLocalizedString();
            var message = LocaleString.JobChangedToX.ToLocalizedString(ColorToken.Green(name));
            SendMessageToPC(player, message);
        }

        private void CreatureDeathXP(uint creature)
        {
            var killer = GetLastKiller();
            var npcStats = _stat.GetNPCStats(creature);
            var party = _party.GetAllPartyMembersWithinRange(killer, 20f, false);
            var level = 0;

            foreach (var member in party)
            {
                var memberLevel = _stat.GetLevel(member);
                if (level < memberLevel)
                {
                    level = memberLevel;
                }
            }

            var delta = level - npcStats.Level;
            var baseXP = _deltaXP.GetBaseXP(delta);
            if (baseXP <= 0)
                return;

            var divisor = party.Count;
            if (divisor > 4)
                divisor = 4;

            var xp = baseXP / divisor;
            foreach (var member in party)
            {
                GiveXP(member, xp);
            }
        }

        [ScriptHandler("bread_test1")]
        public void GiveXPTest()
        {
            var player = GetLastUsedBy();
            var bread = OBJECT_SELF;
            var xp = GetLocalInt(bread, "XP");

            GiveXP(player, xp);
        }
    }
}
