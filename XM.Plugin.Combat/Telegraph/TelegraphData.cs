namespace XM.Plugin.Combat.Telegraph
{
    internal class TelegraphData
    {
        public TelegraphType Shape { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Rotation { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public float Duration { get; set; }
        public string OnStartScript { get; set; }
        public string OnUpdateScript { get; set; }
        public string OnFinishScript { get; set; }
        public float UpdateInterval { get; set; } = 0f;
    }
}
