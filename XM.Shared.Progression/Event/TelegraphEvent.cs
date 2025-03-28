﻿using XM.Shared.Core.EventManagement;

namespace XM.Progression.Event
{
    internal class TelegraphEvent
    {
        public struct RunTelegraphEffect : IXMEvent
        {
        }

        public struct TelegraphApplied : IXMEvent
        {
        }

        public struct TelegraphTicked : IXMEvent
        {
        }

        public struct TelegraphRemoved : IXMEvent
        {
        }
    }
}
