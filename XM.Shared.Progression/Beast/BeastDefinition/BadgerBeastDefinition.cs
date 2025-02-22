using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class BadgerBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.BadgerTest;
        public AppearanceType Appearance => AppearanceType.Badger;
        public float Scale => 1f;
        public int PortraitId => 144;
        public int SoundSetId => 4;
        public LocaleString Name => LocaleString.Badger;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.C,
            MaxEP = GradeType.C,
            Might = GradeType.D,
            Perception = GradeType.C,
            Vitality = GradeType.D,
            Agility = GradeType.F,
            Willpower = GradeType.E,
            Social = GradeType.A,

            DMG = GradeType.C,
            Evasion = GradeType.C
        };

        public List<FeatType> Feats => new()
        {
            FeatType.FlameBreath
        };
    }
}
