using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class WraithEngineBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.WraithEngine;
        public int LevelRequired => 45;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 32;
        public int SoundSetId => 32;
        public LocaleString Name => LocaleString.WraithEngine;
        public int AttackDelay => 320;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.A,
            Vitality = GradeType.A,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.C,

            DMG = GradeType.A,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Spectral Bite ability when available
            // TODO: Add Phase Coil ability when available
            // TODO: Add Ghost Mode ability when available
            // TODO: Add Soul Virus ability when available
        };
    }
}