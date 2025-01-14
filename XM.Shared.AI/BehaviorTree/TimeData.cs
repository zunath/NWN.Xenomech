namespace XM.AI.BehaviorTree
{
    /// <summary>
    /// Represents time. Used to pass time values to behaviour tree nodes.
    /// </summary>
    public struct TimeData
    {
        public TimeData(float deltaTime)
        {
            DeltaTime = deltaTime;
        }

        public float DeltaTime { get; }
    }
}
