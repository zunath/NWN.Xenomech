using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class WindRiderBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.WindRider;
        public int LevelRequired => 24;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 42;
        public int SoundSetId => 42;
        public LocaleString Name => LocaleString.WindRider;
        public int AttackDelay => 200;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.B,
            Might = GradeType.E,
            Perception = GradeType.A,
            Vitality = GradeType.E,
            Agility = GradeType.A,
            Willpower = GradeType.B,
            Social = GradeType.E,

            DMG = GradeType.E,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Wind Blade ability when available
            // TODO: Add Cyclone ability when available
            // TODO: Add Flight Mode ability when available
        };
    }
}