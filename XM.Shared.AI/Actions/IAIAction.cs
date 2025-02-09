namespace XM.AI.Actions
{
    internal interface IAIAction
    {
        void Initialize();
        float DetermineScore();
        void Execute();
        bool IsComplete { get; }
    }
}
