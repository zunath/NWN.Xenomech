using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiBuiltWindow
    {
        public NuiWindow Window { get; }
        public NuiEventCollection EventCollection { get; }
        public NuiRect DefaultGeometry { get; }

        public NuiBuiltWindow(
            NuiWindow window, 
            NuiEventCollection eventCollection,
            NuiRect defaultGeometry)
        {
            Window = window;
            EventCollection = eventCollection;
            DefaultGeometry = defaultGeometry;
        }
    }
}
