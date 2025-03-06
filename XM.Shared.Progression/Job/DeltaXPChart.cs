using System.Collections.Generic;
using Anvil.Services;

namespace XM.Progression.Job
{
    [ServiceBinding(typeof(DeltaXPChart))]
    public class DeltaXPChart: Dictionary<int, int>
    {
        private const int Max = 6;
        private const int Min = -4;

        public DeltaXPChart()
        {
            this[6] = 450;
            this[5] = 350;
            this[4] = 200;
            this[3] = 160;
            this[2] = 140;
            this[1] = 120;
            this[0] = 200;
            this[-1] = 180;
            this[-2] = 160;
            this[-3] = 140;
            this[-4] = 130;
        }

        public int GetBaseXP(int delta)
        {
            if (delta > Max)
                delta = Max;

            return delta < Min 
                ? 0 
                : this[delta];
        }
    }
}
