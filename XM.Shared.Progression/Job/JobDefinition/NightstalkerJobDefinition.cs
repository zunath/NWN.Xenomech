using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class NightstalkerJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Nightstalker;

        public override string IconResref => "night_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.D,
            EP = GradeType.G,
            Might = GradeType.D,
            Perception = GradeType.A,
            Vitality = GradeType.D,
            Agility = GradeType.B,
            Willpower = GradeType.G,
            Social = GradeType.G
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.BackAttack1},
            {4, FeatType.Steal},
            {8, FeatType.Creditfinder},
            {10, FeatType.SneakAttack1},
            {12, FeatType.SubtleBlow1},
            {14, FeatType.TreasureHunter1},
            {18, FeatType.BackAttack2},
            {20, FeatType.VenomStab},
            {22, FeatType.SneakAttack2},
            {24, FeatType.CriticalBonus1},
            {25, FeatType.LightProtection},
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
