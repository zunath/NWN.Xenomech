using System;
using System.Collections.Generic;
using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerMech : EntityBase
    {
        public PlayerMech()
        {
            Init();
        }

        public PlayerMech(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {
            MechCap = 1;
            SavedMechs = new Dictionary<Guid, MechConfiguration>();
        }

        public int MechCap { get; set; }
        public Guid ActiveMechId { get; set; }
        public Dictionary<Guid, MechConfiguration> SavedMechs { get; set; }
    }

    public class MechConfiguration
    {
        public string FrameResref { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Dictionary<int, string> Parts { get; set; } = new();
    }
}
