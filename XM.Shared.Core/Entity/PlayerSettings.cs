using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerSettings: EntityBase
    {
        public PlayerSettings()
        {
            Init();
        }

        public PlayerSettings(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {
            ShowHelmet = true;
            ShowCloak = true;
        }

        public bool DisplayServerResetReminders { get; set; }
        public bool ShowHelmet { get; set; }
        public bool ShowCloak { get; set; }
        public float AppearanceScale { get; set; }
    }
}
