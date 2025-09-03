using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class OmegaFrameBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.OmegaFrame;
        public int LevelRequired => 38;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.OmegaFrame;
        public int AttackDelay => 500;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.B,
            Might = GradeType.A,
            Perception = GradeType.D,
            Vitality = GradeType.A,
            Agility = GradeType.E,
            Willpower = GradeType.B,
            Social = GradeType.F,

            DMG = GradeType.A,
            Evasion = GradeType.E
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Omega Strike ability when available
            // TODO: Add Energy Barrier ability when available
            // TODO: Add Power Overload ability when available
            // TODO: Add System Override ability when available
        };
    }
}