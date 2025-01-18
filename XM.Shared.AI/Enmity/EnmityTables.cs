using System.Collections.Generic;

namespace XM.AI.Enmity
{
    internal class EnmityTables
    {
        private readonly Dictionary<int, float> _ceDamageModifiers = new();
        private readonly Dictionary<int, float> _veDamageModifiers = new();

        private readonly Dictionary<int, float> _ceHealingModifiers = new();
        private readonly Dictionary<int, float> _veHealingModifiers = new();

        public float GetCEDamageModifier(int level)
        {
            return _ceDamageModifiers[level];
        }

        public float GetVEDamageModifier(int level)
        {
            return _veDamageModifiers[level];
        }

        public float GetCEHealingModifier(int level)
        {
            return _ceHealingModifiers[level];
        }

        public float GetVEHealingModifier(int level)
        {
            return _veHealingModifiers[level];
        }

        public EnmityTables()
        {
           LoadCEDamageTable();
           LoadVEDamageTable();
           LoadCEHealingTable();
           LoadVEHealingTable();
        }

        private void LoadCEDamageTable()
        {
            _ceDamageModifiers[-1] = 13.333f;
            _ceDamageModifiers[0] = 13.333f;
            _ceDamageModifiers[1] = 13.333f;
            _ceDamageModifiers[2] = 11.429f;
            _ceDamageModifiers[3] = 11.429f;
            _ceDamageModifiers[4] = 10.000f;
            _ceDamageModifiers[5] = 8.889f;
            _ceDamageModifiers[6] = 8.889f;
            _ceDamageModifiers[7] = 8.000f;
            _ceDamageModifiers[8] = 8.000f;
            _ceDamageModifiers[9] = 7.273f;
            _ceDamageModifiers[10] = 6.667f;
            _ceDamageModifiers[11] = 6.667f;
            _ceDamageModifiers[12] = 6.154f;
            _ceDamageModifiers[13] = 6.154f;
            _ceDamageModifiers[14] = 5.714f;
            _ceDamageModifiers[15] = 5.333f;
            _ceDamageModifiers[16] = 5.333f;
            _ceDamageModifiers[17] = 5.000f;
            _ceDamageModifiers[18] = 4.706f;
            _ceDamageModifiers[19] = 4.706f;
            _ceDamageModifiers[20] = 4.444f;
            _ceDamageModifiers[21] = 4.444f;
            _ceDamageModifiers[22] = 4.211f;
            _ceDamageModifiers[23] = 4.000f;
            _ceDamageModifiers[24] = 4.000f;
            _ceDamageModifiers[25] = 3.810f;
            _ceDamageModifiers[26] = 3.637f;
            _ceDamageModifiers[27] = 3.637f;
            _ceDamageModifiers[28] = 3.478f;
            _ceDamageModifiers[29] = 3.478f;
            _ceDamageModifiers[30] = 3.333f;
            _ceDamageModifiers[31] = 3.200f;
            _ceDamageModifiers[32] = 3.200f;
            _ceDamageModifiers[33] = 3.077f;
            _ceDamageModifiers[34] = 2.963f;
            _ceDamageModifiers[35] = 2.963f;
            _ceDamageModifiers[36] = 2.857f;
            _ceDamageModifiers[37] = 2.857f;
            _ceDamageModifiers[38] = 2.759f;
            _ceDamageModifiers[39] = 2.667f;
            _ceDamageModifiers[40] = 2.667f;
            _ceDamageModifiers[41] = 2.581f;
            _ceDamageModifiers[42] = 2.500f;
            _ceDamageModifiers[43] = 2.500f;
            _ceDamageModifiers[44] = 2.424f;
            _ceDamageModifiers[45] = 2.424f;
            _ceDamageModifiers[46] = 2.353f;
            _ceDamageModifiers[47] = 2.286f;
            _ceDamageModifiers[48] = 2.286f;
            _ceDamageModifiers[49] = 2.222f;
            _ceDamageModifiers[50] = 2.162f;
            _ceDamageModifiers[51] = 2.162f;
            _ceDamageModifiers[52] = 2.105f;
            _ceDamageModifiers[53] = 2.105f;
            _ceDamageModifiers[54] = 2.051f;
            _ceDamageModifiers[55] = 2.000f;
            _ceDamageModifiers[56] = 2.000f;
            _ceDamageModifiers[57] = 1.951f;
            _ceDamageModifiers[58] = 1.905f;
            _ceDamageModifiers[59] = 1.905f;
            _ceDamageModifiers[60] = 1.860f;
            _ceDamageModifiers[61] = 1.860f;
            _ceDamageModifiers[62] = 1.818f;
            _ceDamageModifiers[63] = 1.778f;
            _ceDamageModifiers[64] = 1.778f;
            _ceDamageModifiers[65] = 1.739f;
            _ceDamageModifiers[66] = 1.702f;
            _ceDamageModifiers[67] = 1.702f;
            _ceDamageModifiers[68] = 1.667f;
            _ceDamageModifiers[69] = 1.667f;
            _ceDamageModifiers[70] = 1.633f;
            _ceDamageModifiers[71] = 1.600f;
            _ceDamageModifiers[72] = 1.600f;
            _ceDamageModifiers[73] = 1.569f;
            _ceDamageModifiers[74] = 1.538f;
            _ceDamageModifiers[75] = 1.538f;
        }

