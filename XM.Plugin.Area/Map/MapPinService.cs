using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Plugin.Area.Entity;
using XM.Shared.Core.Caching;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Area.Map
{
    [ServiceBinding(typeof(MapPinService))]
    internal class MapPinService
    {
        private struct MapPinDetails
        {
            public string Text { get; set; }
            public float PositionX { get; set; }
            public float PositionY { get; set; }
        }

        private readonly DBService _db;
        private readonly XMEventService _event;
        private readonly AreaCacheService _areaCache;

        private const string TotalMapPinsVariable = "NW_TOTAL_MAP_PINS";
        private const string PlayerMapPinsLoaded = "MAP_PINS_LOADED";

        public MapPinService(
            DBService db,
            XMEventService @event,
            AreaCacheService areaCache)
        {
            _db = db;
            _event = @event;
            _areaCache = areaCache;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnMapPinAddPinAfter>(AddMapPin);
            _event.Subscribe<NWNXEvent.OnMapPinChangePinAfter>(ChangeMapPin);
            _event.Subscribe<NWNXEvent.OnMapPinDestroyPinAfter>(DeleteMapPin);
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(LoadMapPins);
        }
        private MapPin LoadMapPin(bool getId = true, bool isDestroying = false)
        {
            return new MapPin
            {
                Id = getId ? Convert.ToInt32(EventsPlugin.GetEventData("PIN_ID")) : -1,
                Note = isDestroying ? string.Empty : EventsPlugin.GetEventData("PIN_NOTE"),
                X = isDestroying ? 0 : (float)Convert.ToDouble(EventsPlugin.GetEventData("PIN_X")),
                Y = isDestroying ? 0 : (float)Convert.ToDouble(EventsPlugin.GetEventData("PIN_Y"))
            };
        }

        private void AddMapPin(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player)) 
                return;

            var mapPin = LoadMapPin(false);
            mapPin.Id = GetNumberOfMapPins(player) + 1;

            var playerId = GetObjectUUID(player);
            var dbPlayerMap = _db.Get<PlayerMap>(playerId);
            var area = GetArea(player);
            var areaResref = GetResRef(area);

            if (!dbPlayerMap.MapPins.ContainsKey(areaResref))
                dbPlayerMap.MapPins[areaResref] = new List<MapPin>();

            dbPlayerMap.MapPins[areaResref].Add(mapPin);

            _db.Set(dbPlayerMap);
        }
        private void ChangeMapPin(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player)) 
                return;

            var mapPin = LoadMapPin();
            var playerId = GetObjectUUID(player);
            var dbPlayer = _db.Get<PlayerMap>(playerId);
            if (dbPlayer == null) return;

            var area = GetArea(player);
            var areaResref = GetResRef(area);
            if (!dbPlayer.MapPins.ContainsKey(areaResref))
                return;

            var mapPins = dbPlayer.MapPins[areaResref];
            for (int index = mapPins.Count - 1; index >= 0; index--)
            {
                if (mapPins[index].Id == mapPin.Id)
                {
                    mapPins[index].X = mapPin.X;
                    mapPins[index].Y = mapPin.Y;
                    mapPins[index].Note = mapPin.Note;
                    break;
                }
            }

            _db.Set(dbPlayer);

        }
        private void DeleteMapPin(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player)) 
                return;

            var mapPin = LoadMapPin(true, true);
            var playerId = GetObjectUUID(player);
            var dbPlayer = _db.Get<PlayerMap>(playerId);
            if (dbPlayer == null) return;

            var area = GetArea(player);
            var areaResref = GetResRef(area);
            if (!dbPlayer.MapPins.ContainsKey(areaResref))
                return;

            var mapPins = dbPlayer.MapPins[areaResref];
            for (var index = mapPins.Count - 1; index >= 0; index--)
            {
                if (mapPins[index].Id == mapPin.Id)
                {
                    mapPins.RemoveAt(index);
                    break;
                }
            }

            _db.Set(dbPlayer);
        }

        private void LoadMapPins(uint module)
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player) || GetLocalBool(player, PlayerMapPinsLoaded)) 
                return;

            var playerId = GetObjectUUID(player);
            var dbPlayer = _db.Get<PlayerMap>(playerId);

            var mapPinTuple = dbPlayer
                .MapPins
                .SelectMany(s => s.Value.Select(v => Tuple.Create(s.Key, v)))
                .OrderBy(o => o.Item2.Id);

            int pinsAdded = 0;
            foreach (var (areaResref, mapPin) in mapPinTuple)
            {
                var area = _areaCache.GetAreaByResref(areaResref);
                SetMapPin(player, mapPin.Note, mapPin.X, mapPin.Y, area);

                // Increment the count and update the ID.
                pinsAdded++;
                mapPin.Id = pinsAdded;
            }

            // Ensure this doesn't run again until the next reboot.
            SetLocalBool(player, PlayerMapPinsLoaded, true);

            // Save any changes to the IDs.
            _db.Set(dbPlayer);
        }
        private int GetNumberOfMapPins(uint player)
        {
            return GetLocalInt(player, TotalMapPinsVariable);
        }
        private void SetNumberOfMapPins(uint player, int numberOfMapPins)
        {
            SetLocalInt(player, TotalMapPinsVariable, numberOfMapPins);
        }

        private void SetMapPin(uint player, string note, float x, float y, uint area)
        {
            int numberOfMapPins = GetNumberOfMapPins(player);
            int storeAtIndex = -1;

            for (int index = 0; index < numberOfMapPins; index++)
            {
                var mapPin = GetMapPinDetails(player, index);
                if (string.IsNullOrWhiteSpace(mapPin.Text))
                {
                    storeAtIndex = index;
                    break;
                }
            }

            if (storeAtIndex == -1)
            {
                numberOfMapPins++;
                storeAtIndex = numberOfMapPins - 1;
            }

            storeAtIndex++;

            SetLocalString(player, "NW_MAP_PIN_NTRY_" + storeAtIndex, note);
            SetLocalFloat(player, "NW_MAP_PIN_XPOS_" + storeAtIndex, x);
            SetLocalFloat(player, "NW_MAP_PIN_YPOS_" + storeAtIndex, y);
            SetLocalObject(player, "NW_MAP_PIN_AREA_" + storeAtIndex, area);
            SetNumberOfMapPins(player, numberOfMapPins);
        }
        private MapPinDetails GetMapPinDetails(uint player, int index)
        {
            index++;
            MapPinDetails mapPin = new MapPinDetails()
            {
                Text = GetLocalString(player, "NW_MAP_PIN_NTRY_" + index),
                PositionX = GetLocalFloat(player, "NW_MAP_PIN_XPOS_" + index),
                PositionY = GetLocalFloat(player, "NW_MAP_PIN_YPOS_" + index),
            };

            return mapPin;
        }
    }
}
