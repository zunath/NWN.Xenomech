namespace XM.AI.Targeters
{
    internal class SelfTargeter: IAITargeter
    {
        public uint SelectTarget(IAIContext context)
        {
            return context.Creature;
        }
    }
}
