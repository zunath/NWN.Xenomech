using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using XM.Inventory;
using XM.Progression.Event;
using XM.Progression.Job.Entity;
using XM.Progression.Job.JobDefinition;
using XM.Progression.Stat;
using XM.Progression.Stat.Entity;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using ArgumentException = System.ArgumentException;

namespace XM.Progression.Job
{
    [ServiceBinding(typeof(JobService))]
    public class JobService
    {
        public const int JobCount = 8;

        private readonly Dictionary<GradeType, int> _baseHPByGrade = new()
        {
            { GradeType.A, 19},
            { GradeType.B, 17},
            { GradeType.C, 16},
            { GradeType.D, 14},
            { GradeType.E, 13},
            { GradeType.F, 11},
            { GradeType.G, 10},
        };

        private readonly Dictionary<GradeType, int> _growthHPByGrade = new()
        {
            { GradeType.A, 9},
            { GradeType.B, 8},
            { GradeType.C, 7},
            { GradeType.D, 6},
            { GradeType.E, 5},
            { GradeType.F, 4},
            { GradeType.G, 3},
        };

        private readonly Dictionary<GradeType, int> _bonusHPByGrade = new()
        {
            { GradeType.A, 1},
            { GradeType.B, 1},
            { GradeType.C, 1},
            { GradeType.D, 0},
            { GradeType.E, 0},
            { GradeType.F, 0},
            { GradeType.G, 0},
        };

        private readonly Dictionary<GradeType, int> _baseStatsByGrade = new()
        {
            { GradeType.A, 5},
            { GradeType.B, 4},
            { GradeType.C, 4},
            { GradeType.D, 3},
            { GradeType.E, 3},
            { GradeType.F, 2},
            { GradeType.G, 2},
        };

        private readonly Dictionary<GradeType, int> _baseEPByGrade = new()
        {
            { GradeType.A, 16},
            { GradeType.B, 14},
            { GradeType.C, 12},
            { GradeType.D, 10},
            { GradeType.E, 8},
            { GradeType.F, 6},
            { GradeType.G, 4},
        };

        private readonly Dictionary<GradeType, float> _growthEPByGrade = new()
        {
            { GradeType.A, 6f},
            { GradeType.B, 5f},
            { GradeType.C, 4f},
            { GradeType.D, 3f},
            { GradeType.E, 2f},
            { GradeType.F, 1f},
            { GradeType.G, 0.5f},
        };

        private readonly Dictionary<GradeType, float> _growthStatsByGrade = new()
        {
            { GradeType.A , 0.5f},
            { GradeType.B , 0.45f},
            { GradeType.C , 0.40f},
            { GradeType.D , 0.35f},
            { GradeType.E , 0.30f},
            { GradeType.F , 0.25f},
            { GradeType.G , 0.20f},
        };

        private readonly Dictionary<JobType, IJobDefinition> _jobDefinitions = new()
        {
            { JobType.Invalid, new InvalidJobDefinition()},
            { JobType.Beastmaster , new BeastmasterJobDefinition()},
            { JobType.Brawler , new BrawlerJobDefinition()},
            { JobType.Elementalist , new ElementalistJobDefinition()},
            { JobType.Hunter , new HunterJobDefinition()},
            { JobType.Keeper , new KeeperJobDefinition()},
            { JobType.Mender , new MenderJobDefinition()},
            { JobType.Nightstalker , new NightstalkerJobDefinition()},
            { JobType.Techweaver , new TechweaverJobDefinition()},

        };

        private readonly InventoryService _inventory;
        private readonly DBService _db;
        private readonly StatService _stat;
        private readonly XMEventService _event;

        public JobService(
            InventoryService inventory, 
            DBService db,
            StatService stat,
            XMEventService @event)
        {
            _inventory = inventory;
            _db = db;
            _stat = stat;
            _event = @event;
        }

        internal Dictionary<JobType, IJobDefinition> GetAllJobDefinitions()
        {
            return _jobDefinitions.ToDictionary(x => x.Key, y => y.Value);
        }

        internal IJobDefinition GetJobDefinition(JobType job)
        {
            return _jobDefinitions[job];
        }

        public JobType GetActiveJob(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only PCs can have jobs.");

            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            return dbPlayerJob.ActiveJob;
        }

