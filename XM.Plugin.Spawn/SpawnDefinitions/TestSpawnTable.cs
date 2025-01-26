using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;

namespace XM.Spawn.SpawnDefinitions
{
    [ServiceBinding(typeof(TestSpawnTable))]
    [ServiceBinding(typeof(ISpawnListDefinition))]
    internal class TestSpawnTable: ISpawnListDefinition
    {
        private readonly SpawnTableBuilder _builder = new();

        public Dictionary<string, SpawnTable> BuildSpawnTables()
        {
            TestTable();

            return _builder.Build();
        }

        private void TestTable()
        {
            _builder.Create("VISCARA_WILDLANDS", "Wildlands")
                .AddSpawn(ObjectType.Creature, "goblintest")
                .RespawnDelay(1)
                .WithFrequency(40)
                .RandomlyWalks()
                .ReturnsHome();
        }
    }
}
