using Anvil.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using XM.Inventory;
using XM.Shared.Core.Extension;

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
        private readonly Dictionary<MechPartType, MechPartTypeAttribute> _mechPartTypes = new();

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
            CacheMechPartTypes();
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

        private void CacheMechPartTypes()
        {
            var types = Enum.GetValues(typeof(MechPartType)).Cast<MechPartType>();
            foreach (var type in types)
            {
                var typeDetail = type.GetAttribute<MechPartType, MechPartTypeAttribute>();

                if (typeDetail.IsActive)
                {
                    _mechPartTypes[type] = typeDetail;
                }
            }
        }

        /// <summary>
        /// Retrieves the mech part type details.
        /// </summary>
        /// <param name="type">The type of mech part to retrieve</param>
        /// <returns>A mech part type attribute</returns>
        public MechPartTypeAttribute GetMechPartType(MechPartType type)
        {
            return _mechPartTypes[type];
        }
    }
}