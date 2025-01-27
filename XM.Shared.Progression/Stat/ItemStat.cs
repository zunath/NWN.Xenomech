namespace XM.Progression.Stat
{
    public class ItemStat
    {
        public bool IsEquipped { get; set; }
        public int DMG { get; set; }
        public int Delay { get; set; }
        public int HP { get; set; }
        public int EP { get; set; }
        public int HPRegen { get; set; }
        public int EPRegen { get; set; }
        public int RecastReduction { get; set; }
        public int Defense { get; set; }
        public int Evasion { get; set; }
        public int Accuracy { get; set; }
        public int Attack { get; set; }
        public int EtherAttack { get; set; }
        public int TPGain { get; set; }
        public ResistCollection Resists { get; set; }

        public ItemStat()
        {
            Resists = new ResistCollection();
        }
    }
}
