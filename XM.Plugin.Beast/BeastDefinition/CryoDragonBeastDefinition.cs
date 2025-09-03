using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class CryoDragonBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.CryoDragon;
        public int LevelRequired => 35;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 30;
        public int SoundSetId => 30;
        public LocaleString Name => LocaleString.CryoDragon;
        public int AttackDelay => 320;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.B,
            Perception = GradeType.B,
            Vitality = GradeType.A,
            Agility = GradeType.C,
            Willpower = GradeType.A,
            Social = GradeType.D,

            DMG = GradeType.B,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Ice Breath ability when available
            // TODO: Add Cryo Armor ability when available
            // TODO: Add Blizzard Generator ability when available
            // TODO: Add Zero Point ability when available
        };
    }
}