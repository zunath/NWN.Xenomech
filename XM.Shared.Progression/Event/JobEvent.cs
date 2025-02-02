using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.Progression.Event
{
    public class JobEvent
    {
        public struct PlayerChangedJobEvent : IXMEvent
        {

        }

        public struct JobFeatRemovedEvent : IXMEvent
        {
            public FeatType Feat { get; }
            public JobFeatRemovedEvent(FeatType feat)
            {
                Feat = feat;
            }
        }

        public struct JobFeatAddedEvent : IXMEvent
        {
            public FeatType Feat { get; }

            public JobFeatAddedEvent(FeatType feat)
            {
                Feat = feat;
            }
        }
    }
}
