using System;

namespace XM.UI
{
    public class RequestCloseWindowEventArgs: EventArgs
    {
        public uint Player { get; set; }
        public Type ViewType { get; set; }

        public RequestCloseWindowEventArgs(uint player, Type viewType)
        {
            Player = player;
            ViewType = viewType;
        }
    }
}
