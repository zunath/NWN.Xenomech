using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class FrostWalkerBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.FrostWalker;
        public int LevelRequired => 18;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 7;
        public int SoundSetId => 7;
        public LocaleString Name => LocaleString.FrostWalker;
        public int AttackDelay => 380;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.B,
            MaxEP = GradeType.D,
            Might = GradeType.B,
            Perception = GradeType.E,
            Vitality = GradeType.B,
            Agility = GradeType.F,
            Willpower = GradeType.D,
            Social = GradeType.F,

            DMG = GradeType.C,
            Evasion = GradeType.F
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Ice Claws ability when available
            // TODO: Add Freeze Ray ability when available
        };
    }
}