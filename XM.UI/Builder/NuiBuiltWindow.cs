using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder
{
    public class NuiBuiltWindow
    {
        public NuiWindow Window { get; }
        public NuiEventCollection EventCollection { get; internal set; }
        public NuiRect DefaultGeometry { get; }
        public Dictionary<string, Json> PartialViews { get; }

        public NuiBuiltWindow(
            NuiWindow window, 
            NuiRect defaultGeometry,
            Dictionary<string, Json> partialViews)
        {
            Window = window;
            DefaultGeometry = defaultGeometry;
            PartialViews = partialViews;
        }
    }
}
