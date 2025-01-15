using NLog.Targets;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.Shared.API.BaseTypes;

namespace XM.AI.Context.Condition
{
    internal static class SpatialConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionDistanceAwayFromLocation(
            this FluentBuilder<CreatureAIContext> builder,
            Location location,
            float distance)
        {
            return builder.Condition("Distance Away From Location", context =>
            {
                var creatureLocation = GetLocation(context.Creature);
                return GetDistanceBetweenLocations(creatureLocation, location) >= distance ||
                       GetArea(context.Creature) != GetArea(GetAreaFromLocation(creatureLocation));
            });
        }

        public static FluentBuilder<CreatureAIContext> ConditionDistanceAwayFromObject(
            this FluentBuilder<CreatureAIContext> builder,
            uint target,
            float distance)
        {
            return builder.Condition("Distance Away From Object", context =>
            {
                return GetDistanceBetween(context.Creature, target) >= distance ||
                       GetArea(context.Creature) != GetArea(target);
            });
        }
        public static FluentBuilder<CreatureAIContext> ConditionDistanceAwayFromHome(
            this FluentBuilder<CreatureAIContext> builder,
            float distance)
        {
            return builder.Condition("Distance Away From Home", context =>
            {
                var location = GetLocation(context.Creature);
                return GetDistanceBetweenLocations(location, context.HomeLocation) >= distance ||
                       GetArea(context.Creature) != GetArea(GetAreaFromLocation(location));
            });
        }
    }
}
