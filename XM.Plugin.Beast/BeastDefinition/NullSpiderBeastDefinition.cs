using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class NullSpiderBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.NullSpider;
        public int LevelRequired => 32;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 36;
        public int SoundSetId => 36;
        public LocaleString Name => LocaleString.NullSpider;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.A,
            Might = GradeType.D,
            Perception = GradeType.B,
            Vitality = GradeType.D,
            Agility = GradeType.C,
            Willpower = GradeType.A,
            Social = GradeType.F,

            DMG = GradeType.D,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Void Bite ability when available
            // TODO: Add Reality Tear ability when available
            // TODO: Add Null Web ability when available
        };
    }
}