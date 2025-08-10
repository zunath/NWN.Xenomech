using XM.Shared.Core.Localization;

namespace XM.Codex.Codex.Model
{
    public sealed class CodexEntryRecord
    {
        public LocaleString Title { get; init; }
        public CodexCategory Category { get; init; }
        public LocaleString Content { get; init; }

        public CodexEntryRecord(LocaleString title, CodexCategory category, LocaleString content)
        {
            Title = title;
            Category = category;
            Content = content;
        }
    }
}


