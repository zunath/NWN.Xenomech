using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class TitanBeastBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.TitanBeast;
        public int LevelRequired => 44;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 7;
        public int SoundSetId => 7;
        public LocaleString Name => LocaleString.TitanBeast;
        public int AttackDelay => 480;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.A,
            MaxEP = GradeType.A,
            Might = GradeType.A,
            Perception = GradeType.C,
            Vitality = GradeType.A,
            Agility = GradeType.D,
            Willpower = GradeType.A,
            Social = GradeType.E,

            DMG = GradeType.A,
            Evasion = GradeType.D
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Rage Protocol ability when available
            // TODO: Add Tectonic Strike ability when available
            // TODO: Add Unstoppable Force ability when available
            // TODO: Add Apex Program ability when available
        };
    }
}