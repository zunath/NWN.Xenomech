using System.Collections.Generic;
using Anvil.Services;
using XM.Progression;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Beast.BeastDefinition
{
    [ServiceBinding(typeof(IBeastDefinition))]
    internal class PhotonStagBeastDefinition: IBeastDefinition
    {
        public BeastType Type => BeastType.PhotonStag;
        public int LevelRequired => 36;
        public AppearanceType Appearance => AppearanceType.Badger; // TODO: Replace with proper appearance when available
        public float Scale => 1f;
        public int PortraitId => 24;
        public int SoundSetId => 24;
        public LocaleString Name => LocaleString.PhotonStag;
        public int AttackDelay => 280;

        public StatGrade Grades => new()
        {
            MaxHP = GradeType.B,
            MaxEP = GradeType.A,
            Might = GradeType.B,
            Perception = GradeType.A,
            Vitality = GradeType.B,
            Agility = GradeType.B,
            Willpower = GradeType.A,
            Social = GradeType.C,

            DMG = GradeType.B,
            Evasion = GradeType.B
        };

        public List<FeatType> Feats => new()
        {
            // TODO: Add Laser Beam ability when available
            // TODO: Add Purification Ray ability when available
            // TODO: Add Light Blessing ability when available
            // TODO: Add Hard Light Shield ability when available
        };
    }
}