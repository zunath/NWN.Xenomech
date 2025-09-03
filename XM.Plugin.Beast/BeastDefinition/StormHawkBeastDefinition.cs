using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class StormHawkBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.StormHawk;
        public int LevelRequired => 17;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 42;
        public int SoundSetId => 42;
        public LocaleString Name => LocaleString.StormHawk;
        public int AttackDelay => 220;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.C,
            Might = GradeType.E,
            Perception = GradeType.A,
            Vitality = GradeType.E,
            Agility = GradeType.A,
            Willpower = GradeType.C,
            Social = GradeType.F,

            DMG = GradeType.E,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Lightning Dive ability when available
            // TODO: Add Shock Field ability when available
        };
    }
}