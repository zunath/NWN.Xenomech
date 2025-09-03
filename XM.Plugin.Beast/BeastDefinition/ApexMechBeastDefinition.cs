using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ApexMechBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ApexMech;
        public int LevelRequired => 30;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.ApexMech;
        public int AttackDelay => 450;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.C,
            Might = GradeType.A,
            Perception = GradeType.D,
            Vitality = GradeType.A,
            Agility = GradeType.E,
            Willpower = GradeType.C,
            Social = GradeType.F,

            DMG = GradeType.B,
            Evasion = GradeType.E
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Berserker Mode ability when available
            // TODO: Add Shock Wave ability when available
            // TODO: Add Terror Protocol ability when available
            // TODO: Add Fortify Systems ability when available
        };
    }
}