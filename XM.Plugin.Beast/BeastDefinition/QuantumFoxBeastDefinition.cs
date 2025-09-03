using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class QuantumFoxBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.QuantumFox;
        public int LevelRequired => 39;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 20;
        public int SoundSetId => 15;
        public LocaleString Name => LocaleString.QuantumFox;
        public int AttackDelay => 240;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.A,
            Might = GradeType.C,
            Perception = GradeType.A,
            Vitality = GradeType.C,
            Agility = GradeType.A,
            Willpower = GradeType.A,
            Social = GradeType.B,

            DMG = GradeType.C,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Quantum Fire ability when available
            // TODO: Add Time Dilation ability when available
            // TODO: Add Probability Shift ability when available
            // TODO: Add Reality Anchor ability when available
        };
    }
}