using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class NightmareRiderBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.NightmareRider;
        public int LevelRequired => 47;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 21;
        public int SoundSetId => 21;
        public LocaleString Name => LocaleString.NightmareRider;
        public int AttackDelay => 280;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.B,
            Vitality = GradeType.A,
            Agility = GradeType.A,
            Willpower = GradeType.A,
            Social = GradeType.D,

            DMG = GradeType.A,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Hellfire Charge ability when available
            // TODO: Add Fear Protocol ability when available
            // TODO: Add Shadow Gallop ability when available
            // TODO: Add Apocalypse Systems ability when available
        };
    }
}