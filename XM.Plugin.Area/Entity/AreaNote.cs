using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Plugin.Area.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class AreaNote: EntityBase
    {
        [Indexed]
        public string AreaResref { get; set; }
        public string PublicText { get; set; }
        public string PrivateText { get; set; }

        public AreaNote()
        {
            PublicText = string.Empty;
            PrivateText = string.Empty;
        }
    }
}
