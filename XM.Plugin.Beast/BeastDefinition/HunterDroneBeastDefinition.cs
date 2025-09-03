using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class HunterDroneBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.HunterDrone;
        public int LevelRequired => 8;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 42;
        public int SoundSetId => 42;
        public LocaleString Name => LocaleString.HunterDrone;
        public int AttackDelay => 240;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.D,
            Might = GradeType.F,
            Perception = GradeType.B,
            Vitality = GradeType.F,
            Agility = GradeType.A,
            Willpower = GradeType.D,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Lock-On Strike ability when available
        };
    }
}