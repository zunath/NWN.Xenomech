using System.Collections.Generic;

namespace XM.Shared.Core.Dialog.Snippet
{
    public interface ISnippetListDefinition
    {
        public Dictionary<string, SnippetDetail> BuildSnippets();
    }
}
