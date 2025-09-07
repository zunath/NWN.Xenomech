using System.Collections.Generic;
using Anvil.Services;
using NLog;

namespace XM.Plugin.Mech
{
    [ServiceBinding(typeof(MechService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class MechService : IInitializable
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        private readonly IList<IMechPartListDefinition> _partDefinitions;
        private readonly IList<IMechFrameListDefinition> _frameDefinitions;

        private readonly Dictionary<string, MechPartStats> _mechParts = new();
        private readonly Dictionary<string, MechFrameStats> _mechFrames = new();
        private readonly Dictionary<MechPartType, List<string>> _partsByType = new();

        public MechService(
            IList<IMechPartListDefinition> partDefinitions,
            IList<IMechFrameListDefinition> frameDefinitions)
        {
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
                foreach (var (resref, partStats) in parts)
                {
                    if (!_mechParts.TryAdd(resref, partStats))
                    {
                        _log.Error($"ERROR: Duplicate mech part detected: {resref}");
                        continue;
                    }

                    if (!_partsByType.ContainsKey(partStats.PartType))
                        _partsByType[partStats.PartType] = new List<string>();

                    _partsByType[partStats.PartType].Add(resref);
                }
            }

            _log.Info($"Loaded {_mechParts.Count} mech parts across {_partsByType.Count} categories.");
        }

        private void CacheMechFrames()
        {
            foreach (var definition in _frameDefinitions)
            {
                var frames = definition.BuildMechFrames();
                foreach (var (resref, frameStats) in frames)
                {
                    if (!_mechFrames.TryAdd(resref, frameStats))
                    {
                        _log.Error($"ERROR: Duplicate mech frame detected: {resref}");
                    }
                }
            }

            _log.Info($"Loaded {_mechFrames.Count} mech frames.");
        }

    }
}