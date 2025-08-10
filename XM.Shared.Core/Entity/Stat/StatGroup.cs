using System.Collections.Generic;

namespace XM.Shared.Core.Entity.Stat
{
    public class StatGroup
    {
        public Dictionary<int, int> Stats { get; set; } = new();
        public Dictionary<int, int> Resists { get; set; } = new();
    }
}


