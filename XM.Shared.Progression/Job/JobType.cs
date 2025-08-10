using Ardalis.SmartEnum;

namespace XM.Progression.Job
{
    public sealed class JobType : SmartEnum<JobType>
    {
        public static readonly JobType Invalid = new(nameof(Invalid), 0);
        public static readonly JobType Keeper = new(nameof(Keeper), 1);
        public static readonly JobType Mender = new(nameof(Mender), 2);
        public static readonly JobType Techweaver = new(nameof(Techweaver), 3);
        public static readonly JobType Beastmaster = new(nameof(Beastmaster), 4);
        public static readonly JobType Brawler = new(nameof(Brawler), 5);
        public static readonly JobType Nightstalker = new(nameof(Nightstalker), 6);
        public static readonly JobType Hunter = new(nameof(Hunter), 7);
        public static readonly JobType Elementalist = new(nameof(Elementalist), 8);

        private JobType(string name, int value) : base(name, value) { }
    }
}
