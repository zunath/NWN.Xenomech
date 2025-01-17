using XM.Shared.API.BaseTypes;

namespace XM.Progression.Ability
{
    internal delegate bool AbilityActivationAction(
        uint activator, 
        uint target, 
        Location targetLocation);
    internal delegate void AbilityImpactAction(
        uint activator, 
        uint target, 
        Location targetLocation);
    internal delegate float AbilityActivationDelayAction(
        uint activator, 
        uint target);
    internal delegate float AbilityRecastDelayAction(uint activator);
    internal delegate string AbilityCustomValidationAction(
        uint activator, 
        uint target, 
        Location targetLocation);

}
