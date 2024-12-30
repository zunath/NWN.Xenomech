using Anvil.Services;

namespace XM.Data.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
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
