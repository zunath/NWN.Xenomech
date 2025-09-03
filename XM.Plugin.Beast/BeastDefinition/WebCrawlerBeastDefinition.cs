using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class WebCrawlerBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.WebCrawler;
        public int LevelRequired => 7;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 36;
        public int SoundSetId => 36;
        public LocaleString Name => LocaleString.WebCrawler;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.E,
            Might = GradeType.F,
            Perception = GradeType.D,
            Vitality = GradeType.F,
            Agility = GradeType.D,
            Willpower = GradeType.E,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.D
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Virus Bite ability when available
            // TODO: Add Net Trap ability when available
        };
    }
}