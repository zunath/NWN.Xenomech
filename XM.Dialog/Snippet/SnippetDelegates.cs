namespace XM.Dialog.Snippet
{
    public delegate bool SnippetConditionDelegate(uint player, string[] args);

    public delegate void SnippetActionDelegate(uint player, string[] args);
}
