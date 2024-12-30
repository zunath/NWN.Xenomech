using Anvil.Services;
using NLog;
using System;
using System.Collections.Generic;
using Anvil.API.Events;
using XM.Core;

namespace XM.Inventory.Loot
{
    [ServiceBinding(typeof(LootService))]
    internal class LootService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, LootTable> _lootTables;

        private const float CorpseLifespanSeconds = 360f;
        private const string CorpseBodyVariable = "CORPSE_BODY";
        private const string CorpseCopyItemVariable = "CORPSE_ITEM_COPY";

        [Inject]
        public IList<ILootTableDefinition> Definitions { get; set; }

        public LootService(EventService es)
        {
            

            _lootTables = new Dictionary<string, LootTable>();

            HookEvents();
        }

        private void HookEvents()
        {
            
        }

        public void Init()
        {
            LoadLootTables();
        }

        private void LoadLootTables()
        {
            foreach (var definition in Definitions)
            {
                var tables = definition.BuildLootTables();

                foreach (var (key, table) in tables)
                {
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        _logger.Error($"Loot table {key} has an invalid key. Values must not be null or white space.");
                        continue;
                    }

                    if (_lootTables.ContainsKey(key))
                    {
                        _logger.Error($"Loot table {key} has already been registered. Please make sure all spawn tables use a unique ID.");
                        continue;
                    }

                    _lootTables[key] = table;
                }
            }
        }


        /// <summary>
        /// Retrieves the name of the loot table, the chance to spawn an item, and the number of attempts
        /// by a given comma delimited loot table string.
        /// </summary>
        /// <param name="delimitedString">The comma delimited string </param>
        /// <returns>The table name, the percent chance, and the number of attempts</returns>
        private (string, int, int) ParseLootTableArguments(string delimitedString)
        {
            var data = delimitedString.Split(',');
            var tableName = data[0].Trim();
            var chance = 100;
            var attempts = 1;

            // Second argument: Chance to spawn
            if (data.Length > 1)
            {
                data[1] = data[1].Trim();
                if (!int.TryParse(data[1], out chance))
                {
                    _logger.Error($"Loot Table with arguments '{delimitedString}', 'Chance' variable could not be processed. Must be an integer.");
                }
            }

            // Third argument: Attempts to pull from loot table
            if (data.Length > 2)
            {
                data[2] = data[2].Trim();
                if (!int.TryParse(data[2], out attempts))
                {
                    _logger.Error($"Loot Table with arguments '{delimitedString}', 'Attempts' variable could not be processed. Must be an integer.");
                }
            }

            // Guards against bad data from builder.
            if (chance > 100)
                chance = 100;

            if (attempts <= 0)
                attempts = 1;

            return (tableName, chance, attempts);
        }


        private List<uint> SpawnLoot(uint source, uint receiver, string lootTableName, int chance, int attempts)
        {
            var creditFinderLevel = GetLocalInt(source, "CREDITFINDER_LEVEL");
            var creditPercentIncrease = creditFinderLevel * 0.2f;
            var rareBonusChance = GetLocalInt(source, "RARE_BONUS_CHANCE");

            var lootList = new List<uint>();
            var table = GetLootTableByName(lootTableName);
            if (rareBonusChance > 0 && table.IsRare)
            {
                chance += rareBonusChance;
            }
            for (int x = 1; x <= attempts; x++)
            {
                if (XMRandom.D100(1) > chance) continue;

                var item = table.GetRandomItem(rareBonusChance);
                var quantity = XMRandom.Next(item.MaxQuantity) + 1;

                // CreditFinder perk - Increase the quantity of gold found.
                if (item.Resref == "nw_it_gold001")
                {
                    quantity += (int)(quantity * creditPercentIncrease);
                }

                var loot = CreateItemOnObject(item.Resref, receiver, quantity);
                lootList.Add(loot);

                item.OnSpawn?.Invoke(loot);
            }

            return lootList;
        }

        /// <summary>
        /// Retrieves a loot table by its unique name.
        /// If name is not registered, an exception will be raised.
        /// </summary>
        /// <param name="name">The name of the loot table to retrieve.</param>
        /// <returns>A loot table matching the specified name.</returns>
        private LootTable GetLootTableByName(string name)
        {
            if (!_lootTables.ContainsKey(name))
                throw new Exception($"Loot table '{name}' is not registered. Did you enter the right name?");

            return _lootTables[name];
        }

        /// <summary>
        /// Returns all of the loot table details found on a creature's local variables.
        /// </summary>
        /// <param name="creature">The creature to search.</param>
        /// <param name="lootTablePrefix">The prefix of the loot tables to look for.</param>
        /// <returns>A list of loot table details.</returns>
        private static IEnumerable<string> GetLootTableDetails(uint creature, string lootTablePrefix)
        {
            var lootTables = new List<string>();

            int index = 1;
            var localVariableName = lootTablePrefix + index;
            var localVariable = GetLocalString(creature, localVariableName);

            while (!string.IsNullOrWhiteSpace(localVariable))
            {
                localVariable = GetLocalString(creature, localVariableName);
                if (string.IsNullOrWhiteSpace(localVariable)) break;

                lootTables.Add(localVariableName);

                index++;
                localVariableName = lootTablePrefix + index;
            }

            return lootTables;
        }
    }
}
