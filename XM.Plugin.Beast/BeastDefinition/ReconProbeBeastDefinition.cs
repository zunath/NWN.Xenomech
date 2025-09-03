using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ReconProbeBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ReconProbe;
        public int LevelRequired => 3;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 43;
        public int SoundSetId => 43;
        public LocaleString Name => LocaleString.ReconProbe;
        public int AttackDelay => 280;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.E,
            Might = GradeType.F,
            Perception = GradeType.C,
            Vitality = GradeType.F,
            Agility = GradeType.C,
            Willpower = GradeType.E,
            Social = GradeType.F,

            DMG = GradeType.G,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Probe Strike ability when available
        };
    }
}