namespace XM.AI.Enmity
{
    internal class CreatureEnmity
    {
        private const int EnmityCap = 30000;

        private int _cumulativeEnmity;
        private int _volatileEnmity;

        public CreatureEnmity()
        {
            CumulativeEnmity = 1;
            VolatileEnmity = 0;
        }

        public int CumulativeEnmity
        {
            get => _cumulativeEnmity;
            set
            {
                _cumulativeEnmity = value;
                if (_cumulativeEnmity > EnmityCap)
                    _cumulativeEnmity = EnmityCap;
                else if (_cumulativeEnmity < 1)
                    _cumulativeEnmity = 1;
            }
        }

        public int VolatileEnmity
        {
            get => _volatileEnmity;
            set
            {
                _volatileEnmity = value;
                if (_volatileEnmity > EnmityCap)
                    _volatileEnmity = EnmityCap;
                else if (_volatileEnmity < 0)
                    _volatileEnmity = 0;
            }
        }

        public int TotalEnmity => CumulativeEnmity + VolatileEnmity;
    }
}
