using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    [ServiceBinding(typeof(IJobDefinition))]
    internal abstract class JobDefinitionBase: IJobDefinition
    {
        public abstract JobType Type { get; }
        public abstract ClassType NWNClass { get; }
        public abstract bool IsVisibleToPlayers { get; }
        public abstract LocaleString Name { get; }
        public abstract string IconResref { get; }
        public abstract StatGrade Grades { get; }
        public abstract Dictionary<int, FeatType> FeatAcquisitionLevels { get; }
        private readonly Dictionary<FeatType, int> _featsByLevel = new();

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
                ? -1 
                : _featsByLevel[feat];
        }
    }
}
