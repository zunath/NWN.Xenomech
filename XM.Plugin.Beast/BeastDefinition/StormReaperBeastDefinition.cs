using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class StormReaperBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.StormReaper;
        public int LevelRequired => 33;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 43;
        public int SoundSetId => 43;
        public LocaleString Name => LocaleString.StormReaper;
        public int AttackDelay => 200;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.A,
            Might = GradeType.D,
            Perception = GradeType.A,
            Vitality = GradeType.D,
            Agility = GradeType.A,
            Willpower = GradeType.A,
            Social = GradeType.D,

            DMG = GradeType.D,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Lightning Strike ability when available
            // TODO: Add Storm Genesis ability when available
            // TODO: Add Ion Shield ability when available
        };
    }
}