using System.Collections.Generic;

namespace XM.Plugin.Mech
{
    public interface IMechFrameListDefinition
    {
        public Dictionary<string, MechFrameStats> BuildMechFrames();
    }
}