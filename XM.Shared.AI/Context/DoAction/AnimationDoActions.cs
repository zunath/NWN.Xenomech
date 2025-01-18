using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;
using XM.Shared.API.Constants;

namespace XM.AI.Context.DoAction
{
    internal static class AnimationDoActions
    {
        public static FluentBuilder<CreatureAIContext> DoPlayAnimation(
            this FluentBuilder<CreatureAIContext> builder,
            AnimationType animation,
            float speed = 1f,
            float durationSeconds = 0f)
        {
            return builder.Do("Play Animation", context =>
            {
                AssignCommand(context.Creature, () => ActionPlayAnimation(animation, speed, durationSeconds));

                return BehaviorStatus.Succeeded;
            });
        }
    }
}
