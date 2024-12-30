using System;

namespace XM.Spawn
{
    internal class QueuedSpawn
    {
        public DateTime RespawnTime { get; set; }
        public Guid SpawnDetailId { get; set; }
    }
}
