using System.Linq;
using XM.AI.BehaviorTree.FluentBuilder;

namespace XM.AI.Context.Condition
{
    internal static class ItemConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasAnyItem(
            this FluentBuilder<CreatureAIContext> builder, 
            params string[] resrefs)
        {
            return builder.Condition("Has Item", context =>
            {
                var item = GetFirstItemInInventory(context.Creature);
                while (GetIsObjectValid(item))
                {
                    if (resrefs.Contains(GetResRef(item)))
                        break;

                    item = GetNextItemInInventory(context.Creature);
                }

                var hasItem = GetIsObjectValid(item);

                if (hasItem)
                {
                    context.SelectedItem = item;
                }

                return hasItem;
            });
        }
    }
}
