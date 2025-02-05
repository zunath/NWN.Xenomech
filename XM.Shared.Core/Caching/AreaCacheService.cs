using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using NLog;
using XM.Shared.Core.EventManagement;

namespace XM.Shared.Core.Caching
{
    [ServiceBinding(typeof(AreaCacheService))]
    [ServiceBinding(typeof(IInitializable))]
    public class AreaCacheService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, uint> _areasByResref;
        private readonly Dictionary<uint, List<uint>> _playersByArea = new();

        private readonly XMEventService _event;

        public AreaCacheService(XMEventService @event)
        {
            _event = @event;
            _areasByResref = new Dictionary<string, uint>();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<AreaEvent.OnAreaEnter>(AddPlayerToCache);
            _event.Subscribe<AreaEvent.OnAreaExit>(RemovePlayerFromCache);
        }

        private void AddPlayerToCache(uint area)
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player))
                return;

            if (!_playersByArea.ContainsKey(area))
                _playersByArea[area] = new List<uint>();

            if (!_playersByArea[area].Contains(player))
                _playersByArea[area].Add(player);
        }

        private void RemovePlayerFromCache(uint area)
        {
            var player = GetExitingObject();
            if (!GetIsPC(player))
                return;

            if (!_playersByArea.ContainsKey(area))
                _playersByArea[area] = new List<uint>();

            if (_playersByArea[area].Contains(player))
                _playersByArea[area].Remove(player);
        }

        /// <summary>
        /// Retrieves an area by its resref. If the area does not exist, OBJECT_INVALID will be returned.
        /// </summary>
        /// <param name="resref">The resref to use for the search.</param>
        /// <returns>The area ID or OBJECT_INVALID if area does not exist.</returns>
        public uint GetAreaByResref(string resref)
        {
            if (!_areasByResref.ContainsKey(resref))
                return OBJECT_INVALID;

            return _areasByResref[resref];
        }

        public Dictionary<string, uint> GetAllAreas()
        {
            return _areasByResref.ToDictionary(x => x.Key, y => y.Value);
        }

        public List<uint> GetPlayersInArea(uint area)
        {
            if (!_playersByArea.ContainsKey(area))
                return new List<uint>();

            return _playersByArea[area].ToList();
        }

        /// <summary>
        /// Caches all areas by their resref.
        /// </summary>
        private void CacheAreasByResref()
        {
            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                var resref = GetResRef(area);
                _areasByResref[resref] = area;
            }

            _logger.Info($"Cached {_areasByResref.Count} areas by resref.");
        }

        public void Init()
        {
            CacheAreasByResref();
        }
    }
}
