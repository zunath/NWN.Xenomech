using System.Collections.Generic;

namespace XM.Shared.Core.Party
{
    internal class PartyDetail
    {
        public uint Leader { get; set; }
        public List<uint> Members { get; set; }
        public List<uint> Associates { get; set; }

        public PartyDetail()
        {
            Leader = OBJECT_INVALID;
            Members = new List<uint>();
            Associates = new List<uint>();
        }
    }
}
