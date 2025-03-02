using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Job.JobDefinition
{
    internal class BeastmasterJobDefinition: JobDefinitionBase
    {
        public override JobType Type => JobType.Beastmaster;
        public override ClassType NWNClass => ClassType.Beastmaster;
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Beastmaster;

        public override string IconResref => "beast_icon";

        public override StatGrade Grades { get; } = new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.G,
            Might = GradeType.D,
            Perception = GradeType.C,
            Vitality = GradeType.D,
            Agility = GradeType.F,
            Willpower = GradeType.E,
            Social = GradeType.A,

            Evasion = GradeType.C,

            SkillGrades = new Dictionary<SkillType, GradeType>
            {
                { SkillType.Axe, GradeType.A},
                { SkillType.GreatAxe, GradeType.B},
                { SkillType.Bow, GradeType.C},
            }
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {1, FeatType.CallBeast},
            {4, FeatType.Reward1},
            {8, FeatType.CrescentMoon1},
            {10, FeatType.Sic},
            {12, FeatType.MercyStrike1},
            {14, FeatType.Snarl},
            {16, FeatType.AxeLore},
            {18, FeatType.ResistPoison},
            {20, FeatType.Reward2},
            {22, FeatType.ThirdEye},
            {24, FeatType.CrescentMoon2},
            {25, FeatType.EarthWard},
            {26, FeatType.FeralHowl},
            {30, FeatType.Quickness},
            {32, FeatType.MercyStrike2},
            {34, FeatType.Assault},
            {36, FeatType.KillerInstinct},
            {40, FeatType.CrescentMoon3},
            {42, FeatType.Reward3},
            {44, FeatType.BeastSpeed},
            {48, FeatType.EtherLink},
            {50, FeatType.Familiar},
        };
    }
}
