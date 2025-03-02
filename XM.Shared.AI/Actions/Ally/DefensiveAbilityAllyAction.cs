using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Ally
{
    internal class DefensiveAbilityAllyAction : AIActionBase
    {
        public DefensiveAbilityAllyAction(IAIContext context)
            : base(context, new LowestHPAllyTargeter())
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            _target = Targeter.SelectTarget(Context);
            if (_target == OBJECT_INVALID)
                return 0;

            if (FindBestAvailableAbility())
                return 60; // Medium-high priority to protect allies

            return 0;
        }

        private bool FindBestAvailableAbility()
        {
            var feats = Context.GetFeatsByType(AbilityCategoryType.Defensive, AITargetType.Others);
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