        public int GetJobLevel(uint player, JobType job)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only PCs can have jobs.");

            var playerId = PlayerId.Get(player);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
            var level = dbPlayerJob.JobLevels.ContainsKey(job)
                ? dbPlayerJob.JobLevels[job]
                : 0;
            return level;
        }

        public int GetLevel(uint creature)
        {
            if (GetIsPC(creature))
            {
                var activeJob = GetActiveJob(creature);
                return GetJobLevel(creature, activeJob);
            }
            else
            {
                var npcStats = _stat.GetNPCStats(creature);
                return npcStats.Level;
            }
        }

        internal int CalculateHP(int level, GradeType grade)
        {
            var hpScale = _growthHPByGrade[grade];
            var hpBase = _baseHPByGrade[grade];
            var hpBonus = _bonusHPByGrade[grade];

            return hpScale * (level - 1) + hpBase + hpBonus * level;
        }

        internal int CalculateEP(int level, GradeType grade)
        {
            var epScale = _growthEPByGrade[grade];
            var epBase = _baseEPByGrade[grade];

            return (int)(epScale * (level - 1) + epBase);
        }

        internal int CalculateStat(int level, GradeType grade)
        {
            var statScale = _growthStatsByGrade[grade];
            var statBase = _baseStatsByGrade[grade];

            return (int)(statScale * (level - 1) + statBase);
        }

        public void ChangeJob(uint player, JobType job)
        {
            if (!GetIsPC(player) || GetIsDM(player) || GetIsDMPossessed(player))
                throw new ArgumentException("Only players may change jobs.");

            _inventory.UnequipAllItems(player);

            var definition = GetJobDefinition(job);
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            var dbPlayerJob = _db.Get<PlayerJob>(playerId);
            var level = dbPlayerJob.JobLevels[job];

            var hp = CalculateHP(level, definition.Grades.HP) + dbPlayerStat.HP;
            var ep = CalculateEP(level, definition.Grades.EP) + dbPlayerStat.EP;
            var might = CalculateStat(level, definition.Grades.Might) + 
                        dbPlayerStat.BaseAttributes[AbilityType.Might] + 
                        dbPlayerStat.Attributes[AbilityType.Might];

            var perception = CalculateStat(level, definition.Grades.Perception) + 
                             dbPlayerStat.BaseAttributes[AbilityType.Perception] + 
                             dbPlayerStat.Attributes[AbilityType.Perception];

            var vitality = CalculateStat(level, definition.Grades.Vitality) + 
                           dbPlayerStat.BaseAttributes[AbilityType.Vitality] + 
                           dbPlayerStat.Attributes[AbilityType.Vitality];

            var agility = CalculateStat(level, definition.Grades.Agility) + 
                          dbPlayerStat.BaseAttributes[AbilityType.Agility] + 
                          dbPlayerStat.Attributes[AbilityType.Agility];

            var willpower = CalculateStat(level, definition.Grades.Willpower) + 
                            dbPlayerStat.BaseAttributes[AbilityType.Willpower] + 
                            dbPlayerStat.Attributes[AbilityType.Willpower];

            var social = CalculateStat(level, definition.Grades.Social) + 
                         dbPlayerStat.BaseAttributes[AbilityType.Social] + 
                         dbPlayerStat.Attributes[AbilityType.Social];

            _stat.SetPlayerMaxHP(player, hp);
            _stat.SetPlayerMaxEP(player, ep);
            _stat.SetPlayerAttribute(player, AbilityType.Might, might);
            _stat.SetPlayerAttribute(player, AbilityType.Perception, perception);
            _stat.SetPlayerAttribute(player, AbilityType.Vitality, vitality);
            _stat.SetPlayerAttribute(player, AbilityType.Agility, agility);
            _stat.SetPlayerAttribute(player, AbilityType.Willpower, willpower);
            _stat.SetPlayerAttribute(player, AbilityType.Social, social);

            var name = Locale.GetString(definition.Name);
            SendMessageToPC(player, $"Job changed to: {ColorToken.Green(name)}");

            _event.ExecuteScript(ProgressionEventScript.PlayerChangedJobScript, player);
        }
    }
}
