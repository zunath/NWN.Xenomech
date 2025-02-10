using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Self
{
    internal class HealSelfAction: AIActionBase
    {
        public HealSelfAction(IAIContext context) 
            : base(context, new SelfTargeter())
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;


        public override void Initialize()
        {
        }

        protected override float CalculateScore()
        {
            var creature = Context.Creature;
            var hp = (float)GetCurrentHitPoints(creature) / (float)GetMaxHitPoints(creature);
            var score = 0;

            if (hp <= 0.2f)
                score = 100;
            else if (hp <= 0.5f)
                score = 80;
            else if (hp <= 0.7f)
                score = 30;

            if (score > 0)
                score = FindBestAvailableAbility() ? score : 0;

            return score;
        }

        private bool FindBestAvailableAbility()
        {
            var target = Targeter.SelectTarget(Context);
            var feats = Context.GetFeatsByType(AbilityClassificationType.Healing, AITargetType.Self);
            foreach (var feat in feats)
            {
                if (CanUseAbility(feat, target))
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
            AssignCommand(Context.Creature, () => ActionUseFeat(_selectedFeat, Context.Creature));
        }
    }
}
