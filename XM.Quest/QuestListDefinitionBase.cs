using System.Collections.Generic;

namespace XM.Quest
{
    internal abstract class QuestListDefinitionBase: IQuestListDefinition
    {
        protected QuestBuilder Builder { get; private set; }

        public abstract Dictionary<string, QuestDetail> BuildQuests();


        protected QuestListDefinitionBase(QuestBuilder builder)
        {
            Builder = builder;
        }
    }
}
