using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class IonWolfBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.IonWolf;
        public int LevelRequired => 27;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 27;
        public int SoundSetId => 27;
        public LocaleString Name => LocaleString.IonWolf;
        public int AttackDelay => 280;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.D,
            MaxEP = GradeType.B,
            Might = GradeType.D,
            Perception = GradeType.B,
            Vitality = GradeType.D,
            Agility = GradeType.B,
            Willpower = GradeType.B,
            Social = GradeType.F,

            DMG = GradeType.D,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Ion Bite ability when available
            // TODO: Add Energy Howl ability when available
            // TODO: Add Discharge ability when available
        };
    }
}