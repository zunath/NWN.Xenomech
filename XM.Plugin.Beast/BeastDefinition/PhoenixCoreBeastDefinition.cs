using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class PhoenixCoreBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.PhoenixCore;
        public int LevelRequired => 29;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 37;
        public int SoundSetId => 37;
        public LocaleString Name => LocaleString.PhoenixCore;
        public int AttackDelay => 240;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.A,
            Might = GradeType.D,
            Perception = GradeType.B,
            Vitality = GradeType.D,
            Agility = GradeType.A,
            Willpower = GradeType.A,
            Social = GradeType.E,

            DMG = GradeType.D,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Plasma Burst ability when available
            // TODO: Add Auto-Repair ability when available
            // TODO: Add Energy Aura ability when available
        };
    }
}