﻿using System;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal abstract class JobDefinitionBase: IJobDefinition
    {
        public abstract bool IsVisibleToPlayers { get; }
        public abstract LocaleString Name { get; }
        public abstract string IconResref { get; }
        public abstract JobGrade Grades { get; }
        public abstract Dictionary<int, FeatType> FeatAcquisitionLevels { get; }

        private Dictionary<FeatType, int> _featsByLevel = new();

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
