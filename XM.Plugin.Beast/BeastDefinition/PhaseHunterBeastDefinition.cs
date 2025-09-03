using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class PhaseHunterBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.PhaseHunter;
        public int LevelRequired => 26;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 20;
        public int SoundSetId => 18;
        public LocaleString Name => LocaleString.PhaseHunter;
        public int AttackDelay => 220;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.C,
            Might = GradeType.C,
            Perception = GradeType.A,
            Vitality = GradeType.C,
            Agility = GradeType.A,
            Willpower = GradeType.C,
            Social = GradeType.F,

            DMG = GradeType.C,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Phase Leap ability when available
            // TODO: Add Dimension Cloak ability when available
            // TODO: Add Vanish ability when available
            // TODO: Add Quantum Strike ability when available
        };
    }
}