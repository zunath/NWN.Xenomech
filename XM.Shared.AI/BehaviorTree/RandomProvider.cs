using System;

namespace XM.AI.BehaviorTree
{
    public sealed class RandomProvider : IRandomProvider
    {
        private static readonly Random _random = new();

        public double NextRandomDouble()
        {
            return _random.NextDouble();
        }

        public int NextRandomInteger(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public static readonly IRandomProvider Default = new RandomProvider();
    }
}