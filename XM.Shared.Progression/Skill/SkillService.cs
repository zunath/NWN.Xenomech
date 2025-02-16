using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Progression.Job;
using XM.Progression.Job.Entity;
using XM.Progression.Job.JobDefinition;
using XM.Progression.Skill.SkillDefinition;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill
{
    [ServiceBinding(typeof(SkillService))]
    [ServiceBinding(typeof(IInitializable))]
    public class SkillService: IInitializable
    {
        private readonly DBService _db;
        private readonly StatService _stat;
        private readonly JobService _job;
        private readonly XMEventService _event;

        private readonly IList<ISkillDefinition> _skillDefinitions;
        private readonly Dictionary<SkillType, ISkillDefinition> _skills = new();
        private readonly Dictionary<BaseItemType, SkillType> _weaponToSkills = new();
        private readonly SkillGrades _skillGrades = new();

        public SkillService(
            DBService db,
            XMEventService @event,
            StatService stat,
            JobService job,
            IList<ISkillDefinition> skillDefinitions)
        {
            _db = db;
            _stat = stat;
            _job = job;
            _event = @event;
            _skillDefinitions = skillDefinitions;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnItemHit>(ProcessSkillGain);
        }

        private void ProcessSkillGain(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var target = GetSpellTargetObject();

            if (GetIsPC(target) || GetIsDM(target))
                return;

            var item = GetSpellCastItem();
            var itemType = GetBaseItemType(item);
            if (!_weaponToSkills.ContainsKey(itemType))
                return;

            var playerLevel = _stat.GetLevel(player);
            var targetLevel = _stat.GetLevel(target);
            var job = _job.GetActiveJob(player);
            var skillType = _weaponToSkills[itemType];
            var skill = _skills[skillType];
            var grade = GetGrade(player, job, skill);
            if (grade == GradeType.Invalid)
                return;

            var playerSkillCap = _skillGrades.GetSkillCap(grade, playerLevel);
            var targetSkillCap = GetSkillCap(grade, targetLevel);

            if (!CanGainSkill(player, skillType, playerSkillCap, targetSkillCap))
                return;

            var increaseChance = 100 - (playerLevel * 2);
            if (increaseChance < 1)
                increaseChance = 1;

            var roll = XMRandom.Next(100);
            if (roll <= increaseChance)
            {
                LevelUpSkill(player, skillType);
            }
        }

        private void LevelUpSkill(uint player, SkillType skillType)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerSkill = _db.Get<PlayerSkill>(playerId);
            if (!dbPlayerSkill.Skills.ContainsKey(skillType))
                dbPlayerSkill.Skills[skillType] = 0;
            const int IncreaseBy = 1;

            var newLevel = dbPlayerSkill.Skills[skillType] += IncreaseBy;
            dbPlayerSkill.Skills[skillType] = newLevel;

            _db.Set(dbPlayerSkill);

            var definition = _skills[skillType];
            SendMessageToPC(player, LocaleString.YourXSkillIncreasesToY.ToLocalizedString(definition.Name.ToLocalizedString(), dbPlayerSkill.Skills[skillType]));

            if (definition.WeaponSkillAcquisitionLevels.ContainsKey(newLevel))
            {
                var feat = definition.WeaponSkillAcquisitionLevels[newLevel];
                var featNameId = Convert.ToInt32(Get2DAString("feat", "FEAT", (int)feat));
                var featName = GetStringByStrRef(featNameId);
                CreaturePlugin.AddFeatByLevel(player, feat, 1);
                var message = LocaleString.YouLearnedXWeaponSkillY.ToLocalizedString(definition.Name.ToLocalizedString(), featName);
                SendMessageToPC(player, message);
            }
        }

        public GradeType GetGrade(
            uint player, 
            IJobDefinition job,
            ISkillDefinition skill)
        {
            if (GetHasFeat(skill.LoreFeat, player))
                return GradeType.A;

            if (!job.Grades.SkillGrades.ContainsKey(skill.Type))
                return GradeType.Invalid;

            return job.Grades.SkillGrades[skill.Type];
        }

        public int GetSkillCap(
            GradeType grade,
            int jobLevel)
        {
            return _skillGrades.GetSkillCap(grade, jobLevel);
        }

        private bool CanGainSkill(
            uint player, 
            SkillType skillType, 
            int playerSkillCap,
            int targetSkillCap)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerSkill = _db.Get<PlayerSkill>(playerId);
            if (!dbPlayerSkill.Skills.ContainsKey(skillType))
                dbPlayerSkill.Skills[skillType] = 0;

            // Player is at the cap for their current level.
            var currentSkillLevel = dbPlayerSkill.Skills[skillType];
            if (currentSkillLevel >= playerSkillCap)
                return false;

            // Target level is too low for player to gain skills.
            if (targetSkillCap <= currentSkillLevel)
                return false;

            return true;
        }

        public List<ISkillDefinition> GetAllSkillDefinitions()
        {
            return _skillDefinitions.ToList();
        }

        public ISkillDefinition GetSkillDefinition(SkillType skillType)
        {
            return _skills[skillType];
        }

        public SkillType GetSkillOfWeapon(uint weapon)
        {
            var baseItemType = GetBaseItemType(weapon);
            return _weaponToSkills.ContainsKey(baseItemType)
                ? _weaponToSkills[baseItemType]
                : SkillType.Invalid;
        }

        public int GetSkillLevel(uint player, SkillType skillType)
        {
            if (!GetIsPC(player) || GetIsDM(player))
                return 0;

            var playerId = PlayerId.Get(player);
            var dbPlayerSkill = _db.Get<PlayerSkill>(playerId);

            if (!dbPlayerSkill.Skills.ContainsKey(skillType))
                dbPlayerSkill.Skills[skillType] = 0;

            return dbPlayerSkill.Skills[skillType];
        }

        public int GetEvasionSkill(uint creature)
        {
            if (GetIsPC(creature))
            {
                var job = _job.GetActiveJob(creature);
                var level = _stat.GetLevel(creature);
                var grade = job.Grades.Evasion;
                return _skillGrades.GetSkillCap(grade, level);
            }
            else
            {
                var npcStats = _stat.GetNPCStats(creature);
                var grade = npcStats.EvasionGrade;
                return _skillGrades.GetSkillCap(grade, npcStats.Level);
            }
        }

        public void Init()
        {
            LoadSkillDefinitions();
        }

        private void LoadSkillDefinitions()
        {
            foreach (var definition in _skillDefinitions)
            {
                _skills[definition.Type] = definition;

                foreach (var itemType in definition.BaseItems)
                {
                    _weaponToSkills[itemType] = definition.Type;
                }
            }
        }
    }
}
