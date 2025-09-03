using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class EchoWingBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.EchoWing;
        public int LevelRequired => 14;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 6;
        public int SoundSetId => 6;
        public LocaleString Name => LocaleString.EchoWing;
        public int AttackDelay => 260;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.F,
            MaxEP = GradeType.C,
            Might = GradeType.F,
            Perception = GradeType.D,
            Vitality = GradeType.F,
            Agility = GradeType.A,
            Willpower = GradeType.C,
            Social = GradeType.F,

            DMG = GradeType.F,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Sonic Pulse ability when available
            // TODO: Add Flight Mode ability when available
        };
    }
}