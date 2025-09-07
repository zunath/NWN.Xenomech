namespace XM.Plugin.Mech
{
    public class MechFrameStats
    {
        public MechFrameType FrameType { get; set; } = MechFrameType.Invalid;
        public int LevelRequirement { get; set; } = 0;
        public int BaseHp { get; set; } = 0;
        public int BaseFuel { get; set; } = 0;
        public int BaseAttack { get; set; } = 0;
        public int BaseEtherAttack { get; set; } = 0;
        public int BaseDefense { get; set; } = 0;
        public int BaseEtherDefense { get; set; } = 0;
        public int BaseAccuracy { get; set; } = 0;
        public int BaseEvasion { get; set; } = 0;
        public int BaseFuelConsumption { get; set; } = 0;
    }
}