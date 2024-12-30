using System;
using System.Collections.Generic;
using XM.AI;
using XM.API.Constants;

namespace XM.Spawn
{
    public delegate void OnSpawnDelegate(uint spawn);

    internal class SpawnObject
    {
        public ObjectType Type { get; set; }
        public string Resref { get; set; }
        public int Weight { get; set; }
        public AIFlag AIFlags { get; set; }

        public List<DayOfWeek> RealWorldDayOfWeekRestriction { get; set; }
        public TimeSpan? RealWorldStartRestriction { get; set; }
        public TimeSpan? RealWorldEndRestriction { get; set; }

        public int GameHourStartRestriction { get; set; }
        public int GameHourEndRestriction { get; set; }

        public List<OnSpawnDelegate> OnSpawnActions { get; } = new();

        public SpawnObject()
        {
            AIFlags = AIFlag.None;
            RealWorldDayOfWeekRestriction = new List<DayOfWeek>();
            GameHourStartRestriction = -1;
            GameHourEndRestriction = -1;
        }
    }
}
