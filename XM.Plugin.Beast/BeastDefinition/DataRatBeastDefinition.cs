using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class DataRatBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.DataRat;
        public int LevelRequired => 2;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 15;
        public int SoundSetId => 15;
        public LocaleString Name => LocaleString.DataRat;
        public int AttackDelay => 320;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.F,
            Might = GradeType.F,
            Perception = GradeType.E,
            Vitality = GradeType.F,
            Agility = GradeType.E,
            Willpower = GradeType.F,
            Social = GradeType.F,

            DMG = GradeType.G,
            Evasion = GradeType.E
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Data Mine ability when available
        };
    }
}