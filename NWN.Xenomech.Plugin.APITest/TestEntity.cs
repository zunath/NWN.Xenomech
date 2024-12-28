using System;
using NWN.Xenomech.Data;

namespace NWN.Xenomech.Plugin.APITest
{
    [Serializable]
    public class TestEntity: EntityBase
    {
        public string Name { get; set; }
    }
}
