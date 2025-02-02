using Anvil.API;
using XM.Progression.Recast;
using XM.Shared.API.Constants;

namespace XM.Plugin.Item
{
    public delegate void ApplyItemEffectsDelegate(uint user, uint item, uint target, Location targetLocation, int itemPropertyIndex);

    public delegate string ValidateItemDelegate(uint user, uint item, uint target, Location targetLocation, int itemPropertyIndex);

    public delegate float CalculateDistanceDelegate(uint user, uint item, uint target, Location targetLocation, int itemPropertyIndex );

    public delegate bool ReducesItemChargeDelegate(uint user, uint item, uint target, Location targetLocation, int itemPropertyIndex );

    public delegate string InitializationMessageDelegate(uint user, uint item, uint target, Location targetLocation, int itemPropertyIndex);

    public delegate float CalculateDelayDelegate(uint user, uint item, uint target, Location targetLocation, int itemPropertyIndex);
    public class ItemDetail
    {
        public InitializationMessageDelegate InitializationMessageAction { get; set; }
        public ValidateItemDelegate ValidateAction { get; set; }
        public ApplyItemEffectsDelegate ApplyAction { get; set; }
        public CalculateDistanceDelegate CalculateDistanceAction { get; set; }
        public CalculateDelayDelegate DelayAction { get; set; }
        public bool UserFacesTarget { get; set; }
        public AnimationType ActivationAnimation { get; set; }
        public ReducesItemChargeDelegate ReducesItemChargeAction { get; set; }
        public bool CanTargetLocation { get; set; }
        public RecastGroup? RecastGroup { get; set; }
        public float? RecastCooldown { get; set; }

        public ItemDetail()
        {
            InitializationMessageAction = (user, item, target, location, itemPropertyIndex) => string.Empty;
            DelayAction = (user, item, target, location, itemPropertyIndex) => 0.0f;
            UserFacesTarget = false;
            ActivationAnimation = AnimationType.Invalid;
            CalculateDistanceAction = (user, item, target, location, itemPropertyIndex) => 3.5f;
            ReducesItemChargeAction = (user, item, target, location, itemPropertyIndex) => false;
            CanTargetLocation = false;
        }
        
    }
}