        private void LoadVEDamageTable()
        {
            _veDamageModifiers[-1] = 40f;
            _veDamageModifiers[0] = 40f;
            _veDamageModifiers[1] = 40f;
            _veDamageModifiers[2] = 34.29f;
            _veDamageModifiers[3] = 34.29f;
            _veDamageModifiers[4] = 30f;
            _veDamageModifiers[5] = 26.67f;
            _veDamageModifiers[6] = 26.67f;
            _veDamageModifiers[7] = 24f;
            _veDamageModifiers[8] = 24f;
            _veDamageModifiers[9] = 21.82f;
            _veDamageModifiers[10] = 20f;
            _veDamageModifiers[11] = 20f;
            _veDamageModifiers[12] = 18.46f;
            _veDamageModifiers[13] = 18.46f;
            _veDamageModifiers[14] = 17.14f;
            _veDamageModifiers[15] = 16f;
            _veDamageModifiers[16] = 16f;
            _veDamageModifiers[17] = 15f;
            _veDamageModifiers[18] = 14.12f;
            _veDamageModifiers[19] = 14.12f;
            _veDamageModifiers[20] = 13.33f;
            _veDamageModifiers[21] = 13.33f;
            _veDamageModifiers[22] = 12.63f;
            _veDamageModifiers[23] = 12f;
            _veDamageModifiers[24] = 12f;
            _veDamageModifiers[25] = 11.43f;
            _veDamageModifiers[26] = 10.91f;
            _veDamageModifiers[27] = 10.91f;
            _veDamageModifiers[28] = 10.43f;
            _veDamageModifiers[29] = 10.43f;
            _veDamageModifiers[30] = 10f;
            _veDamageModifiers[31] = 9.6f;
            _veDamageModifiers[32] = 9.6f;
            _veDamageModifiers[33] = 9.23f;
            _veDamageModifiers[34] = 8.89f;
            _veDamageModifiers[35] = 8.89f;
            _veDamageModifiers[36] = 8.57f;
            _veDamageModifiers[37] = 8.57f;
            _veDamageModifiers[38] = 8.28f;
            _veDamageModifiers[39] = 8f;
            _veDamageModifiers[40] = 8f;
            _veDamageModifiers[41] = 7.74f;
            _veDamageModifiers[42] = 7.5f;
            _veDamageModifiers[43] = 7.5f;
            _veDamageModifiers[44] = 7.27f;
            _veDamageModifiers[45] = 7.27f;
            _veDamageModifiers[46] = 7.06f;
            _veDamageModifiers[47] = 6.86f;
            _veDamageModifiers[48] = 6.86f;
            _veDamageModifiers[49] = 6.67f;
            _veDamageModifiers[50] = 6.49f;
            _veDamageModifiers[51] = 6.49f;
            _veDamageModifiers[52] = 6.32f;
            _veDamageModifiers[53] = 6.32f;
            _veDamageModifiers[54] = 6.15f;
            _veDamageModifiers[55] = 6f;
            _veDamageModifiers[56] = 6f;
            _veDamageModifiers[57] = 5.85f;
            _veDamageModifiers[58] = 5.71f;
            _veDamageModifiers[59] = 5.71f;
            _veDamageModifiers[60] = 5.58f;
            _veDamageModifiers[61] = 5.58f;
            _veDamageModifiers[62] = 5.45f;
            _veDamageModifiers[63] = 5.33f;
            _veDamageModifiers[64] = 5.33f;
            _veDamageModifiers[65] = 5.22f;
            _veDamageModifiers[66] = 5.11f;
            _veDamageModifiers[67] = 5.11f;
            _veDamageModifiers[68] = 5f;
            _veDamageModifiers[69] = 5f;
            _veDamageModifiers[70] = 4.9f;
            _veDamageModifiers[71] = 4.8f;
            _veDamageModifiers[72] = 4.8f;
            _veDamageModifiers[73] = 4.71f;
            _veDamageModifiers[74] = 4.62f;
            _veDamageModifiers[75] = 4.62f;

        }

