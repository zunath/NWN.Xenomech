

namespace XM.Shared.Core.Data
{
    public class SearchCriteria
    {
        public string Text { get; set; }
        public bool SkipEscaping { get; set; }

        public SearchCriteria(string text)
        {
            Text = text;
            SkipEscaping = false;
        }
    }
}
