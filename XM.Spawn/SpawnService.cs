using Anvil.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using NLog;
using XM.API.Constants;
using XM.Core.EventManagement.AreaEvent;
using XM.Core.EventManagement.CreatureEvent;
using System.Numerics;
using XM.Area;
using XM.Core;
using XM.Core.EventManagement;
using XM.Core.EventManagement.XMEvent;
using AreaPlugin = XM.API.NWNX.AreaPlugin.AreaPlugin;
using CreaturePlugin = XM.API.NWNX.CreaturePlugin.CreaturePlugin;
using ObjectPlugin = XM.API.NWNX.ObjectPlugin.ObjectPlugin;

namespace XM.Spawn
{
    [ServiceBinding(typeof(SpawnService))]
    internal class SpawnService : IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public const int DespawnMinutes = 20;
        public const int DefaultRespawnMinutes = 5;

        private readonly Dictionary<Guid, SpawnDetail> _spawns = new();
        private readonly List<QueuedSpawn> _queuedSpawns = new();
        private readonly Dictionary<uint, List<QueuedSpawn>> _queuedSpawnsByArea = new();
        private readonly Dictionary<uint, DateTime> _queuedAreaDespawns = new();
        private readonly Dictionary<string, SpawnTable> _spawnTables = new();
        private readonly Dictionary<uint, List<Guid>> _allSpawnsByArea = new();
        private readonly Dictionary<uint, List<ActiveSpawn>> _activeSpawnsByArea = new();

        [Inject] public IList<ISpawnListDefinition> Definitions { get; set; }

        private readonly WalkmeshService _walkmesh;
        private readonly XMEventService _event;

        public SpawnService(WalkmeshService walkmesh, XMEventService @event)
        {
            _walkmesh = walkmesh;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<AreaEnterEvent>(OnAreaEnter);
            _event.Subscribe<AreaExitEvent>(OnAreaExit);
            _event.Subscribe<CreatureOnDeathBeforeEvent>(CreatureOnDeathBefore);
            _event.Subscribe<ServerHeartbeatEvent>(OnXMServerHeartbeat);
        }

        public void Init()
        {
            LoadSpawnTables();
            StoreSpawns();
        }

        private void LoadSpawnTables()
        {
            foreach (var definition in Definitions)
            {
                var tables = definition.BuildSpawnTables();
                foreach (var (key, table) in tables)
                {
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        _logger.Error($"Spawn table {key} has an invalid key. Values must be greater than zero.");
                        continue;
                    }

                    if (!_spawnTables.TryAdd(key, table))
                    {
                        _logger.Error(
                            $"Spawn table {key} has already been registered. Please make sure all spawn tables use a unique ID.");
                        continue;
                    }
                }
            }