        private void LoadCEHealingTable()
        {
            _ceHealingModifiers[0] = 3.636f;
            _ceHealingModifiers[1] = 3.636f;
            _ceHealingModifiers[2] = 3.333f;
            _ceHealingModifiers[3] = 3.077f;
            _ceHealingModifiers[4] = 2.857f;
            _ceHealingModifiers[5] = 2.667f;
            _ceHealingModifiers[6] = 2.500f;
            _ceHealingModifiers[7] = 2.353f;
            _ceHealingModifiers[8] = 2.222f;
            _ceHealingModifiers[9] = 2.105f;
            _ceHealingModifiers[10] = 2.000f;
            _ceHealingModifiers[11] = 2.000f;
            _ceHealingModifiers[12] = 1.905f;
            _ceHealingModifiers[13] = 1.905f;
            _ceHealingModifiers[14] = 1.818f;
            _ceHealingModifiers[15] = 1.818f;
            _ceHealingModifiers[16] = 1.739f;
            _ceHealingModifiers[17] = 1.739f;
            _ceHealingModifiers[18] = 1.667f;
            _ceHealingModifiers[19] = 1.667f;
            _ceHealingModifiers[20] = 1.600f;
            _ceHealingModifiers[21] = 1.600f;
            _ceHealingModifiers[22] = 1.539f;
            _ceHealingModifiers[23] = 1.539f;
            _ceHealingModifiers[24] = 1.482f;
            _ceHealingModifiers[25] = 1.482f;
            _ceHealingModifiers[26] = 1.429f;
            _ceHealingModifiers[27] = 1.429f;
            _ceHealingModifiers[28] = 1.379f;
            _ceHealingModifiers[29] = 1.379f;
            _ceHealingModifiers[30] = 1.333f;
            _ceHealingModifiers[31] = 1.333f;
            _ceHealingModifiers[32] = 1.290f;
            _ceHealingModifiers[33] = 1.290f;
            _ceHealingModifiers[34] = 1.250f;
            _ceHealingModifiers[35] = 1.250f;
            _ceHealingModifiers[36] = 1.212f;
            _ceHealingModifiers[37] = 1.212f;
            _ceHealingModifiers[38] = 1.177f;
            _ceHealingModifiers[39] = 1.177f;
            _ceHealingModifiers[40] = 1.143f;
            _ceHealingModifiers[41] = 1.143f;
            _ceHealingModifiers[42] = 1.111f;
            _ceHealingModifiers[43] = 1.111f;
            _ceHealingModifiers[44] = 1.081f;
            _ceHealingModifiers[45] = 1.081f;
            _ceHealingModifiers[46] = 1.053f;
            _ceHealingModifiers[47] = 1.053f;
            _ceHealingModifiers[48] = 1.026f;
            _ceHealingModifiers[49] = 1.026f;
            _ceHealingModifiers[50] = 1.000f;
            _ceHealingModifiers[51] = 1.000f;
            _ceHealingModifiers[52] = 0.976f;
            _ceHealingModifiers[53] = 0.976f;
            _ceHealingModifiers[54] = 0.952f;
            _ceHealingModifiers[55] = 0.930f;
            _ceHealingModifiers[56] = 0.930f;
            _ceHealingModifiers[57] = 0.909f;
            _ceHealingModifiers[58] = 0.909f;
            _ceHealingModifiers[59] = 0.889f;
            _ceHealingModifiers[60] = 0.870f;
            _ceHealingModifiers[61] = 0.870f;
            _ceHealingModifiers[62] = 0.851f;
            _ceHealingModifiers[63] = 0.851f;
            _ceHealingModifiers[64] = 0.833f;
            _ceHealingModifiers[65] = 0.816f;
            _ceHealingModifiers[66] = 0.816f;
            _ceHealingModifiers[67] = 0.800f;
            _ceHealingModifiers[68] = 0.800f;
            _ceHealingModifiers[69] = 0.784f;
            _ceHealingModifiers[70] = 0.769f;
            _ceHealingModifiers[71] = 0.769f;
            _ceHealingModifiers[72] = 0.755f;
            _ceHealingModifiers[73] = 0.755f;
            _ceHealingModifiers[74] = 0.741f;
            _ceHealingModifiers[75] = 0.727f;

        }

