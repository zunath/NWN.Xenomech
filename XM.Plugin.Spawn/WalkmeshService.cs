﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Anvil.API;
using Anvil.Services;
using NLog;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;
using XM.Shared.Core.EventManagement;

namespace XM.Spawn
{
    [ServiceBinding(typeof(WalkmeshService))]
    public class WalkmeshService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<uint, List<uint>> _noSpawnZoneTriggers = new();
        private Dictionary<string, List<Vector3>> _walkmeshesByArea = new();
        private const int AreaBakeStep = 2;
        private bool _bakingRan;

        private readonly DBService _db;
        private readonly XMEventService _event;

        public WalkmeshService(DBService db, XMEventService @event)
        {
            _db = db;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnLoad>(OnModuleLoad);
            _event.Subscribe<XMEvent.OnModuleContentChanged>(OnModuleContentChanged);
        }

        private void OnModuleLoad(uint objectSelf)
        {
            if (_bakingRan)
                return;

            var serverConfig = _db.Get<ModuleCache>(ModuleCache.CacheIdName);
            _walkmeshesByArea = serverConfig.WalkmeshesByArea;
            _logger.Info($"Loaded {_walkmeshesByArea.Count} area walkmeshes.");
        }

        private void OnModuleContentChanged(uint objectSelf)
        {
            LoadWalkmeshes();
        }

        private void LoadWalkmeshes()
        {
            StoreNoSpawnZoneTriggers();

            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                BakeArea(area);
            }

            var serverConfig = _db.Get<ModuleCache>(ModuleCache.CacheIdName) ?? new ModuleCache();
            serverConfig.WalkmeshesByArea = _walkmeshesByArea;
            _db.Set(serverConfig);

            _bakingRan = true;
            _logger.Info($"Baked {_walkmeshesByArea.Count} areas.");
        }

        /// <summary>
        /// When the module loads, find all of the "no spawn zone" triggers that have been hand placed by a builder.
        /// These indicate that walkmesh locations within the trigger are not valid and will be excluded from the list.
        /// </summary>
        private void StoreNoSpawnZoneTriggers()
        {
            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                for (var obj = GetFirstObjectInArea(area); GetIsObjectValid(obj); obj = GetNextObjectInArea(area))
                {
                    var resref = GetResRef(obj);

                    if (resref != "anti_spawn_trigg")
                        continue;

                    if (!_noSpawnZoneTriggers.ContainsKey(area))
                        _noSpawnZoneTriggers[area] = new List<uint>();

                    _noSpawnZoneTriggers[area].Add(obj);
                }
            }
        }

        // Area baking process
        // Run through and look for valid locations for later use by the spawn system.
        // Each tile is 10x10 meters. The "step" value in the config table determines how many meters we progress before checking for a valid location.
        private void BakeArea(uint area)
        {
            var resref = GetResRef(area);
            _walkmeshesByArea[resref] = new List<Vector3>();

            const float MinDistance = 6.0f;
            var width = GetAreaSize(AreaDimensionType.Width, area);
            var height = GetAreaSize(AreaDimensionType.Height, area);

            var arraySizeX = width * (10 / AreaBakeStep);
            var arraySizeY = height * (10 / AreaBakeStep);

            for (var x = 0; x < arraySizeX; x++)
            {
                for (var y = 0; y < arraySizeY; y++)
                {
                    var checkPosition = Vector(x * AreaBakeStep, y * AreaBakeStep);
                    var checkLocation = Location(area, checkPosition, 0.0f);
                    var material = GetSurfaceMaterial(checkLocation);
                    var isWalkable = Convert.ToInt32(Get2DAString("surfacemat", "Walk", material)) == 1;

                    // Location is not walkable if another object exists nearby.
                    var nearest = GetNearestObjectToLocation( ObjectType.Creature | ObjectType.Door | ObjectType.Placeable | ObjectType.Trigger, checkLocation);
                    var distance = GetDistanceBetweenLocations(checkLocation, GetLocation(nearest));
                    if (GetIsObjectValid(nearest) && distance <= MinDistance)
                    {
                        isWalkable = false;
                    }

                    // Location is not walkable if it's contained within any "no spawn zone" triggers.
                    if (_noSpawnZoneTriggers.ContainsKey(area))
                    {
                        foreach (var trigger in _noSpawnZoneTriggers[area])
                        {
                            if (ObjectPlugin.GetPositionIsInTrigger(trigger, checkPosition))
                            {
                                isWalkable = false;
                                break;
                            }
                        }
                    }

                    if (isWalkable)
                    {
                        var position = new Vector3(x * AreaBakeStep,
                            y * AreaBakeStep,
                            GetGroundHeight(checkLocation));
                        _walkmeshesByArea[resref].Add(position);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a random location from the walkmeshes in an area.
        /// </summary>
        /// <param name="area">The area to retrieve a random location for.</param>
        /// <returns>A random location within an area.</returns>
        public Location GetRandomLocation(uint area)
        {
            var resref = GetResRef(area);
            if (!_walkmeshesByArea.ContainsKey(resref))
                return Location(area, Vector3.Zero, 0.0f);

            var count = _walkmeshesByArea[resref].Count;
            if (count <= 0)
                return Location(area, Vector3.Zero, 0.0f);

            var index = XMRandom.Next(count);
            var position = _walkmeshesByArea[resref][index];
            return Location(area, position, 0.0f);
        }

    }
}
