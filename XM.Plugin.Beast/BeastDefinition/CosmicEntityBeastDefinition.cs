using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class CosmicEntityBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.CosmicEntity;
        public int LevelRequired => 46;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 42;
        public int SoundSetId => 42;
        public LocaleString Name => LocaleString.CosmicEntity;
        public int AttackDelay => 180;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.B,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.A,
            Vitality = GradeType.B,
            Agility = GradeType.A,
            Willpower = GradeType.A,
            Social = GradeType.A,

            DMG = GradeType.A,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Stellar Dive ability when available
            // TODO: Add Gravity Storm ability when available
            // TODO: Add Black Hole ability when available
            // TODO: Add Supernova ability when available
        };
    }
}