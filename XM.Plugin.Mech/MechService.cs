using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using NLog;
using XM.Inventory;

namespace XM.Plugin.Mech
{
    [ServiceBinding(typeof(MechService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class MechService : IInitializable
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        private readonly ItemCacheService _itemCache;

        private readonly IList<IMechPartListDefinition> _partDefinitions;
        private readonly IList<IMechFrameListDefinition> _frameDefinitions;

        private readonly Dictionary<string, MechPart> _mechParts = new();
        private readonly Dictionary<string, MechFrame> _mechFrames = new();
        private readonly Dictionary<MechPartType, List<string>> _partsByType = new();

        public MechService(
            ItemCacheService itemCache,
            IList<IMechPartListDefinition> partDefinitions,
            IList<IMechFrameListDefinition> frameDefinitions)
        {
            _itemCache = itemCache;
            _partDefinitions = partDefinitions;
            _frameDefinitions = frameDefinitions;
        }

        public void Init()
        {
            CacheMechParts();
            CacheMechFrames();
        }

        private void CacheMechParts()
        {
            foreach (var definition in _partDefinitions)
            {
                var parts = definition.BuildMechParts();
                foreach (var (resref, part) in parts)
                {
                    part.Name = _itemCache.GetItemNameByResref(resref);

                    if (!_mechParts.TryAdd(resref, part))
                    {
                        _log.Error($"ERROR: Duplicate mech part detected: {resref}");
                        continue;
                    }

                    if (!_partsByType.ContainsKey(part.PartType))
                        _partsByType[part.PartType] = new List<string>();

                    _partsByType[part.PartType].Add(resref);
                }
            }

            _log.Info($"Loaded {_mechParts.Count} mech parts across {_partsByType.Count} categories.");
        }

        private void CacheMechFrames()
        {
            foreach (var definition in _frameDefinitions)
            {
                var frames = definition.BuildMechFrames();
                foreach (var (resref, frame) in frames)
                {
                    frame.Name = _itemCache.GetItemNameByResref(resref);

                    if (!_mechFrames.TryAdd(resref, frame))
                    {
                        _log.Error($"ERROR: Duplicate mech frame detected: {resref}");
                    }
                }
            }

            _log.Info($"Loaded {_mechFrames.Count} mech frames.");
        }

        /// <summary>
        /// Gets the mech part details by resref.
        /// </summary>
        /// <param name="resref">The resref of the mech part.</param>
        /// <returns>The mech part stats, or null if not found.</returns>
        public MechPart GetMechPart(string resref)
        {
            _mechParts.TryGetValue(resref, out var partStats);
            return partStats;
        }

    }
}