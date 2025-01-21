using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class MenderJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Mender;

        public override string IconResref => "mender_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.E,
            EP = GradeType.C,
            Might = GradeType.D,
            Perception = GradeType.F,
            Vitality = GradeType.D,
            Agility = GradeType.E,
            Willpower = GradeType.A,
            Social = GradeType.C
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.EtherBloom1},
            {4, FeatType.Antidote},
            {8, FeatType.Protection1},
            {10, FeatType.DivineSeal},
            {12, FeatType.EtherBloom2},
            {14, FeatType.AutoRegen1},
            {18, FeatType.EtherBlast1},
            {20, FeatType.Regen1},
            {22, FeatType.Revive},
            {24, FeatType.EtherBloom3},
            {25, FeatType.FireProtection},
            {26, FeatType.Protection2},
            {30, FeatType.Tsu},
            {32, FeatType.EtherBlast2},
            {34, FeatType.Erase},
            {36, FeatType.AutoRegen2},
            {40, FeatType.Haste},
            {42, FeatType.EtherBloom4},
            {44, FeatType.Regen2},
            {48, FeatType.Protection3},
            {50, FeatType.Benediction},
        };
    }
}
