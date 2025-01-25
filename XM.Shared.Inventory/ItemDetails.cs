namespace XM.Inventory
{
    public class ItemDetails
    {
        public uint Item { get; private set; }

        public int DMG { get; set; }
        public int Delay { get; set; }

        public ItemDetails(uint item)
        {
            Item = item;
        }
    }
}
