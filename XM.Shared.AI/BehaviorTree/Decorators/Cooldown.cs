namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class Cooldown<TContext> : DecoratorBehavior<TContext> where TContext : IClock
    {
        public readonly long CooldownTimeInMilliseconds;
        private long _cooldownStartedTimestamp;

        public bool OnCooldown { get; private set; }

        public Cooldown(IBehavior<TContext> child, int cooldownTimeInMilliseconds) : this("Cooldown", child, cooldownTimeInMilliseconds)
        {
        }

        public Cooldown(string name, IBehavior<TContext> child, int cooldownTimeInMilliseconds) : base(name, child)
        {
            CooldownTimeInMilliseconds = cooldownTimeInMilliseconds;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            return OnCooldown ? CooldownBehaviour(context) : RegularBehaviour(context);
        }

        private BehaviorStatus RegularBehaviour(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviorStatus.Succeeded)
            {
                EnterCooldown(context);
            }

            return childStatus;
        }

        private BehaviorStatus CooldownBehaviour(TContext context)
        {
            var currentTimeStamp = context.GetTimeStampInMilliseconds();

            var elapsedMilliseconds = currentTimeStamp - _cooldownStartedTimestamp;

            if (elapsedMilliseconds >= CooldownTimeInMilliseconds)
            {
                ExitCooldown();

                return RegularBehaviour(context);
            }

            return BehaviorStatus.Failed;
        }

        private void ExitCooldown()
        {
            OnCooldown = false;
            _cooldownStartedTimestamp = 0;
        }

        private void EnterCooldown(TContext context)
        {
            OnCooldown = true;
            _cooldownStartedTimestamp = context.GetTimeStampInMilliseconds();
        }
    }
}
