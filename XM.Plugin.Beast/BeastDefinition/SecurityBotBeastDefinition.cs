using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class SecurityBotBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.SecurityBot;
        public int LevelRequired => 6;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.SecurityBot;
        public int AttackDelay => 380;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.E,
            Might = GradeType.C,
            Perception = GradeType.E,
            Vitality = GradeType.C,
            Agility = GradeType.F,
            Willpower = GradeType.F,
            Social = GradeType.F,

            DMG = GradeType.D,
            Evasion = GradeType.F
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Guard Protocol ability when available
        };
    }
}