using System;
using XM.Shared.Core.EventManagement;

namespace XM.UI.Event
{
    public class UIEvent
    {
        public struct UIRefreshEvent : IXMEvent
        {
        }
        public struct OpenWindow : IXMEvent
        {
            public Type ViewType { get; set; }

            public OpenWindow(Type viewType)
            {
                ViewType = viewType;
            }
        }
        public struct CloseWindow: IXMEvent
        {
            public Type ViewType { get; set; }

            public CloseWindow(Type viewType)
            {
                ViewType = viewType;
            }
        }
        public struct ToggleWindow: IXMEvent
        {
            public Type ViewType { get; set; }

            public ToggleWindow(Type viewType)
            {
                ViewType = viewType;
            }
        }
    }
}
