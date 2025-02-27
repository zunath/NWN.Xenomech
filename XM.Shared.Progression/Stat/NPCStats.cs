namespace XM.Progression.Stat
{
    public class NPCStats
    {
        public GradeType EvasionGrade { get; set; }
        public int Level { get; set; }
        public int MainHandDMG { get; set; }
        public int OffHandDMG { get; set; }
        public int MainHandDelay { get; set; }
        public int OffHandDelay { get; set; }
        
        public StatGroup StatGroup { get; set; }

        public NPCStats()
        {
            EvasionGrade = GradeType.C;
            StatGroup = new StatGroup();
        }
    }
}
