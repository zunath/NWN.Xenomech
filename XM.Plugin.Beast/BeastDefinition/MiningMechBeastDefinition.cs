using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class MiningMechBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.MiningMech;
        public int LevelRequired => 11;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.MiningMech;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.F,
            Might = GradeType.E,
            Perception = GradeType.E,
            Vitality = GradeType.E,
            Agility = GradeType.C,
            Willpower = GradeType.F,
            Social = GradeType.E,

            DMG = GradeType.E,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Drill Rush ability when available
            // TODO: Add Stabilizers ability when available
        };
    }
}