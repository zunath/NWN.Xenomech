using System;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal abstract class JobDefinitionBase: IJobDefinition
    {
        public abstract JobType Type { get; }
        public abstract bool IsVisibleToPlayers { get; }
        public abstract LocaleString Name { get; }
        public abstract string IconResref { get; }
        public abstract JobGrade Grades { get; }
        public abstract Dictionary<int, FeatType> FeatAcquisitionLevels { get; }

        private readonly Dictionary<FeatType, int> _featsByLevel = new();

        private readonly Dictionary<int, FeatType> _resonanceNodeLevels = new()
        {
            {5, FeatType.ResonanceNode1},
            {10, FeatType.ResonanceNode2},
            {15, FeatType.ResonanceNode3},
            {20, FeatType.ResonanceNode4},
            {25, FeatType.ResonanceNode5},
            {30, FeatType.ResonanceNode6},
            {35, FeatType.ResonanceNode7},
            {40, FeatType.ResonanceNode8},
            {45, FeatType.ResonanceNode9},
            {50, FeatType.ResonanceNode10}
        };

        protected JobDefinitionBase()
        {
            CacheFeatsByLevel();
        }

        private void CacheFeatsByLevel()
        {
            foreach (var (level, feat) in FeatAcquisitionLevels)
            {
                if (!_featsByLevel.TryAdd(feat, level))
                    throw new Exception($"Feat {feat} has already been registered for job.");
            }
        }

        public int GetFeatAcquiredLevel(FeatType feat)
        {
            return !_featsByLevel.ContainsKey(feat) 
                ? 999 
                : _featsByLevel[feat];
        }
    }
}
