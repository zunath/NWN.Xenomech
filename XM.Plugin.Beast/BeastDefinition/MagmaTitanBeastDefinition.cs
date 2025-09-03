using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class MagmaTitanBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.MagmaTitan;
        public int LevelRequired => 34;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 26;
        public int SoundSetId => 26;
        public LocaleString Name => LocaleString.MagmaTitan;
        public int AttackDelay => 480;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.B,
            Might = GradeType.A,
            Perception = GradeType.F,
            Vitality = GradeType.A,
            Agility = GradeType.F,
            Willpower = GradeType.B,
            Social = GradeType.F,

            DMG = GradeType.A,
            Evasion = GradeType.F
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Lava Cannon ability when available
            // TODO: Add Thermal Fortress ability when available
            // TODO: Add Heat Pulse ability when available
        };
    }
}