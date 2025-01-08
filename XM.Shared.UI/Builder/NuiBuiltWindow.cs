using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder
{
    public class NuiBuiltWindow
    {
        public string WindowId { get; }
        public Json Window { get; }
        public NuiEventCollection EventCollection { get; internal set; }
        public NuiRect DefaultGeometry { get; }
        public Dictionary<string, Json> PartialViews { get; }

        public NuiBuiltWindow(
            string windowId,
            Json window, 
            NuiRect defaultGeometry,
            Dictionary<string, Json> partialViews)
        {
            WindowId = windowId;
            Window = window;
            DefaultGeometry = defaultGeometry;
            PartialViews = partialViews;
        }
    }
}
