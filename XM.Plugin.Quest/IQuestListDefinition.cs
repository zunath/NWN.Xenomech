using System.Collections.Generic;

namespace XM.Quest
{
    internal interface IQuestListDefinition
    {
        public Dictionary<string, QuestDetail> BuildQuests();
    }
}
