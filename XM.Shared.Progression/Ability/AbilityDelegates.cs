using Anvil.API;

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

    public delegate void AbilityEquippedAction(uint creature);

    public delegate void AbilityUnequippedAction(uint creature);
}
