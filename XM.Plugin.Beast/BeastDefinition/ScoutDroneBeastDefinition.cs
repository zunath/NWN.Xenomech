using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ScoutDroneBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ScoutDrone;
        public int LevelRequired => 1;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 144;
        public int SoundSetId => 4;
        public LocaleString Name => LocaleString.ScoutDrone;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.F,
            Might = GradeType.E,
            Perception = GradeType.D,
            Vitality = GradeType.E,
            Agility = GradeType.D,
            Willpower = GradeType.F,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.D
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Scan ability when available
        };
    }
}
