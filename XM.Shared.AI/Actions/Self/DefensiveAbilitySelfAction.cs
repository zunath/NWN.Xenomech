using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Self
{
    internal class DefensiveAbilitySelfAction : AIActionBase
    {
        public DefensiveAbilitySelfAction(IAIContext context)
            : base(context, new SelfTargeter())
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            var creature = Context.Creature;
            _target = creature;

            if (FindBestAvailableAbility())
                return 75; // High priority for survivability

            return 0;
        }

        private bool FindBestAvailableAbility()
        {
            var feats = Context.GetFeatsByType(AbilityCategoryType.Defensive, AITargetType.Self);
            foreach (var feat in feats)
            {
                if (CanUseAbility(feat, _target))
                {
                    _selectedFeat = feat;
                    return true;
                }
            }

            _selectedFeat = FeatType.Invalid;
            return false;
        }

        public override void Execute()
        {
            UseAbility(_selectedFeat, _target);
        }
    }
}