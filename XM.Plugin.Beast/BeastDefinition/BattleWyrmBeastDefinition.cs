using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class BattleWyrmBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.BattleWyrm;
        public int LevelRequired => 28;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 29;
        public int SoundSetId => 29;
        public LocaleString Name => LocaleString.BattleWyrm;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.B,
            Might = GradeType.C,
            Perception = GradeType.B,
            Vitality = GradeType.C,
            Agility = GradeType.C,
            Willpower = GradeType.B,
            Social = GradeType.E,

            DMG = GradeType.C,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Tail Laser ability when available
            // TODO: Add Wing Thrusters ability when available
            // TODO: Add Nano Sting ability when available
        };
    }
}