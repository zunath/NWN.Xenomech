using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Job.JobDefinition
{
    internal class KeeperJobDefinition: JobDefinitionBase
    {
        public override JobType Type => JobType.Keeper;
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Keeper;

        public override string IconResref => "keeper_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.C,
            EP = GradeType.F,
            Might = GradeType.B,
            Perception = GradeType.E,
            Vitality = GradeType.A,
            Agility = GradeType.G,
            Willpower = GradeType.C,
            Social = GradeType.C,

            Evasion = GradeType.C,

            SkillGrades = new Dictionary<SkillType, GradeType>
            {
                { SkillType.Longsword, GradeType.A},
                { SkillType.GreatSword, GradeType.B},
                { SkillType.Pistol, GradeType.C},
            }
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.Provoke1},
            {4, FeatType.DefenseBonus1},
            {8, FeatType.Restoration1},
            {10, FeatType.ShieldMastery1},
            {12, FeatType.Aggressor1},
            {14, FeatType.DefenseBonus2},
            {18, FeatType.Provoke2},
            {20, FeatType.RadiantBlast1},
            {22, FeatType.Restoration2},
            {24, FeatType.HPBonus1},
            {25, FeatType.DarknessProtection},
            {26, FeatType.Rampart},
            {30, FeatType.DefenseBonus3},
            {32, FeatType.Raise},
            {34, FeatType.ShieldMastery2},
            {36, FeatType.Aggressor2},
            {40, FeatType.RadiantBlast2},
            {42, FeatType.Restoration3},
            {44, FeatType.HPBonus2},
            {48, FeatType.DefenseBonus4},
            {50, FeatType.MightyStrikes},
        };
    }
}
