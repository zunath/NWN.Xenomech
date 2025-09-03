using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class BioTankBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.BioTank;
        public int LevelRequired => 10;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 25;
        public int SoundSetId => 25;
        public LocaleString Name => LocaleString.BioTank;
        public int AttackDelay => 360;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.D,
            Might = GradeType.E,
            Perception = GradeType.F,
            Vitality = GradeType.E,
            Agility = GradeType.F,
            Willpower = GradeType.D,
            Social = GradeType.F,

            DMG = GradeType.E,
            Evasion = GradeType.F
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Bio Cannon ability when available
            // TODO: Add Acid Spit ability when available
        };
    }
}