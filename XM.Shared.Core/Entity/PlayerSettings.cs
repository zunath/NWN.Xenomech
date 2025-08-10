using Anvil.Services;

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
            DisplayServerResetReminders = true;
            ShowHelmet = true;
            ShowCloak = true;
            AppearanceScale = 1f;
        }

        public bool DisplayServerResetReminders { get; set; }
        public bool ShowHelmet { get; set; }
        public bool ShowCloak { get; set; }
        public float AppearanceScale { get; set; }
    }
}
