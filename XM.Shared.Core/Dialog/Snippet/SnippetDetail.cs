namespace XM.Shared.Core.Dialog.Snippet
{
    public class SnippetDetail
    {
        public string Description { get; set; }
        public SnippetConditionDelegate ConditionAction { get; set; }
        public SnippetActionDelegate ActionsTakenAction { get; set; }

        public SnippetDetail()
        {
            Description = string.Empty;
        }
    }
}
