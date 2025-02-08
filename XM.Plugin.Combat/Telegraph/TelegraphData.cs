using System.Numerics;

namespace XM.Plugin.Combat.Telegraph
{
    internal class TelegraphData
    {
        public uint Creator { get; set; }
        public TelegraphType Shape { get; set; }
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Size { get; set; }
        public float Duration { get; set; }
        
    }
}