        private void LoadVEHealingTable()
        {
            _veHealingModifiers[0] = 21.82f;
            _veHealingModifiers[1] = 21.82f;
            _veHealingModifiers[2] = 20.00f;
            _veHealingModifiers[3] = 18.46f;
            _veHealingModifiers[4] = 17.14f;
            _veHealingModifiers[5] = 16.00f;
            _veHealingModifiers[6] = 15.00f;
            _veHealingModifiers[7] = 14.12f;
            _veHealingModifiers[8] = 13.33f;
            _veHealingModifiers[9] = 12.63f;
            _veHealingModifiers[10] = 12.00f;
            _veHealingModifiers[11] = 12.00f;
            _veHealingModifiers[12] = 11.43f;
            _veHealingModifiers[13] = 11.43f;
            _veHealingModifiers[14] = 10.91f;
            _veHealingModifiers[15] = 10.91f;
            _veHealingModifiers[16] = 10.43f;
            _veHealingModifiers[17] = 10.43f;
            _veHealingModifiers[18] = 10.00f;
            _veHealingModifiers[19] = 10.00f;
            _veHealingModifiers[20] = 9.60f;
            _veHealingModifiers[21] = 9.60f;
            _veHealingModifiers[22] = 9.23f;
            _veHealingModifiers[23] = 9.23f;
            _veHealingModifiers[24] = 8.89f;
            _veHealingModifiers[25] = 8.89f;
            _veHealingModifiers[26] = 8.57f;
            _veHealingModifiers[27] = 8.57f;
            _veHealingModifiers[28] = 8.28f;
            _veHealingModifiers[29] = 8.28f;
            _veHealingModifiers[30] = 8.00f;
            _veHealingModifiers[31] = 8.00f;
            _veHealingModifiers[32] = 7.74f;
            _veHealingModifiers[33] = 7.74f;
            _veHealingModifiers[34] = 7.50f;
            _veHealingModifiers[35] = 7.50f;
            _veHealingModifiers[36] = 7.27f;
            _veHealingModifiers[37] = 7.27f;
            _veHealingModifiers[38] = 7.06f;
            _veHealingModifiers[39] = 7.06f;
            _veHealingModifiers[40] = 6.86f;
            _veHealingModifiers[41] = 6.86f;
            _veHealingModifiers[42] = 6.67f;
            _veHealingModifiers[43] = 6.67f;
            _veHealingModifiers[44] = 6.49f;
            _veHealingModifiers[45] = 6.49f;
            _veHealingModifiers[46] = 6.32f;
            _veHealingModifiers[47] = 6.32f;
            _veHealingModifiers[48] = 6.15f;
            _veHealingModifiers[49] = 6.15f;
            _veHealingModifiers[50] = 6.00f;
            _veHealingModifiers[51] = 6.00f;
            _veHealingModifiers[52] = 5.85f;
            _veHealingModifiers[53] = 5.85f;
            _veHealingModifiers[54] = 5.71f;
            _veHealingModifiers[55] = 5.58f;
            _veHealingModifiers[56] = 5.58f;
            _veHealingModifiers[57] = 5.45f;
            _veHealingModifiers[58] = 5.45f;
            _veHealingModifiers[59] = 5.33f;
            _veHealingModifiers[60] = 5.22f;
            _veHealingModifiers[61] = 5.22f;
            _veHealingModifiers[62] = 5.11f;
            _veHealingModifiers[63] = 5.11f;
            _veHealingModifiers[64] = 5.00f;
            _veHealingModifiers[65] = 4.90f;
            _veHealingModifiers[66] = 4.90f;
            _veHealingModifiers[67] = 4.80f;
            _veHealingModifiers[68] = 4.80f;
            _veHealingModifiers[69] = 4.71f;
            _veHealingModifiers[70] = 4.62f;
            _veHealingModifiers[71] = 4.62f;
            _veHealingModifiers[72] = 4.53f;
            _veHealingModifiers[73] = 4.53f;
            _veHealingModifiers[74] = 4.44f;
            _veHealingModifiers[75] = 4.36f;

        }
    }
}
