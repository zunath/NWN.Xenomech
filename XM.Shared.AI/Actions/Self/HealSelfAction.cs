using Anvil.API;
using System.Collections.Generic;
using XM.AI.Targeters;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Self
{
    internal class HealSelfAction: AIActionBase
    {
        public HealSelfAction(IAIContext context) 
            : base(context, new SelfTargeter())
        {
        }

        private readonly HashSet<FeatType> _fullAbilityPool =
        [
            FeatType.EtherBloom4,
            FeatType.EtherBloom3,
            FeatType.EtherBloom2,
            FeatType.EtherBloom1,
        ];

        private readonly HashSet<FeatType> _filteredAbilityPool = new();
        private FeatType _selectedFeat = FeatType.Invalid;


        public override void Initialize()
        {
            foreach (var feat in _fullAbilityPool)
            {
                if (GetHasFeat(feat, Context.Creature))
                    _filteredAbilityPool.Add(feat);
            }
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
            foreach (var feat in _filteredAbilityPool)
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
