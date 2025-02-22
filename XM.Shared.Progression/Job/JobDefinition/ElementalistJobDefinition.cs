using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Job.JobDefinition
{
    internal class ElementalistJobDefinition: JobDefinitionBase
    {
        public override JobType Type => JobType.Elementalist;
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Elementalist;

        public override string IconResref => "elem_icon";

        public override StatGrade Grades { get; } = new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.B,
            Might = GradeType.F,
            Perception = GradeType.C,
            Vitality = GradeType.F,
            Agility = GradeType.C,
            Willpower = GradeType.A,
            Social = GradeType.D,

            Evasion = GradeType.E,

            SkillGrades = new Dictionary<SkillType, GradeType>
            {
                { SkillType.Staff, GradeType.A},
                { SkillType.Club, GradeType.B},
                { SkillType.Throwing, GradeType.C},
            }
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.Flame1},
            {4, FeatType.Neurotoxin},
            {8, FeatType.ClearMind},
            {10, FeatType.Drown1},
            {12, FeatType.EtherAttackBonus1},
            {14, FeatType.ShockwaveSurge1},
            {16, FeatType.StaffLore},
            {18, FeatType.ShockingCircle},
            {20, FeatType.Flame2},
            {22, FeatType.AbyssalVeil1},
            {24, FeatType.ElementalSeal},
            {25, FeatType.LightningWard},
            {26, FeatType.Escape},
            {30, FeatType.ZephyrShroud},
            {32, FeatType.EtherAttackBonus2},
            {34, FeatType.Drown2},
            {36, FeatType.EtherWall},
            {40, FeatType.ShockwaveSurge2},
            {42, FeatType.AbyssalVeil2},
            {44, FeatType.Flame3},
            {48, FeatType.GravitonField},
            {50, FeatType.Manafont},
        };
    }
}
