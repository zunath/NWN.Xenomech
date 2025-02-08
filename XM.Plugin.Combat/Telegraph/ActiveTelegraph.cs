namespace XM.Plugin.Combat.Telegraph
{
    internal class ActiveTelegraph
    {
        public TelegraphData Data { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public ActiveTelegraph(int start, int end, TelegraphData data)
        {
            Start = start;
            End = end;
            Data = data;
        }
    }
}
