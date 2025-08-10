using System;
using System.Collections.Generic;

namespace XM.Shared.Core.Entity
{
    public class PlayerQuestDetail
    {
        public int CurrentState { get; set; }
        public int TimesCompleted { get; set; }
        public DateTime? DateLastCompleted { get; set; }

        public Dictionary<int, int> KillProgresses { get; set; } = new();
        public Dictionary<string, int> ItemProgresses { get; set; } = new();
    }
}


