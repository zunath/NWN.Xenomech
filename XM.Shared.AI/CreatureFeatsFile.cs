using System.Collections.Generic;
using XM.Shared.API.Constants;

namespace XM.AI
{
    public class CreatureFeatsFile
    {
        public string Resref { get; set; }
        public List<FeatType> Feats { get; set; } = new();
    }
}
