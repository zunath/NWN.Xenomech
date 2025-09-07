namespace XM.Plugin.Mech
{
    public class MechPartStats
    {
        public MechPartType PartType { get; set; } = MechPartType.Invalid;
        public int HpPercent { get; set; } = 0;
        public int FuelPercent { get; set; } = 0;
        public int AttackPercent { get; set; } = 0;
        public int EtherAttackPercent { get; set; } = 0;
        public int DefensePercent { get; set; } = 0;
        public int EtherDefensePercent { get; set; } = 0;
        public int AccuracyPercent { get; set; } = 0;
        public int EvasionPercent { get; set; } = 0;
        public int FuelConsumptionPercent { get; set; } = 0;
    }
}