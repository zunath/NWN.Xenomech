using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class AlphaPrimeBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.AlphaPrime;
        public int LevelRequired => 43;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 27;
        public int SoundSetId => 27;
        public LocaleString Name => LocaleString.AlphaPrime;
        public int AttackDelay => 260;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.B,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.A,
            Vitality = GradeType.B,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.D,

            DMG = GradeType.B,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Prime Strike ability when available
            // TODO: Add Pack Command ability when available
            // TODO: Add Ultimate Protocol ability when available
            // TODO: Add Apex Evolution ability when available
        };
    }
}