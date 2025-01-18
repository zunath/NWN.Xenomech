using Anvil.API;
using XM.Shared.API.BaseTypes;

namespace XM.Progression.Ability
{
    public delegate bool AbilityActivationAction(
        uint activator, 
        uint target, 
        Location targetLocation);
    public delegate void AbilityImpactAction(
        uint activator, 
        uint target, 
        Location targetLocation);
    public delegate float AbilityActivationDelayAction(
        uint activator, 
        uint target);
    public delegate float AbilityRecastDelayAction(uint activator);
    public delegate string AbilityCustomValidationAction(
        uint activator, 
        uint target, 
        Location targetLocation);

}
