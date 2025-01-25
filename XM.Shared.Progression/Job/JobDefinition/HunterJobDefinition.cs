using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class HunterJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Hunter;

        public override string IconResref => "hunter_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.E,
            EP = GradeType.G,
            Might = GradeType.E,
            Perception = GradeType.D,
            Vitality = GradeType.D,
            Agility = GradeType.A,
            Willpower = GradeType.D,
            Social = GradeType.E
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {
            {2, FeatType.CryoVenom1},
            {4, FeatType.FreezingShot1},
            {8, FeatType.Volley1},
            {10, FeatType.ChokingShot1},
            {12, FeatType.AccuracyBonus1},
            {14, FeatType.FreezingShot2},
            {18, FeatType.Cripple1},
            {20, FeatType.Recycle1},
            {22, FeatType.ChokingShot2},
            {24, FeatType.Volley2},
            {25, FeatType.IceProtection},
            {26, FeatType.CryoVenom2},
            {30, FeatType.Sharpshot},
            {32, FeatType.AccuracyBonus2},
            {34, FeatType.ReflexBonus},
            {36, FeatType.Recycle2},
            {40, FeatType.Cripple2},
            {42, FeatType.Volley3},
            {44, FeatType.FreezingShot3},
            {48, FeatType.Barrage},
            {50, FeatType.EagleEyeShot},
        };
    }
}
