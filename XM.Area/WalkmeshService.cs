using System;
using Anvil.Services;
using XM.API.BaseTypes;

namespace XM.Area
{
    [ServiceBinding(typeof(WalkmeshService))]
    public class WalkmeshService
    {

        public Location GetRandomLocation(uint area)
        {
            throw new NotImplementedException();
        }
    }
}
