using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class CyberCatBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.CyberCat;
        public int LevelRequired => 5;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 15;
        public int SoundSetId => 15;
        public LocaleString Name => LocaleString.CyberCat;
        public int AttackDelay => 260;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.E,
            MaxEP = GradeType.E,
            Might = GradeType.E,
            Perception = GradeType.C,
            Vitality = GradeType.E,
            Agility = GradeType.B,
            Willpower = GradeType.E,
            Social = GradeType.F,

            DMG = GradeType.E,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Digital Pounce ability when available
            // TODO: Add Stealth Mode ability when available
        };
    }
}