namespace XM.Progression.Stat
{
    public class ItemStatGroup: StatGroup
    {
        public bool IsEquipped { get; set; }
        public int DMG { get; set; }
        public int Delay { get; set; }
        public float Condition { get; set; }

        public ItemStatGroup()
        {
            Condition = 1f;
        }
    }
}
