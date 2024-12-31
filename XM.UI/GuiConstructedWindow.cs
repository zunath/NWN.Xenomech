using System.Collections.Generic;
using XM.API.BaseTypes;
using XM.UI.Component;
using XM.UI.WindowDefinition;

namespace XM.UI
{
    public class GuiConstructedWindow
    {
        public GuiWindowType Type { get; set; }
        public string WindowId { get; set; }
        public Json Window { get; set; }
        public CreatePlayerWindowDelegate CreatePlayerWindowAction { get; set; }
        public GuiRectangle InitialGeometry { get; set; }
        public Dictionary<string, Json> PartialViews { get; set; }

        public GuiConstructedWindow(
            GuiWindowType type, 
            string windowId, 
            Json window,
            GuiRectangle initialGeometry,
            Dictionary<string, Json> partialViews,
            CreatePlayerWindowDelegate createPlayerWindowAction)
        {
            Type = type;
            WindowId = windowId;
            Window = window;
            InitialGeometry = initialGeometry;
            PartialViews = partialViews;
            CreatePlayerWindowAction = createPlayerWindowAction;
        }
    }
}
