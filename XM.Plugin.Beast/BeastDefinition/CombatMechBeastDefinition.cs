using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class CombatMechBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.CombatMech;
        public int LevelRequired => 4;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.CombatMech;
        public int AttackDelay => 340;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.F,
            Might = GradeType.D,
            Perception = GradeType.E,
            Vitality = GradeType.D,
            Agility = GradeType.E,
            Willpower = GradeType.F,
            Social = GradeType.F,

            DMG = GradeType.E,
            Evasion = GradeType.E
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Ramming Strike ability when available
        };
    }
}