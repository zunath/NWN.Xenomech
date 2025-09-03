using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ArchangelUnitBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ArchangelUnit;
        public int LevelRequired => 42;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 38;
        public int SoundSetId => 38;
        public LocaleString Name => LocaleString.ArchangelUnit;
        public int AttackDelay => 220;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.B,
            Perception = GradeType.A,
            Vitality = GradeType.B,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.B,

            DMG = GradeType.B,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Judgment Strike ability when available
            // TODO: Add Sacred Protocol ability when available
            // TODO: Add Light Spear ability when available
            // TODO: Add Divine Override ability when available
        };
    }
}