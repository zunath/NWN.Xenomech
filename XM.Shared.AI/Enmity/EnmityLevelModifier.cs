using System.Collections.Generic;

namespace XM.AI.Enmity
{
    internal class EnmityLevelModifier
    {
        private readonly Dictionary<int, float> _levelModifier = new();

        public float GetModifier(int levelDelta)
        {
            return _levelModifier[levelDelta];
        }

        public EnmityLevelModifier()
        {
            _levelModifier[-1] = 13.333f;
            _levelModifier[1] = 13.333f;
            _levelModifier[2] = 11.429f;
            _levelModifier[3] = 11.429f;
            _levelModifier[4] = 10.000f;
            _levelModifier[5] = 8.889f;
            _levelModifier[6] = 8.889f;
            _levelModifier[7] = 8.000f;
            _levelModifier[8] = 8.000f;
            _levelModifier[9] = 7.273f;
            _levelModifier[10] = 6.667f;
            _levelModifier[11] = 6.667f;
            _levelModifier[12] = 6.154f;
            _levelModifier[13] = 6.154f;
            _levelModifier[14] = 5.714f;
            _levelModifier[15] = 5.333f;
            _levelModifier[16] = 5.333f;
            _levelModifier[17] = 5.000f;
            _levelModifier[18] = 4.706f;
            _levelModifier[19] = 4.706f;
            _levelModifier[20] = 4.444f;
            _levelModifier[21] = 4.444f;
            _levelModifier[22] = 4.211f;
            _levelModifier[23] = 4.000f;
            _levelModifier[24] = 4.000f;
            _levelModifier[25] = 3.810f;
            _levelModifier[26] = 3.637f;
            _levelModifier[27] = 3.637f;
            _levelModifier[28] = 3.478f;
            _levelModifier[29] = 3.478f;
            _levelModifier[30] = 3.333f;
            _levelModifier[31] = 3.200f;
            _levelModifier[32] = 3.200f;
            _levelModifier[33] = 3.077f;
            _levelModifier[34] = 2.963f;
            _levelModifier[35] = 2.963f;
            _levelModifier[36] = 2.857f;
            _levelModifier[37] = 2.857f;
            _levelModifier[38] = 2.759f;
            _levelModifier[39] = 2.667f;
            _levelModifier[40] = 2.667f;
            _levelModifier[41] = 2.581f;
            _levelModifier[42] = 2.500f;
            _levelModifier[43] = 2.500f;
            _levelModifier[44] = 2.424f;
            _levelModifier[45] = 2.424f;
            _levelModifier[46] = 2.353f;
            _levelModifier[47] = 2.286f;
            _levelModifier[48] = 2.286f;
            _levelModifier[49] = 2.222f;
            _levelModifier[50] = 2.162f;
            _levelModifier[51] = 2.162f;
            _levelModifier[52] = 2.105f;
            _levelModifier[53] = 2.105f;
            _levelModifier[54] = 2.051f;
            _levelModifier[55] = 2.000f;
            _levelModifier[56] = 2.000f;
            _levelModifier[57] = 1.951f;
            _levelModifier[58] = 1.905f;
            _levelModifier[59] = 1.905f;
            _levelModifier[60] = 1.860f;
            _levelModifier[61] = 1.860f;
            _levelModifier[62] = 1.818f;
            _levelModifier[63] = 1.778f;
            _levelModifier[64] = 1.778f;
            _levelModifier[65] = 1.739f;
            _levelModifier[66] = 1.702f;
            _levelModifier[67] = 1.702f;
            _levelModifier[68] = 1.667f;
            _levelModifier[69] = 1.667f;
            _levelModifier[70] = 1.633f;
            _levelModifier[71] = 1.600f;
            _levelModifier[72] = 1.600f;
            _levelModifier[73] = 1.569f;
            _levelModifier[74] = 1.538f;
            _levelModifier[75] = 1.538f;
        }
    }
}
