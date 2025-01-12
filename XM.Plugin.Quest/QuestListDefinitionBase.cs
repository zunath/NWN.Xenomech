using System.Collections.Generic;
using Anvil.Services;

namespace XM.Quest
{
    [ServiceBinding(typeof(IQuestListDefinition))]
    internal abstract class QuestListDefinitionBase: IQuestListDefinition
    {
        public abstract Dictionary<string, QuestDetail> BuildQuests();
    }
}
