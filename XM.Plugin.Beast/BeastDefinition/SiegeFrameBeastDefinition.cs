using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class SiegeFrameBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.SiegeFrame;
        public int LevelRequired => 25;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 44;
        public int SoundSetId => 44;
        public LocaleString Name => LocaleString.SiegeFrame;
        public int AttackDelay => 420;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.D,
            Might = GradeType.A,
            Perception = GradeType.F,
            Vitality = GradeType.A,
            Agility = GradeType.G,
            Willpower = GradeType.D,
            Social = GradeType.G,

            DMG = GradeType.A,
            Evasion = GradeType.G
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Seismic Strike ability when available
            // TODO: Add Barrier Field ability when available
            // TODO: Add Artillery ability when available
        };
    }
}