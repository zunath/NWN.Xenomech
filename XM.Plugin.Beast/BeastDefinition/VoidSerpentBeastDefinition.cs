using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class VoidSerpentBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.VoidSerpent;
        public int LevelRequired => 41;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 30;
        public int SoundSetId => 30;
        public LocaleString Name => LocaleString.VoidSerpent;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.B,
            Perception = GradeType.B,
            Vitality = GradeType.A,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.C,

            DMG = GradeType.B,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Void Breath ability when available
            // TODO: Add Reality Fracture ability when available
            // TODO: Add Null Field ability when available
            // TODO: Add Dimension Storm ability when available
        };
    }
}