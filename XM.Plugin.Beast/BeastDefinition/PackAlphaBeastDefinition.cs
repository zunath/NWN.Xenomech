using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class PackAlphaBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.PackAlpha;
        public int LevelRequired => 9;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 1;
        public int SoundSetId => 1;
        public LocaleString Name => LocaleString.PackAlpha;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.E,
            Might = GradeType.D,
            Perception = GradeType.C,
            Vitality = GradeType.D,
            Agility = GradeType.C,
            Willpower = GradeType.E,
            Social = GradeType.F,

            DMG = GradeType.D,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Pack Sync ability when available
            // TODO: Add Alert Signal ability when available
        };
    }
}