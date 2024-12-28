namespace NWN.Xenomech.Data.Entity
{
    public class Account: EntityBase
    {
        public Account()
        {
        }

        public Account(string cdKey)
        {
            Id = cdKey;
        }

        [Indexed]
        public ulong TimesLoggedIn { get; set; }

        [Indexed]
        public bool HasCompletedTutorial { get; set; }
    }

}
