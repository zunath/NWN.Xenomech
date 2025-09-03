using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class VoidStalkerBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.VoidStalker;
        public int LevelRequired => 37;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 1;
        public int SoundSetId => 1;
        public LocaleString Name => LocaleString.VoidStalker;
        public int AttackDelay => 300;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.B,
            MaxEP = GradeType.A,
            Might = GradeType.B,
            Perception = GradeType.A,
            Vitality = GradeType.B,
            Agility = GradeType.A,
            Willpower = GradeType.A,
            Social = GradeType.E,

            DMG = GradeType.B,
            Evasion = GradeType.A
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Shadow Maul ability when available
            // TODO: Add Dark Contract ability when available
            // TODO: Add Nightmare Protocol ability when available
            // TODO: Add Soul Drain ability when available
        };
    }
}