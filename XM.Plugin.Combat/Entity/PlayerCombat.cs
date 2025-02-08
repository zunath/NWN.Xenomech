using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Plugin.Combat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerCombat: EntityBase
    {
        public PlayerCombat()
        {
        }

        public PlayerCombat(string id)
        {
            Id = id;
        }

    }
}
