using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class LeviathanMechBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.LeviathanMech;
        public int LevelRequired => 40;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 35;
        public int SoundSetId => 35;
        public LocaleString Name => LocaleString.LeviathanMech;
        public int AttackDelay => 400;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.B,
            Perception = GradeType.C,
            Vitality = GradeType.A,
            Agility = GradeType.D,
            Willpower = GradeType.A,
            Social = GradeType.E,

            DMG = GradeType.B,
            Evasion = GradeType.D
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Tsunami Cannon ability when available
            // TODO: Add Whirlpool Generator ability when available
            // TODO: Add Pressure Wave ability when available
            // TODO: Add Depth Charge ability when available
        };
    }
}