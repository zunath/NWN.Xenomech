using XM.AI.Targeters;

namespace XM.AI.Actions.Enemy
{
    internal class AttackEnemyAction: AIActionBase
    {
        public AttackEnemyAction(IAIContext context) 
            : base(context, new HighestEnmityTargeter())
        {
        }

        private uint _target;

        protected override float CalculateScore()
        {
            _target = Context.Services.Enmity.GetHighestEnmityTarget(Context.Creature);
            if (_target == OBJECT_INVALID)
                return 0;

            var currentTarget = GetAttackTarget(Context.Creature);
            if (currentTarget == _target)
                return 0;

            return 10;
        }

        public override void Execute()
        {
            var creature = Context.Creature;
            AssignCommand(creature, () => ClearAllActions());
            AssignCommand(creature, () => ActionAttack(_target));
        }
    }
}