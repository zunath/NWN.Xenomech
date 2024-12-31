using System.Collections.Generic;

namespace XM.Dialog.Snippet
{
    public interface ISnippetListDefinition
    {
        public Dictionary<string, SnippetDetail> BuildSnippets();
    }
}
