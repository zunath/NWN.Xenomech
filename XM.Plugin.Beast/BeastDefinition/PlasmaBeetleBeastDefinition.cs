using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class PlasmaBeetleBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.PlasmaBeetle;
        public int LevelRequired => 12;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 9;
        public int SoundSetId => 9;
        public LocaleString Name => LocaleString.PlasmaBeetle;
        public int AttackDelay => 320;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.D,
            Might = GradeType.F,
            Perception = GradeType.E,
            Vitality = GradeType.F,
            Agility = GradeType.E,
            Willpower = GradeType.D,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.E
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Plasma Burst ability when available
        };
    }
}