using System;
using XM.Localization;

namespace XM.Quest
{
    internal class QuestNPCGroupAttribute: Attribute
    {
        public LocaleString Name { get; set; }

        public QuestNPCGroupAttribute(LocaleString name)
        {
            Name = name;
        }
    }
}
