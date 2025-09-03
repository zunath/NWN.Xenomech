using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class ShadowUnitBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.ShadowUnit;
        public int LevelRequired => 15;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 20;
        public int SoundSetId => 18;
        public LocaleString Name => LocaleString.ShadowUnit;
        public int AttackDelay => 240;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.D,
            Might = GradeType.C,
            Perception = GradeType.B,
            Vitality = GradeType.C,
            Agility = GradeType.A,
            Willpower = GradeType.D,
            Social = GradeType.F,

            DMG = GradeType.C,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Phase Strike ability when available
            // TODO: Add Cloak ability when available
            // TODO: Add Digital Pounce ability when available
        };
    }
}