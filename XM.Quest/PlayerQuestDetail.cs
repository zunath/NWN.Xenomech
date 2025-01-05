using System;
using System.Collections.Generic;

namespace XM.Quest
{
    public class PlayerQuestDetail
    {
        public int CurrentState { get; set; }
        public int TimesCompleted { get; set; }
        public DateTime? DateLastCompleted { get; set; }

        public Dictionary<QuestNPCGroupType, int> KillProgresses { get; set; } = new();
        public Dictionary<string, int> ItemProgresses { get; set; } = new();
    }
}
