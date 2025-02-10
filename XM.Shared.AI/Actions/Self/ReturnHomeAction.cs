using XM.AI.Targeters;

namespace XM.AI.Actions.Self
{
    internal class ReturnHomeAction: AIActionBase
    {
        public ReturnHomeAction(IAIContext context) 
            : base(context, new SelfTargeter())
        {
        }

        protected override float CalculateScore()
        {
            if (!Context.AIFlags.HasFlag(AIFlag.ReturnHome))
                return 0;

            var location = GetLocation(Context.Creature);
            if (GetDistanceBetweenLocations(location, Context.HomeLocation) <= 15f)
                return 0;

            return 1;
        }

        public override void Execute()
        {
            AssignCommand(Context.Creature, () => ActionMoveToLocation(Context.HomeLocation));
        }
    }
}
