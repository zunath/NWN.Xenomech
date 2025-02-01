using XM.Progression.Recast;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Ability
{
    public class AbilityDetail
    {
        public LocaleString Name { get; set; }
        public AbilityActivationAction ActivationAction { get; set; }
        public AbilityImpactAction ImpactAction { get; set; }
        public AbilityActivationDelayAction ActivationDelay { get; set; }
        public AbilityRecastDelayAction RecastDelay { get; set; }
        public AbilityCustomValidationAction CustomValidation { get; set; }
        public int EPRequired { get; set; }
        public VisualEffectType ActivationVisualEffect { get; set; }
        public RecastGroup RecastGroup { get; set; }
        public AbilityActivationType ActivationType { get; set; }
        public AnimationType AnimationType { get; set; }
        public float MaxRange { get; set; }
        public bool IsHostileAbility { get; set; }
        public bool DisplaysActivationMessage { get; set; }
        public int ResonanceCost { get; set; }

        public AbilityDetail()
        {
            ActivationVisualEffect = VisualEffectType.None;
            AnimationType = AnimationType.Invalid;
            EPRequired = 0;
            MaxRange = 5.0f;
            IsHostileAbility = false;
            DisplaysActivationMessage = true;
        }
    }
}