            _logger.Info($"Loaded {_spawnTables.Count} spawn tables.");
        }

        /// <summary>
        /// When the module loads, spawns are located in all areas. Details about those spawns are stored into the cached data.
        /// Spawns can be hand placed creatures, waypoints, or marked as a local variable on the area.
        /// Resource spawn tables use 'RESOURCE_SPAWN_TABLE_ID' for the table name and 'RESOURCE_SPAWN_COUNT' for the number of spawns.
        /// Creature spawn tables use 'CREATURE_SPAWN_TABLE_ID' for the table name and 'CREATURE_SPAWN_COUNT' for the number of spawns.
        /// </summary>
        private void StoreSpawns()
        {
            void RegisterAreaSpawnTable(uint area, string variableName, int spawnCount)
            {
                var spawnTableId = GetLocalString(area, variableName);
                if (!string.IsNullOrWhiteSpace(spawnTableId))
                {
                    if (!_spawnTables.ContainsKey(spawnTableId))
                    {
                        _logger.Error($"Area has an invalid spawn table Id. ({spawnTableId}) is not defined. Do you have the right spawn table Id?");
                        return;
                    }

                    for (var count = 1; count <= spawnCount; count++)
                    {
                        var id = Guid.NewGuid();
                        var spawnTable = _spawnTables[spawnTableId];
                        _spawns.Add(id, new SpawnDetail
                        {
                            SpawnTableId = spawnTableId,
                            Area = area,
                            RespawnDelayMinutes = spawnTable.RespawnDelayMinutes,
                            UseRandomSpawnLocation = true
                        });

                        if (!_allSpawnsByArea.ContainsKey(area))
                            _allSpawnsByArea[area] = new List<Guid>();

                        _allSpawnsByArea[area].Add(id);
                    }
                }
            }

            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                // Process spawns within the area.
                for (var obj = GetFirstObjectInArea(area); GetIsObjectValid(obj); obj = GetNextObjectInArea(area))
                {
                    var type = GetObjectType(obj);
                    var position = GetPosition(obj);
                    var facing = GetFacing(obj);
                    var id = Guid.NewGuid();

                    // Hand-placed creature information is stored and the actual NPC is destroyed so it can be spawned by the system.
                    if (type == ObjectType.Creature)
                    {
                        // Some plot creatures use the Object Visibility service.  This relies on object references so we 
                        // should not spawn new instances of those creatures.  Just leave them as they are.
                        if (!String.IsNullOrEmpty(GetLocalString(obj, "VISIBILITY_OBJECT_ID")))
                        {
                            continue;
                        }

                        _spawns.Add(id, new SpawnDetail
                        {
                            SerializedObject = ObjectPlugin.Serialize(obj),
                            X = position.X,
                            Y = position.Y,
                            Z = position.Z,
                            Facing = facing,
                            Area = area,
                            RespawnDelayMinutes = 5
                        });

                        // Add this entry to the spawns by area cache.
                        if (!_allSpawnsByArea.ContainsKey(area))
                            _allSpawnsByArea[area] = new List<Guid>();

                        _allSpawnsByArea[area].Add(id);

                        DestroyObject(obj);
                    }
                    // Waypoints with a spawn table ID in the tag
                    else if (type == ObjectType.Waypoint)
                    {
                        var spawnTableId = GetTag(obj);
                        if (_spawnTables.ContainsKey(spawnTableId))
                        {
                            var spawnTable = _spawnTables[spawnTableId];
                            _spawns.Add(id, new SpawnDetail
                            {
                                SpawnTableId = spawnTableId,
                                X = position.X,
                                Y = position.Y,
                                Z = position.Z,
                                Facing = facing,
                                Area = area,
                                RespawnDelayMinutes = spawnTable.RespawnDelayMinutes
                            });

                            // Add this entry to the spawns by area cache.
                            if (!_allSpawnsByArea.ContainsKey(area))
                                _allSpawnsByArea[area] = new List<Guid>();

                            _allSpawnsByArea[area].Add(id);
                        }
                    }
                }

                // Resource and creature spawn tables can be placed as a local variable on the area.
                // If one is found, it will be registered.
                RegisterAreaSpawnTable(area, "RESOURCE_SPAWN_TABLE_ID", CalculateResourceSpawnCount(area));
                RegisterAreaSpawnTable(area, "CREATURE_SPAWN_TABLE_ID", CalculateCreatureSpawnCount(area));
            }
        }


        /// <summary>
        /// Determines the number of spawns to use in an area.
        /// If an int local variable 'RESOURCE_SPAWN_COUNT' is found, use that number.
        /// Otherwise the size of the area will be used to determine the count.
        /// </summary>
        /// <param name="area">The area to determine spawn counts for</param>
        /// <returns>A positive integer indicating the number of resource spawns to use in a given area.</returns>
        private int CalculateResourceSpawnCount(uint area)
        {
            var count = GetLocalInt(area, "RESOURCE_SPAWN_COUNT");

            // Found the local variable. Use that count.
            if (count > 0) return count;

            // Local variable wasn't found or was zero. 
            // Determine the count by the size of the area.
            var width = GetAreaSize(AreaDimensionType.Width, area);
            var height = GetAreaSize(AreaDimensionType.Height, area);
            var size = width * height;

            if (size <= 12)
                count = 2;
            else if (size <= 32)
                count = 6;
            else if (size <= 64)
                count = 10;
            else if (size <= 256)
                count = 25;
            else if (size <= 512)
                count = 40;
            else if (size <= 1024)
                count = 50;

            return count;
        }

        private int CalculateCreatureSpawnCount(uint area)
        {
            var count = GetLocalInt(area, "CREATURE_SPAWN_COUNT");

            // Found the local variable. Use that count.
            if (count > 0) return count;

            // Local variable wasn't found or was zero. 
            // Determine the count by the size of the area.
            var width = GetAreaSize(AreaDimensionType.Width, area);
            var height = GetAreaSize(AreaDimensionType.Height, area);
            var size = width * height;

            if (size <= 12)
                count = 3;
            else if (size <= 32)
                count = 6;
            else if (size <= 64)
                count = 14;
            else if (size <= 256)
                count = 20;
            else if (size <= 512)
                count = 35;
            else if (size <= 1024)
                count = 45;

            return count;
        }

        private void SpawnArea()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) && !GetIsDM(player)) return;

            var area = OBJECT_SELF;

            // Area isn't registered. Could be an instanced area? No need to spawn.
            if (!_allSpawnsByArea.ContainsKey(area)) return;

            if (!_activeSpawnsByArea.ContainsKey(area))
                _activeSpawnsByArea[area] = new List<ActiveSpawn>();

            if (!_queuedSpawnsByArea.ContainsKey(area))
                _queuedSpawnsByArea[area] = new List<QueuedSpawn>();

            var activeSpawns = _activeSpawnsByArea[area];
            var queuedSpawns = _queuedSpawnsByArea[area];

            // Spawns are currently active for this area. No need to spawn.
            if (activeSpawns.Count > 0 || queuedSpawns.Count > 0) return;

            var now = DateTime.UtcNow;
            // No spawns are active. Spawn the area.
            foreach (var spawn in _allSpawnsByArea[area])
            {
                CreateQueuedSpawn(spawn, now);
            }
        }

        private void OnAreaEnter()
        {
            SpawnArea();
        }

        private void OnAreaExit()
        {
            QueueDespawnArea();
        }

        private void QueueDespawnArea()
        {
            var player = GetExitingObject();
            if (!GetIsPC(player) && !GetIsDM(player)) return;

            var area = OBJECT_SELF;
            var playerCount = AreaPlugin.GetNumberOfPlayersInArea(area);
            if (playerCount > 0) return;

            var now = DateTime.UtcNow;
            _queuedAreaDespawns[area] = now.AddMinutes(DespawnMinutes);
        }

        /// <summary>
        /// Creates a queued spawn record which is picked up by the processor.
        /// The spawn object will be created when the respawn time has passed.
        /// </summary>
        /// <param name="spawnDetailId">The ID of the spawn detail.</param>
        /// <param name="respawnTime">The time the spawn will be created.</param>
        private void CreateQueuedSpawn(Guid spawnDetailId, DateTime respawnTime)
        {
            var queuedSpawn = new QueuedSpawn
            {
                RespawnTime = respawnTime,
                SpawnDetailId = spawnDetailId
            };
            _queuedSpawns.Add(queuedSpawn);

            var spawnDetail = _spawns[spawnDetailId];
            if (!_queuedSpawnsByArea.ContainsKey(spawnDetail.Area))
                _queuedSpawnsByArea[spawnDetail.Area] = new List<QueuedSpawn>();

            _queuedSpawnsByArea[spawnDetail.Area].Add(queuedSpawn);
        }

        /// <summary>
        /// Removes a queued spawn.
        /// </summary>
        /// <param name="queuedSpawn">The queued spawn to remove.</param>
        private void RemoveQueuedSpawn(QueuedSpawn queuedSpawn)
        {
            var spawnDetail = _spawns[queuedSpawn.SpawnDetailId];
            _queuedSpawns.Remove(queuedSpawn);
            _queuedSpawnsByArea[spawnDetail.Area].Remove(queuedSpawn);
        }

        private void CreatureOnDeathBefore()
        {
            QueueRespawn();
        }

        private void QueueRespawn()
        {
            var creature = OBJECT_SELF;
            var spawnId = GetLocalString(creature, "SPAWN_ID");
            if (string.IsNullOrWhiteSpace(spawnId)) return;
            if (GetLocalInt(creature, "RESPAWN_QUEUED") == 1) return;

            var spawnGuid = new Guid(spawnId);
            var detail = _spawns[spawnGuid];
            var respawnTime = DateTime.UtcNow.AddMinutes(detail.RespawnDelayMinutes);

            CreateQueuedSpawn(spawnGuid, respawnTime);
            SetLocalInt(creature, "RESPAWN_QUEUED", 1);
        }

        private void OnXMServerHeartbeat()
        {
            ProcessQueuedSpawns();
            ProcessDespawnAreas();
        }

        /// <summary>
        /// On each module heartbeat, iterate over the list of queued spawns.
        /// If enough time has elapsed and spawn table rules are met, spawn the object and remove it from the queue.
        /// </summary>
        public void ProcessQueuedSpawns()
        {
            var now = DateTime.UtcNow;
            for (var index = _queuedSpawns.Count - 1; index >= 0; index--)
            {
                var queuedSpawn = _queuedSpawns.ElementAt(index);

                if (now > queuedSpawn.RespawnTime)
                {
                    var detail = _spawns[queuedSpawn.SpawnDetailId];
                    var spawnedObject = SpawnObject(queuedSpawn.SpawnDetailId, detail);

                    // A valid spawn wasn't found because the spawn table didn't provide a resref.
                    // Either the table is configured wrong or the requirements for that specific table weren't met.
                    // In this case, we bump the next respawn time and move to the next queued respawn.
                    if (spawnedObject == OBJECT_INVALID)
                    {
                        queuedSpawn.RespawnTime = now.AddMinutes(detail.RespawnDelayMinutes);
                        continue;
                    }

                    var activeSpawn = new ActiveSpawn
                    {
                        SpawnDetailId = queuedSpawn.SpawnDetailId,
                        SpawnObject = spawnedObject
                    };

                    _activeSpawnsByArea[detail.Area].Add(activeSpawn);
                    RemoveQueuedSpawn(queuedSpawn);
                }
            }
        }

        /// <summary>
        /// On each module heartbeat, iterate over the list of areas which are scheduled to
        /// be despawned. If players have since entered the area, remove it from the queue list.
        /// </summary>
        private void ProcessDespawnAreas()
        {
            var now = DateTime.UtcNow;
            for (var index = _queuedAreaDespawns.Count - 1; index >= 0; index--)
            {
                var (area, despawnTime) = _queuedAreaDespawns.ElementAt(index);
                // Players have entered this area. Remove it and move to the next entry.
                if (AreaPlugin.GetNumberOfPlayersInArea(area) > 0)
                {
                    _queuedAreaDespawns.Remove(area);
                    continue;
                }

                // Queued respawns are pending. These must all spawn before a despawn can occur.
                // Leave the queued despawn in place to ensure it eventually gets processed.
                if (_queuedSpawnsByArea.ContainsKey(area) &&
                    _queuedSpawnsByArea[area].Count > 0)
                {
                    continue;
                }

                if (now > despawnTime)
                {
                    // Destroy active spawned objects from the module.
                    if (_activeSpawnsByArea.ContainsKey(area))
                    {
                        foreach (var activeSpawn in _activeSpawnsByArea[area])
                        {
                            ExecuteScript("spawn_despawn", activeSpawn.SpawnObject);
                            DestroyObject(activeSpawn.SpawnObject);
                        }
                    }

                    if (!_queuedSpawnsByArea.ContainsKey(area))
                        _queuedSpawnsByArea[area] = new List<QueuedSpawn>();

                    // Removing all spawn Ids from the master queue list.
                    var spawnIds = _queuedSpawnsByArea[area].Select(s => s.SpawnDetailId);
                    _queuedSpawns.RemoveAll(x => spawnIds.Contains(x.SpawnDetailId));

                    // Remove area from the various cache collections.
                    _queuedSpawnsByArea.Remove(area);

                    if (_activeSpawnsByArea.ContainsKey(area))
                    {
                        _activeSpawnsByArea[area].Clear();
                    }

                    _queuedAreaDespawns.Remove(area);
                }
            }
        }


        /// <summary>
        /// Make plot/immortal NPCs incredibly strong to dissuade players from attacking them and messing with spawns.
        /// </summary>
        /// <param name="spawn"></param>
        private void AdjustStats(uint spawn)
        {
            if (!GetIsObjectValid(spawn) || GetObjectType(spawn) != ObjectType.Creature)
                return;

            if (GetIsPC(spawn) || GetIsDM(spawn) || GetIsDMPossessed(spawn))
                return;

            if (!GetPlotFlag(spawn) && !GetImmortal(spawn))
                return;

            CreaturePlugin.SetBaseAC(spawn, 100);
            CreaturePlugin.SetRawAbilityScore(spawn, AbilityType.Might, 100);
            CreaturePlugin.SetRawAbilityScore(spawn, AbilityType.Perception, 100);
            CreaturePlugin.SetRawAbilityScore(spawn, AbilityType.Vitality, 100);
            CreaturePlugin.SetRawAbilityScore(spawn, AbilityType.Agility, 100);
            CreaturePlugin.SetRawAbilityScore(spawn, AbilityType.Willpower, 100);
            CreaturePlugin.SetRawAbilityScore(spawn, AbilityType.Social, 100);
            CreaturePlugin.SetBaseAttackBonus(spawn, 254);
            CreaturePlugin.AddFeatByLevel(spawn, FeatType.WeaponProficiencyCreature, 1);

            AssignCommand(spawn, () => ClearAllActions());

            if (!GetIsObjectValid(GetItemInSlot(InventorySlotType.CreatureWeaponRight, spawn)))
            {
                var claw = CreateItemOnObject("npc_claw", spawn);
                AssignCommand(spawn, () =>
                {
                    ActionEquipItem(claw, InventorySlotType.CreatureWeaponRight);
                });
            }
            if (!GetIsObjectValid(GetItemInSlot(InventorySlotType.CreatureWeaponLeft, spawn)))
            {
                var claw = CreateItemOnObject("npc_claw", spawn);
                AssignCommand(spawn, () =>
                {
                    ActionEquipItem(claw, InventorySlotType.CreatureWeaponLeft);
                });
            }
        }

        /// <summary>
        /// Creates a new spawn object into its spawn area.
        /// Hand-placed objects are deserialized and added to the area.
        /// Spawn tables run their own logic to determine which object to spawn.
        /// </summary>
        /// <param name="spawnId">The ID of the spawn</param>
        /// <param name="detail">The details of the spawn</param>
        private uint SpawnObject(Guid spawnId, SpawnDetail detail)
        {
            // Hand-placed spawns are stored as a serialized string.
            // Deserialize and add it to the area.
            if (!string.IsNullOrWhiteSpace(detail.SerializedObject))
            {
                var deserialized = ObjectPlugin.Deserialize(detail.SerializedObject);
                var position = detail.UseRandomSpawnLocation ?
                    GetPositionFromLocation(_walkmesh.GetRandomLocation(detail.Area)) :
                    new Vector3(detail.X, detail.Y, detail.Z);
                ObjectPlugin.AddToArea(deserialized, detail.Area, position);

                var facing = detail.UseRandomSpawnLocation ? XMRandom.Next(360) : detail.Facing;
                AssignCommand(deserialized, () => SetFacing(facing));
                SetLocalString(deserialized, "SPAWN_ID", spawnId.ToString());
                AdjustStats(deserialized);

                return deserialized;
            }
            // Spawn tables have their own logic which must be run to determine the spawn to use.
            // Create the object at the stored location.
            else if (!string.IsNullOrWhiteSpace(detail.SpawnTableId))
            {
                var spawnTable = _spawnTables[detail.SpawnTableId];
                var spawnObject = spawnTable.GetNextSpawn();

                // It's possible that the rules of the spawn table don't have a spawn ready to be created.
                // In this case, exit early.
                if (string.IsNullOrWhiteSpace(spawnObject.Resref))
                {
                    return OBJECT_INVALID;
                }

                var position = detail.UseRandomSpawnLocation ?
                    GetPositionFromLocation(_walkmesh.GetRandomLocation(detail.Area)) :
                    new Vector3(detail.X, detail.Y, detail.Z);

                var facing = detail.UseRandomSpawnLocation ? XMRandom.Next(360) : detail.Facing;
                var location = Location(detail.Area, position, facing);

                var spawn = CreateObject(spawnObject.Type, spawnObject.Resref, location);
                SetLocalString(spawn, "SPAWN_ID", spawnId.ToString());
                AdjustStats(spawn);

                foreach (var action in spawnObject.OnSpawnActions)
                {
                    action(spawn);
                }

                ExecuteScript(EventScript.OnXMSpawnCreatedScript, spawn);

                return spawn;
            }

            return OBJECT_INVALID;
        }
    }
}
