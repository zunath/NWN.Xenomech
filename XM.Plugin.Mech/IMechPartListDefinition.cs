using System.Collections.Generic;

namespace XM.Plugin.Mech
{
    public interface IMechPartListDefinition
    {
        public Dictionary<string, MechPartStats> BuildMechParts();
    }
}