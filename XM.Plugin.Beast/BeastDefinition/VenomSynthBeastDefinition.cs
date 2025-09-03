using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class VenomSynthBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.VenomSynth;
        public int LevelRequired => 19;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 32;
        public int SoundSetId => 32;
        public LocaleString Name => LocaleString.VenomSynth;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.D,
            Might = GradeType.F,
            Perception = GradeType.C,
            Vitality = GradeType.F,
            Agility = GradeType.B,
            Willpower = GradeType.D,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Poison Dart ability when available
            // TODO: Add Constrict ability when available
            // TODO: Add Toxin Cloud ability when available
        };
    }
}