using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class TitanFrameBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.TitanFrame;
        public int LevelRequired => 16;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.TitanFrame;
        public int AttackDelay => 400;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.B,
            MaxEP = GradeType.E,
            Might = GradeType.A,
            Perception = GradeType.F,
            Vitality = GradeType.A,
            Agility = GradeType.G,
            Willpower = GradeType.E,
            Social = GradeType.G,

            DMG = GradeType.B,
            Evasion = GradeType.G
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Armor Plating ability when available
            // TODO: Add Heavy Strike ability when available
        };
    }
}