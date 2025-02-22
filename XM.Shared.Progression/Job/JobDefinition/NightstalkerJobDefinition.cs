using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Job.JobDefinition
{
    internal class NightstalkerJobDefinition: JobDefinitionBase
    {
        public override JobType Type => JobType.Nightstalker;
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Nightstalker;

        public override string IconResref => "night_icon";

        public override StatGrade Grades { get; } = new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.G,
            Might = GradeType.D,
            Perception = GradeType.A,
            Vitality = GradeType.D,
            Agility = GradeType.B,
            Willpower = GradeType.G,
            Social = GradeType.G,

            Evasion = GradeType.A,

            SkillGrades = new Dictionary<SkillType, GradeType>
            {
                { SkillType.Dagger, GradeType.A},
                { SkillType.ShortSword, GradeType.B},
                { SkillType.Pistol, GradeType.C},
            }
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.BackAttack1},
            {4, FeatType.Steal},
            {8, FeatType.Creditfinder},
            {10, FeatType.SneakAttack1},
            {12, FeatType.SubtleBlow1},
            {14, FeatType.TreasureHunter1},
            {16, FeatType.DaggerLore},
            {18, FeatType.BackAttack2},
            {22, FeatType.SneakAttack2},
            {24, FeatType.CriticalBonus1},
            {25, FeatType.LightWard},
            {26, FeatType.Flee},
            {30, FeatType.TreasureHunter2},
            {32, FeatType.BackAttack3},
            {34, FeatType.StasisField},
            {36, FeatType.SneakAttack3},
            {40, FeatType.SubtleBlow2},
            {42, FeatType.CriticalBonus2},
            {44, FeatType.Hide},
            {48, FeatType.BackAttack4},
            {50, FeatType.PerfectDodge},
        };
    }
}
