using Anvil.Services;
using NLog;
using System;
using System.Collections.Generic;
using XM.Core;
using XM.API.Constants;
using XM.Core.EventManagement.CreatureEvent;

namespace XM.Inventory.Loot
{
    [ServiceBinding(typeof(LootService))]
    [ServiceBinding(typeof(ICreatureOnDeathBeforeEvent))]
    internal class LootService: IInitializable, ICreatureOnDeathBeforeEvent
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, LootTable> _lootTables;

        private const float CorpseLifespanSeconds = 360f;
        private const string CorpseBodyVariable = "CORPSE_BODY";
        private const string CorpseCopyItemVariable = "CORPSE_ITEM_COPY";

        private const string CorpseClosedScript = "corpse_closed";
        private const string CorpseDisturbedScript = "corpse_disturbed";


        [Inject]
        public IList<ILootTableDefinition> Definitions { get; set; }

        private readonly InventoryService _inventory;

        public LootService(InventoryService inventory)
        {
            _inventory = inventory;
            _lootTables = new Dictionary<string, LootTable>();
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

            _logger.Info($"Loaded {_lootTables.Count} loot tables.");
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


        public List<uint> SpawnLoot(uint source, uint receiver, string lootTableName, int chance, int attempts)
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
        /// Attempts to spawn items found in the associated loot table based on the configured variables.
        /// </summary>
        /// <param name="source">The source of the items (must contain the local variables)</param>
        /// <param name="receiver">The receiver of the items</param>
        /// <param name="lootTablePrefix">The prefix of the loot tables. In the toolset this should be numeric starting at 1.</param>
        public void SpawnLoot(uint source, uint receiver, string lootTablePrefix)
        {
            var lootTableEntries = GetLootTableDetails(source, lootTablePrefix);
            foreach (var entry in lootTableEntries)
            {
                var delimitedString = GetLocalString(source, entry);
                var (tableName, chance, attempts) = ParseLootTableArguments(delimitedString);

                SpawnLoot(source, receiver, tableName, chance, attempts);
            }
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
        private IEnumerable<string> GetLootTableDetails(uint creature, string lootTablePrefix)
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


        /// <summary>
        /// Handles creating a corpse placeable on a creature's death, copying its inventory to the placeable,
        /// and changing the name of the placeable to match the creature.
        /// </summary>
        private void ProcessCorpse()
        {
            var self = OBJECT_SELF;
            SetIsDestroyable(false);

            var area = GetArea(self);
            var position = GetPosition(self);
            var facing = GetFacing(self);
            var lootPosition = Vector(position.X, position.Y, position.Z - 0.11f);
            var spawnLocation = Location(area, lootPosition, facing);

            var container = CreateObject(ObjectType.Placeable, "corpse", spawnLocation);
            SetLocalObject(container, CorpseBodyVariable, self);
            SetName(container, $"{GetName(self)}'s Corpse");

            AssignCommand(container, () =>
            {
                var gold = GetGold(self);
                TakeGoldFromCreature(gold, self);
            });

            // Dump equipped items in container
            for (var slot = 0; slot < GeneralConstants.NumberOfInventorySlots; slot++)
            {
                if (slot == (int)InventorySlotType.CreatureArmor ||
                    slot == (int)InventorySlotType.CreatureWeaponBite ||
                    slot == (int)InventorySlotType.CreatureWeaponLeft ||
                    slot == (int)InventorySlotType.CreatureWeaponRight)
                    continue;

                var item = GetItemInSlot((InventorySlotType)slot, self);
                if (GetIsObjectValid(item) && !GetItemCursedFlag(item) && GetDroppableFlag(item))
                {
                    var copy = CopyItem(item, container, true);

                    if (slot == (int)InventorySlotType.Head ||
                        slot == (int)InventorySlotType.Chest)
                    {
                        SetLocalObject(copy, CorpseCopyItemVariable, item);
                    }
                    else
                    {
                        DestroyObject(item);
                    }
                }
            }

            for (var item = GetFirstItemInInventory(self); GetIsObjectValid(item); item = GetNextItemInInventory(self))
            {
                if (GetIsObjectValid(item) && !GetItemCursedFlag(item) && GetDroppableFlag(item))
                {
                    CopyItem(item, container, true);
                    DestroyObject(item);
                }
            }

            ScheduleCorpseCleanup(container);
        }

        private void ScheduleCorpseCleanup(uint placeable)
        {
            DelayCommand(CorpseLifespanSeconds, () =>
            {
                if (!GetIsObjectValid(placeable))
                    return;

                var body = GetLocalObject(placeable, CorpseBodyVariable);
                AssignCommand(body, () => SetIsDestroyable(true));

                for (var item = GetFirstItemInInventory(body); GetIsObjectValid(item); item = GetNextItemInInventory(body))
                {
                    DestroyObject(item);
                }
                DestroyObject(body);

                for (var item = GetFirstItemInInventory(placeable); GetIsObjectValid(item); item = GetNextItemInInventory(placeable))
                {
                    DestroyObject(item);
                }
                DestroyObject(placeable);
            });
        }

        public void CreatureOnDeathBefore()
        {
            ProcessCorpse();
        }

        /// <summary>
        /// When the loot corpse is closed, either spawn an "Extract" placeable to be used with Beast Mastery DNA extraction
        /// or remove the dead creature from the game.
        /// </summary>
        [ScriptHandler(CorpseClosedScript)]
        public void CloseCorpseContainer()
        {
            var container = OBJECT_SELF;
            var firstItem = GetFirstItemInInventory(container);
            var corpseOwner = GetLocalObject(container, CorpseBodyVariable);

            if (!GetIsObjectValid(firstItem))
            {
                DestroyObject(container);

                AssignCommand(corpseOwner, () =>
                {
                    SetIsDestroyable(true);
                });
            }
        }

        /// <summary>
        /// When a player adds an item to a corpse, return it to them.
        /// When a player removes an item from the corpse, update the connected creature's appearance if needed.
        /// </summary>
        [ScriptHandler(CorpseDisturbedScript)]
        public void DisturbCorpseContainer()
        {
            var looter = GetLastDisturbed();
            var item = GetInventoryDisturbItem();
            var type = GetInventoryDisturbType();

            AssignCommand(looter, () =>
            {
                ActionPlayAnimation(AnimationType.LoopingGetLow, 1.0f, 1.0f);
            });

            if (type == InventoryDisturbType.Added)
            {
                _inventory.ReturnItem(looter, item);
                SendMessageToPC(looter, "You cannot place items inside of corpses.");
            }
            else if (type == InventoryDisturbType.Removed)
            {
                var copy = GetLocalObject(item, CorpseCopyItemVariable);

                if (GetIsObjectValid(copy))
                {
                    DestroyObject(copy);
                }

                DeleteLocalObject(item, CorpseCopyItemVariable);
            }
        }
    }
}
