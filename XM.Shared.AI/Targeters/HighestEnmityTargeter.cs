namespace XM.AI.Targeters
{
    internal class HighestEnmityTargeter: IAITargeter
    {
        public uint SelectTarget(IAIContext context)
        {
            var target = context
                .Services
                .Enmity
                .GetHighestEnmityTarget(context.Creature);
            return target;
        }
    }
}
