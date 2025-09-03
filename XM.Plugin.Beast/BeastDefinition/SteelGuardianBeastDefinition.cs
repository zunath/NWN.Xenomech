using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class SteelGuardianBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.SteelGuardian;
        public int LevelRequired => 20;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 1;
        public int SoundSetId => 1;
        public LocaleString Name => LocaleString.SteelGuardian;
        public int AttackDelay => 320;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.E,
            Might = GradeType.B,
            Perception = GradeType.C,
            Vitality = GradeType.B,
            Agility = GradeType.C,
            Willpower = GradeType.E,
            Social = GradeType.F,

            DMG = GradeType.C,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Metal Fang ability when available
            // TODO: Add Sonic Bark ability when available
        };
    }
}