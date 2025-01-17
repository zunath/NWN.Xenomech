using Anvil.API;
using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;

namespace XM.AI.Context.DoAction
{
    internal static class ItemDoActions
    {
        public static FluentBuilder<CreatureAIContext> DoUseSelectedItem(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Do("Use Selected Item", context =>
            {
                var ip = GetFirstItemProperty(context.SelectedItem);
                AssignCommand(
                    context.Creature,
                    () => ActionUseItemOnObject(context.SelectedItem, ip, context.Creature));

                context.SelectedItem = OBJECT_INVALID;
                return BehaviorStatus.Succeeded;
            });
        }
    }
}
