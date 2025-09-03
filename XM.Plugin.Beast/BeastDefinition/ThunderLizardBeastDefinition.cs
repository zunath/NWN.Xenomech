using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ThunderLizardBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ThunderLizard;
        public int LevelRequired => 22;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 28;
        public int SoundSetId => 28;
        public LocaleString Name => LocaleString.ThunderLizard;
        public int AttackDelay => 340;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.C,
            Might = GradeType.D,
            Perception = GradeType.C,
            Vitality = GradeType.D,
            Agility = GradeType.D,
            Willpower = GradeType.C,
            Social = GradeType.F,

            DMG = GradeType.D,
            Evasion = GradeType.D
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Electric Roar ability when available
            // TODO: Add Chain Lightning ability when available
        };
    }
}