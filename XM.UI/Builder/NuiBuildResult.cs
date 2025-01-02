using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiBuildResult
    {
        public NuiWindow Window { get; }
        public NuiEventCollection EventCollection { get; }

        public NuiBuildResult(NuiWindow window, NuiEventCollection eventCollection)
        {
            Window = window;
            EventCollection = eventCollection;
        }
    }
}
