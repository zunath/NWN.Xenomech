using System.Collections.Generic;
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

        public AreaCacheService(XMEventService @event)
        {
            _areasByResref = new Dictionary<string, uint>();
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
