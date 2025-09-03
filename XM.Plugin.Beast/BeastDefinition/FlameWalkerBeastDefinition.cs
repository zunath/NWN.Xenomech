using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class FlameWalkerBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.FlameWalker;
        public int LevelRequired => 21;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 20;
        public int SoundSetId => 15;
        public LocaleString Name => LocaleString.FlameWalker;
        public int AttackDelay => 260;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.C,
            Might = GradeType.E,
            Perception = GradeType.B,
            Vitality = GradeType.E,
            Agility = GradeType.A,
            Willpower = GradeType.C,
            Social = GradeType.E,

            DMG = GradeType.E,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Fire Dash ability when available
            // TODO: Add Heat Wave ability when available
            // TODO: Add Ignition ability when available
        };
    }
}