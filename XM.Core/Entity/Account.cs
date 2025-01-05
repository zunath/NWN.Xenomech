using Anvil.Services;
using XM.Core.Data;

namespace XM.Core.Entity
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
