using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class StellarHoundBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.StellarHound;
        public int LevelRequired => 31;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 27;
        public int SoundSetId => 27;
        public LocaleString Name => LocaleString.StellarHound;
        public int AttackDelay => 260;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.A,
            Might = GradeType.C,
            Perception = GradeType.A,
            Vitality = GradeType.C,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.E,

            DMG = GradeType.C,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Cosmic Bite ability when available
            // TODO: Add Void Howl ability when available
            // TODO: Add Nebula Beam ability when available
            // TODO: Add Solar Flare ability when available
        };
    }
}