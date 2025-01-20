using System.Collections.Generic;

namespace XM.Progression.Stat
{
    public class ResistCollection: Dictionary<ResistType, int>
    {
        public ResistCollection()
        {
            this[ResistType.Fire] = 0;
            this[ResistType.Ice] = 0;
            this[ResistType.Wind] = 0;
            this[ResistType.Earth] = 0;
            this[ResistType.Lightning] = 0;
            this[ResistType.Water] = 0;
            this[ResistType.Light] = 0;
            this[ResistType.Darkness] = 0;
            this[ResistType.Mind] = 0;
        }
    }
}
