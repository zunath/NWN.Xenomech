using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class TechweaverJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Techweaver;

        public override string IconResref => "techweaver_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.D,
            EP = GradeType.D,
            Might = GradeType.D,
            Perception = GradeType.D,
            Vitality = GradeType.E,
            Agility = GradeType.E,
            Willpower = GradeType.C,
            Social = GradeType.D
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.NeuralCascade1},
            {4, FeatType.NullLance1},
            {8, FeatType.SynapticShock},
            {10, FeatType.Rupture},
            {12, FeatType.CerebralSpike1},
            {14, FeatType.AutoRefresh1},
            {18, FeatType.EPBonus1},
            {20, FeatType.AurionInfusion1},
            {22, FeatType.NullLance2},
            {24, FeatType.Mindstream1},
            {25, FeatType.MindProtection},
            {26, FeatType.NeuralStasis},
            {30, FeatType.NeuralCascade2},
            {32, FeatType.EPBonus2},
            {34, FeatType.CerebralSpike2},
            {36, FeatType.AutoRefresh2},
            {40, FeatType.AurionInfusion2},
            {42, FeatType.Convert},
            {44, FeatType.NullLance3},
            {48, FeatType.Mindstream2},
            {50, FeatType.Etherwarp},
        };
    }
}
