using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Job.JobDefinition
{
    internal class BrawlerJobDefinition: JobDefinitionBase
    {
        public override JobType Type => JobType.Brawler;
        public override ClassType NWNClass => ClassType.Brawler;
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Brawler;

        public override string IconResref => "brawler_icon";

        public override StatGrade Grades { get; } = new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.G,
            Might = GradeType.C,
            Perception = GradeType.B,
            Vitality = GradeType.A,
            Agility = GradeType.F,
            Willpower = GradeType.D,
            Social = GradeType.E,

            Evasion = GradeType.B,

            SkillGrades = new Dictionary<SkillType, GradeType>
            {
                { SkillType.HandToHand, GradeType.A},
                { SkillType.Staff, GradeType.B},
                { SkillType.Club, GradeType.C},
            }
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.ElectricFist1},
            {4, FeatType.StrikingCobra1},
            {8, FeatType.Flash},
            {10, FeatType.Parry1},
            {12, FeatType.AttackBonus1},
            {14, FeatType.PowerAttack1},
            {16, FeatType.HandToHandLore},
            {18, FeatType.Shadowstrike1},
            {20, FeatType.ElectricFist2},
            {22, FeatType.Warcry},
            {24, FeatType.Chi1},
            {25, FeatType.WindWard},
            {26, FeatType.Parry2},
            {30, FeatType.StrikingCobra2},
            {32, FeatType.AttackBonus2},
            {34, FeatType.PowerAttack2},
            {36, FeatType.ElectricFist3},
            {40, FeatType.Chi2},
            {42, FeatType.Shadowstrike2},
            {44, FeatType.Parry3},
            {48, FeatType.MartialArts},
            {50, FeatType.HundredFists},
        };
    }
}
