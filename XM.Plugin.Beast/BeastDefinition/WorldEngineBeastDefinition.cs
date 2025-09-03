using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class WorldEngineBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.WorldEngine;
        public int LevelRequired => 48;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 26;
        public int SoundSetId => 26;
        public LocaleString Name => LocaleString.WorldEngine;
        public int AttackDelay => 600;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.E,
            Vitality = GradeType.A,
            Agility = GradeType.F,
            Willpower = GradeType.A,
            Social = GradeType.F,

            DMG = GradeType.A,
            Evasion = GradeType.F
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Continental Strike ability when available
            // TODO: Add World Barrier ability when available
            // TODO: Add Ecosystem Override ability when available
            // TODO: Add Planet Pulse ability when available
        };
    }
}