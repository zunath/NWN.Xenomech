using System.Collections.Generic;
using Anvil.Services;

namespace XM.Progression.Job
{
    [ServiceBinding(typeof(XPChart))]
    public class XPChart: Dictionary<int, int>
    {
        public XPChart()
        {
            this[0] = 550;
            this[1] = 825;
            this[2] = 1100;
            this[3] = 1375;
            this[4] = 1650;
            this[5] = 1925;
            this[6] = 2200;
            this[7] = 2420;
            this[8] = 2640;
            this[9] = 2860;
            this[10] = 3080;
            this[11] = 4200;
            this[12] = 4480;
            this[13] = 4760;
            this[14] = 5040;
            this[15] = 5320;
            this[16] = 5600;
            this[17] = 5880;
            this[18] = 6160;
            this[19] = 6440;
            this[20] = 6720;
            this[21] = 8500;
            this[22] = 8670;
            this[23] = 8840;
            this[24] = 9010;
            this[25] = 9180;
            this[26] = 9350;
            this[27] = 9520;
            this[28] = 9690;
            this[29] = 9860;
            this[30] = 10030;
            this[31] = 10200;
            this[32] = 10370;
            this[33] = 10540;
            this[34] = 10710;
            this[35] = 10880;
            this[36] = 11050;
            this[37] = 11220;
            this[38] = 11390;
            this[39] = 11560;
            this[40] = 11730;
            this[41] = 14000;
            this[42] = 14200;
            this[43] = 14400;
            this[44] = 14600;
            this[45] = 14800;
            this[46] = 15000;
            this[47] = 15200;
            this[48] = 15400;
            this[49] = 16000;
            this[50] = 18400;
            this[51] = 24960;
            this[52] = 27840;
            this[53] = 30720;
            this[54] = 33600;
            this[55] = 36480;
            this[56] = 39360;
            this[57] = 42240;
            this[58] = 45120;
            this[59] = 48000;
            this[60] = 51600;
            this[61] = 55200;
            this[62] = 58800;
            this[63] = 62400;
            this[64] = 66000;
            this[65] = 69600;
            this[66] = 73200;
            this[67] = 76800;
            this[68] = 81600;
            this[69] = 86400;
            this[70] = 91200;
            this[71] = 108000;
            this[72] = 113400;
            this[73] = 118800;
            this[74] = 120150;
            this[75] = 121500;
            this[76] = 122850;
            this[77] = 124200;
            this[78] = 125550;
            this[79] = 126900;
            this[80] = 128250;
            this[81] = 144000;
            this[82] = 145500;
            this[83] = 147000;
            this[84] = 148500;
            this[85] = 150000;
            this[86] = 151500;
            this[87] = 153000;
            this[88] = 154500;
            this[89] = 156000;
            this[90] = 159000;
            this[91] = 216000;
            this[92] = 220000;
            this[93] = 224000;
            this[94] = 228000;
            this[95] = 232000;
            this[96] = 236000;
            this[97] = 240000;
            this[98] = 260000;
            this[99] = 280000;
            this[100] = 400000;
        }
    }
}
