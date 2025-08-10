namespace XM.Shared.Core.Entity.Stat
{
    public class ItemStatGroup : StatGroup
    {
        public bool IsEquipped { get; set; }
        public int DMG { get; set; }
        public int Delay { get; set; }
        public float Condition { get; set; } = 1f;
    }
}


