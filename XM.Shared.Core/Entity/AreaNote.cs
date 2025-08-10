using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class AreaNote : EntityBase
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


