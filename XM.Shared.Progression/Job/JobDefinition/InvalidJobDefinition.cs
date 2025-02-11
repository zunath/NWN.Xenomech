using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using SkillType = XM.Progression.Skill.SkillType;

namespace XM.Progression.Job.JobDefinition
{
    internal class InvalidJobDefinition : JobDefinitionBase
    {
        public override JobType Type => JobType.Invalid;
        public override bool IsVisibleToPlayers => false;

        public override LocaleString Name => LocaleString.Empty;

        public override string IconResref => string.Empty;

        public override JobGrade Grades { get; } = new()
        {
            MaxHP = GradeType.G,
            MaxEP = GradeType.G,
            Might = GradeType.G,
            Perception = GradeType.G,
            Vitality = GradeType.G,
            Agility = GradeType.G,
            Willpower = GradeType.G,
            Social = GradeType.G,

            Evasion = GradeType.G,

            SkillGrades = new Dictionary<SkillType, GradeType>
            {
            }
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
