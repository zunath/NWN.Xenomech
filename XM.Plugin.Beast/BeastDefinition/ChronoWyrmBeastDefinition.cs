using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ChronoWyrmBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ChronoWyrm;
        public int LevelRequired => 49;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 30;
        public int SoundSetId => 30;
        public LocaleString Name => LocaleString.ChronoWyrm;
        public int AttackDelay => 260;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.A,
            Vitality = GradeType.A,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.B,

            DMG = GradeType.A,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Time Stop ability when available
            // TODO: Add Temporal Strike ability when available
            // TODO: Add Causality Loop ability when available
            // TODO: Add Reality Reset ability when available
        };
    }
}