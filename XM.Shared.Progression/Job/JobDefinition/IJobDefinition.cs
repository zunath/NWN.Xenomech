﻿using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal interface IJobDefinition
    {
        public bool IsVisibleToPlayers { get; }
        public LocaleString Name { get; }
        public string IconResref { get; }
        public JobGrade Grades { get; }
        public Dictionary<int, FeatType> FeatAcquisitionLevels { get; }
        public int GetFeatAcquiredLevel(FeatType feat);
    }
}
