using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Caching;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using RestEventType = XM.Shared.API.Constants.RestEventType;

namespace XM.Plugin.Area
{
    [ServiceBinding(typeof(PersistentLocationService))]
    internal class PersistentLocationService
    {
        private readonly XMEventService _event;
        private readonly DBService _db;
        private readonly AreaCacheService _areaCache;

        private const string DefaultStartingLocationWaypointTag = "DEFAULT_STARTING_LOCATION";
        private const string StartingAreaResref = "ooc_area";

        public PersistentLocationService(
            XMEventService @event,
            DBService db,
            AreaCacheService areaCache)
        {
            _event = @event;
            _db = db;
            _areaCache = areaCache;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<AreaEvent.OnAreaEnter>(OnAreaEnter);
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(SaveOnModuleLeave);
            _event.Subscribe<ModuleEvent.OnPlayerRest>(SaveOnPlayerRest);
        }

        private void OnAreaEnter(uint area)
        {
            var resref = GetResRef(area);
            var player = GetEnteringObject();

            // Entering the OOC area after a restart.
            // Send the player to their saved location.
            if (resref == StartingAreaResref)
            {
                LoadLocation(player);
            }
            else
            {
                SaveLocation(player);
            }
        }

        private void SaveOnModuleLeave(uint module)
        {
            var player = GetExitingObject();
            SaveLocation(player);
        }
        private void SaveOnPlayerRest(uint module)
        {
            var player = GetLastPCRested();
            var type = GetLastRestEventType();
            if (type != RestEventType.RestStarted)
                return;

            SaveLocation(player);
        }

        private void SaveLocation(uint player)
        {
            var area = GetArea(player);
            var areaResref = GetResRef(area);

            if (!GetIsPC(player))
                return;

            // Instanced areas are not eligible for saving locations.
            if (_areaCache.GetAreaByResref(areaResref) == OBJECT_INVALID)
                return;

            var position = GetPosition(player);
            var orientation = GetFacing(player);
            var playerId = PlayerId.Get(player);
            var dbPlayerLocation = _db.Get<PlayerLocation>(playerId);

            dbPlayerLocation.LocationX = position.X;
            dbPlayerLocation.LocationY = position.Y;
            dbPlayerLocation.LocationZ = position.Z;
            dbPlayerLocation.LocationOrientation = orientation;
            dbPlayerLocation.LocationAreaResref = GetResRef(area);

            _db.Set(dbPlayerLocation);
        }

        private void LoadLocation(uint player)
        {
            if (!GetIsPC(player))
                return;

            var playerId = PlayerId.Get(player);
            var dbPlayerLocation = _db.Get<PlayerLocation>(playerId);

            Location location;
            if (string.IsNullOrWhiteSpace(dbPlayerLocation.LocationAreaResref))
            {
                var waypoint  = GetWaypointByTag(DefaultStartingLocationWaypointTag);
                location = GetLocation(waypoint);
            }
            else
            {
                var area = _areaCache.GetAreaByResref(dbPlayerLocation.LocationAreaResref);
                var position = Vector(
                    dbPlayerLocation.LocationX,
                    dbPlayerLocation.LocationY,
                    dbPlayerLocation.LocationZ);
                location = Location(area, position, dbPlayerLocation.LocationOrientation);
            }

            AssignCommand(player, () =>
            {
                ClearAllActions();
                ActionJumpToLocation(location);
            });
        }
    }
}
