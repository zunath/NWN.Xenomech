using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class GenesisMachineBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.GenesisMachine;
        public int LevelRequired => 50;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 40;
        public int SoundSetId => 40;
        public LocaleString Name => LocaleString.GenesisMachine;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.B,
            Vitality = GradeType.A,
            Agility = GradeType.C,
            Willpower = GradeType.A,
            Social = GradeType.B,

            DMG = GradeType.A,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Genesis Protocol ability when available
            // TODO: Add Reality Engine ability when available
            // TODO: Add Infinite Processing ability when available
            // TODO: Add Omninet Access ability when available
        };
    }
}