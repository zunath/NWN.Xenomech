using System;
using System.Collections.Generic;
using System.Linq;

namespace XM.Progression.Stat
{
    public class ResistCollection: Dictionary<ResistType, int>
    {
        public ResistCollection()
        {
            foreach (var resist in Enum.GetValues(typeof(ResistType)).Cast<ResistType>())
            {
                this[resist] = 0;
            }
        }
    }
}
