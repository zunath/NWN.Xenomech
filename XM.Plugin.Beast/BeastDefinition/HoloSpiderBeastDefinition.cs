using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class HoloSpiderBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.HoloSpider;
        public int LevelRequired => 23;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 36;
        public int SoundSetId => 36;
        public LocaleString Name => LocaleString.HoloSpider;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.B,
            Might = GradeType.F,
            Perception = GradeType.D,
            Vitality = GradeType.F,
            Agility = GradeType.D,
            Willpower = GradeType.B,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.D
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Light Beam ability when available
            // TODO: Add Hard Light Web ability when available
            // TODO: Add Refraction ability when available
        };
    }
}